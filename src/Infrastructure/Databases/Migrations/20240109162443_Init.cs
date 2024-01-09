using System;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Databases.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Film");

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    AvatarPhoto = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FullName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PasswordChangeRequired = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    PasswordValidUntilDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordHashTemporary = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "3"),
                    UserName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    SecurityStamp = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    LockoutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    Otp = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: true, defaultValueSql: "'000000'"),
                    OtpValidEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OtpCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false, defaultValueSql: "'?'"),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NormalizedName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    ObjectId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Theaters",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TotalRating = table.Column<decimal>(type: "numeric", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    Latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountLogin",
                schema: "Film",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId1 = table.Column<long>(type: "bigint", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AccountLogin_Account_AccountId1",
                        column: x => x.AccountId1,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTokens",
                schema: "Film",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTokens", x => new { x.LoginProvider, x.Name, x.AccountId });
                    table.ForeignKey(
                        name: "FK_AccountTokens_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Total = table.Column<double>(type: "double precision", nullable: false),
                    TotalBeforeDiscount = table.Column<double>(type: "double precision", nullable: false),
                    Discount = table.Column<double>(type: "double precision", nullable: false),
                    CouponId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    IsReceived = table.Column<int>(type: "integer", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountCategory",
                schema: "Film",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountCategory", x => new { x.AccountId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AccountCategory_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Film",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    GroupEntityId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryIds = table.Column<List<long>>(type: "jsonb", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Director = table.Column<string>(type: "text", nullable: true),
                    Actor = table.Column<string>(type: "text", nullable: true),
                    Genre = table.Column<string>(type: "text", nullable: true),
                    Premiere = table.Column<string>(type: "text", nullable: true),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: true),
                    Rated = table.Column<string>(type: "text", nullable: true),
                    Trailer = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    TotalRating = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_Groups_GroupEntityId",
                        column: x => x.GroupEntityId,
                        principalSchema: "Film",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    GroupEntityId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Groups_GroupEntityId",
                        column: x => x.GroupEntityId,
                        principalSchema: "Film",
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoles",
                schema: "Film",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccountRoles_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Film",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "Film",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "Film",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Film",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Film",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountFavorites",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: true),
                    FilmId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFavorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountFavorites_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Film",
                        principalTable: "Theaters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilmFeedback",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmFeedback_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "Film",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmFeedback_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomSeats",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomSeats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomSeats_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Film",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedulers",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    FilmId = table.Column<long>(type: "bigint", nullable: false),
                    TheaterId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedulers_Films_FilmId",
                        column: x => x.FilmId,
                        principalSchema: "Film",
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulers_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "Film",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulers_Theaters_TheaterId",
                        column: x => x.TheaterId,
                        principalSchema: "Film",
                        principalTable: "Theaters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomSeatId = table.Column<long>(type: "bigint", nullable: false),
                    SchedulerId = table.Column<long>(type: "bigint", nullable: false),
                    TicketId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_RoomSeats_RoomSeatId",
                        column: x => x.RoomSeatId,
                        principalSchema: "Film",
                        principalTable: "RoomSeats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Schedulers_SchedulerId",
                        column: x => x.SchedulerId,
                        principalSchema: "Film",
                        principalTable: "Schedulers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalSchema: "Film",
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                schema: "Film",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookingId = table.Column<long>(type: "bigint", nullable: false),
                    SeatId = table.Column<long>(type: "bigint", nullable: false),
                    Foods = table.Column<List<FoodRequest>>(type: "jsonb", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<long>(type: "bigint", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalSchema: "Film",
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Seats_SeatId",
                        column: x => x.SeatId,
                        principalSchema: "Film",
                        principalTable: "Seats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Film",
                table: "Account",
                columns: new[] { "Id", "AvatarPhoto", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "Email", "EmailConfirmed", "FullName", "Gender", "LockoutEnabled", "LockoutEnd", "ModifiedBy", "ModifiedTime", "NormalizedEmail", "NormalizedUserName", "Otp", "OtpCount", "OtpValidEnd", "PasswordHash", "PasswordHashTemporary", "PasswordValidUntilDate", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "UserName" },
                values: new object[] { 921946681335811L, null, 921946681335812L, new DateTimeOffset(new DateTime(2024, 1, 9, 16, 24, 43, 290, DateTimeKind.Unspecified).AddTicks(2484), new TimeSpan(0, 0, 0, 0, 0)), false, null, null, "nva030801@gmail.com", true, "Nguyen Van An", true, true, null, 921946681335812L, new DateTimeOffset(new DateTime(2024, 1, 9, 16, 24, 43, 290, DateTimeKind.Unspecified).AddTicks(2468), new TimeSpan(0, 0, 0, 0, 0)), "NVA030801@GMAIL.COM", "NVA3801", "000000", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AMJoiJQ9xLazxisVPXx+lBDRw7wfWBerhXipsLpHNGLXGAAKIeCnwi5XhIRbTbqovA==", null, null, "0966093801", true, "31C8FDF8-298B-417E-89F0-4B71BE16EC5D", 3, "admin" });

            migrationBuilder.InsertData(
                schema: "Film",
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "Description", "ModifiedBy", "ModifiedTime", "Name", "NormalizedName" },
                values: new object[] { 921946681335813L, "ROOT:ROOT:SYSADMIN", null, null, false, null, null, "The system admin permission", null, null, "System Admin", "SYSTEM ADMIN" });

            migrationBuilder.InsertData(
                schema: "Film",
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "Deleted", "DeletedBy", "DeletedTime", "Description", "ModifiedBy", "ModifiedTime", "Name", "NormalizedName", "Status" },
                values: new object[] { 921946681335812L, null, null, false, null, null, "The system Admin Role", null, null, "System Admin", "SYSTEM ADMIN", 1 });

            migrationBuilder.InsertData(
                schema: "Film",
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" },
                values: new object[] { 921946681335811L, 921946681335812L });

            migrationBuilder.InsertData(
                schema: "Film",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 921946681335813L, 921946681335812L });

            migrationBuilder.CreateIndex(
                name: "IX_Account_PhoneNumber",
                schema: "Film",
                table: "Account",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountCategory_CategoryId",
                schema: "Film",
                table: "AccountCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_AccountId",
                schema: "Film",
                table: "AccountFavorites",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_FilmId",
                schema: "Film",
                table: "AccountFavorites",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFavorites_TheaterId",
                schema: "Film",
                table: "AccountFavorites",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLogin_AccountId1",
                schema: "Film",
                table: "AccountLogin",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccountLogin_LoginProvider_ProviderKey",
                schema: "Film",
                table: "AccountLogin",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_AccountId_RoleId",
                schema: "Film",
                table: "AccountRoles",
                columns: new[] { "AccountId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoles_RoleId",
                schema: "Film",
                table: "AccountRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTokens_AccountId",
                schema: "Film",
                table: "AccountTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTokens_LoginProvider_Name_AccountId",
                schema: "Film",
                table: "AccountTokens",
                columns: new[] { "LoginProvider", "Name", "AccountId" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                schema: "Film",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_SeatId",
                schema: "Film",
                table: "BookingDetails",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AccountId",
                schema: "Film",
                table: "Bookings",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "Film",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmFeedback_AccountId",
                schema: "Film",
                table: "FilmFeedback",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmFeedback_FilmId",
                schema: "Film",
                table: "FilmFeedback",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_GroupEntityId",
                schema: "Film",
                table: "Films",
                column: "GroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Name",
                schema: "Film",
                table: "Films",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_GroupEntityId",
                schema: "Film",
                table: "Foods",
                column: "GroupEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Title",
                schema: "Film",
                table: "Foods",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Title",
                schema: "Film",
                table: "Groups",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_News_Title",
                schema: "Film",
                table: "News",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                schema: "Film",
                table: "Permissions",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_NormalizedName",
                schema: "Film",
                table: "Permissions",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId_RoleId",
                schema: "Film",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                schema: "Film",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "Film",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_NormalizedName",
                schema: "Film",
                table: "Roles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Name",
                schema: "Film",
                table: "Rooms",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TheaterId",
                schema: "Film",
                table: "Rooms",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomSeats_Name",
                schema: "Film",
                table: "RoomSeats",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomSeats_RoomId",
                schema: "Film",
                table: "RoomSeats",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_FilmId",
                schema: "Film",
                table: "Schedulers",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_RoomId",
                schema: "Film",
                table: "Schedulers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulers_TheaterId",
                schema: "Film",
                table: "Schedulers",
                column: "TheaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_RoomSeatId",
                schema: "Film",
                table: "Seats",
                column: "RoomSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SchedulerId_RoomSeatId",
                schema: "Film",
                table: "Seats",
                columns: new[] { "SchedulerId", "RoomSeatId" });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_TicketId",
                schema: "Film",
                table: "Seats",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Slides_Name",
                schema: "Film",
                table: "Slides",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theaters_Name",
                schema: "Film",
                table: "Theaters",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Title",
                schema: "Film",
                table: "Tickets",
                column: "Title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountCategory",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "AccountFavorites",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "AccountLogin",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "AccountRoles",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "AccountTokens",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "BookingDetails",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "FilmFeedback",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Foods",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "News",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Slides",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Seats",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Account",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "RoomSeats",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Schedulers",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Tickets",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Films",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Groups",
                schema: "Film");

            migrationBuilder.DropTable(
                name: "Theaters",
                schema: "Film");
        }
    }
}
