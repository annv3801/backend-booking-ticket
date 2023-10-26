using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Common.Configurations;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Implementations;
using Application.Interface;
using Application.Repositories.Account;
using Application.Repositories.Category;
using Application.Services.Account;
using Application.Services.Category;
using Domain.Common.Implementations;
using Domain.Common.Interface;
using Domain.Extensions;
using Infrastructure.Databases;
using Infrastructure.Repositories.Account;
using Infrastructure.Repositories.Category;
using Infrastructure.Services;
using Infrastructure.Services.Common;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationDbContext"));
});
// Add services to the container.
builder.Services.AddOptions();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"
};

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApplicationConfiguration>(builder.Configuration.GetSection("ApplicationConfigurations"));
builder.Services.Configure<PasswordOptions>(builder.Configuration.GetSection("PasswordOptions"));
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfigurations"));
builder.Services.AddLocalization(option => { option.ResourcesPath = "Resources"; });

#region Dependency Injection

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IStringLocalizationService, StringLocalizationServiceService>();
builder.Services.AddSingleton<ICurrentAccountService, CurrentAccountService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordGeneratorService, PasswordGeneratorService>();
builder.Services.AddScoped<IPaginationService, PaginationService>();
builder.Services.AddScoped<IJsonSerializerService, JsonSerializerService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ISnowflakeIdService, SnowflakeIdService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IDateTimeService, DateTimeService>();

// Account
builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();    
builder.Services.AddScoped<IAccountTokenRepository, AccountTokenRepository>();

// Category
builder.Services.AddScoped<ICategoryManagementService, CategoryManagementService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();    

#endregion

#region JWT

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
// Configure JWT authentication mechanism
builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
//.AddCookie(config => config.SlidingExpiration = true)
.AddJwtBearer(config =>
{
    //cfg.
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = builder.Configuration["JwtConfigurations:Issuer"],
        ValidAudience = builder.Configuration["JwtConfigurations:Audience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtConfigurations:SymmetricSecurityKey"])),
    };
    config.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // If the request is for our hub...
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/notification/web")))
            {
                // Read the token out of the query string
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        },
        OnTokenValidated = async context =>
        {
            // To check token is valid and must be existing in the UserToken table in the database
            // Once JWT is not existing in the UserToken table, the authentication process will be set as failed.

            var userIdClaim =
                context.Principal?.Claims.FirstOrDefault(claim => claim.Type == "uid");
            if (userIdClaim == null)
            {
                context.Fail("JWT Token does not contain User Id Claim.");
            }

            var accountRepository = context.HttpContext.RequestServices.GetRequiredService<IAccountRepository>();

            var token = context.HttpContext.Request.Headers["Authorization"].ToString()
                .Replace("Bearer ", "");
            // If we cannot get token from header, try to use from querystring (for wss)
            if (token.IsMissing())
            {
                token = context.Request.Query["access_token"];
            }

            // Check the token from the db
            if (!await accountRepository.IsValidJwtAsync(
                long.Parse(userIdClaim?.Value), token,
                "SELF", CancellationToken.None))
            {
                context.Fail("JWT Token is invalid or no longer exists.");
            }

            Console.WriteLine(@"Token Validated OK");
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            // Ensure we always have an error and error description.
            if (string.IsNullOrEmpty(context.Error))
                context.Error = "invalid_token";
            if (string.IsNullOrEmpty(context.ErrorDescription))
            {
                // Pass the message from OnTokenValidated on method context.Fail(<message>)
                if (context.AuthenticateFailure != null &&
                    context.AuthenticateFailure.Message.Length > 0)
                {
                    context.ErrorDescription = context.AuthenticateFailure.Message;
                }
                else
                {
                    // If we dont have error message from OnTokenValidated, set a message
                    context.ErrorDescription =
                        "This request requires a valid JWT access token to be provided.";
                }
            }

            // Add some extra context for expired tokens.
            if (context.AuthenticateFailure != null && context.AuthenticateFailure.GetType() ==
                typeof(SecurityTokenExpiredException))
            {
                var authenticationException =
                    context.AuthenticateFailure as SecurityTokenExpiredException;
                context.Response.Headers.Add("WWW-Authenticate", "Bearer");
                context.ErrorDescription = $"The token expired on {authenticationException?.Expires:o}";
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = 401,
                error = context.Error,
                errorDescription = context.ErrorDescription
            }));
        }
    };
});

#endregion

builder.Services.AddCors(options =>
    options.AddPolicy(
        "AllowInternal",
        policy => policy.WithOrigins("*")
            .WithMethods("POST", "GET", "OPTIONS", "PUT", "DELETE")
            .AllowAnyHeader()
            .AllowAnyOrigin()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
var uploadsPath = Path.Combine(builder.Environment.ContentRootPath, "Uploads");

if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(uploadsPath),
    RequestPath = "/Resources"
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowInternal");
app.Run();