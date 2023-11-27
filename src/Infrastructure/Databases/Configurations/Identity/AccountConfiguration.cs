using Application.Common;
using Domain.Entities.Identity;
using Domain.Enums;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Databases.Configurations.Identity;
public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();
        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.NormalizedEmail);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();

        builder.Property(u => u.Email).IsRequired().IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(u => u.NormalizedEmail).IsRequired().IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(t => t.EmailConfirmed).HasDefaultValueSql("true");
        builder.Property(t => t.AvatarPhoto).HasMaxLength(Constants.FieldLength.UrlMaxLength);

        builder.Property(u => u.FullName).IsUnicode().HasMaxLength(Constants.FieldLength.MiddleTextLength);

        builder.Property(u => u.UserName).IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength);
        builder.Property(u => u.NormalizedUserName).IsUnicode().HasMaxLength(Constants.FieldLength.TextMaxLength);

        builder.Property(u => u.PhoneNumber).IsUnicode().HasMaxLength(Constants.FieldLength.MiddleTextLength);
        builder.Property(u => u.PhoneNumberConfirmed).HasDefaultValueSql("true");

        builder.Property(t => t.PasswordChangeRequired).HasDefaultValueSql("false");
        builder.Property(t => t.Gender).HasDefaultValueSql("true");
        builder.Property(t => t.TwoFactorEnabled).HasDefaultValueSql("false");
        builder.Property(t => t.LockoutEnabled).HasDefaultValueSql("true");
        builder.Property(t => t.AccessFailedCount).HasDefaultValueSql("0");
        builder.Property(t => t.Status).HasDefaultValueSql(((int) AccountStatus.Active).ToString());


        builder.Property(t => t.Otp).HasMaxLength(6).HasDefaultValueSql("'000000'");

        builder.Property(u => u.SecurityStamp).HasMaxLength(36);
        // builder.Property(u => u.SecurityStamp).HasDefaultValue(Guid.NewGuid().ToString());
        builder.ToTable("Account", "Film");


        var records = new List<Account>();
        var accountSystem = new Account()
        {
            Id = 921946681335811,
            Email = "nva030801@gmail.com",
            NormalizedEmail = "NVA030801@GMAIL.COM",
            Gender = true,
            Status = AccountStatus.Active,
            AvatarPhoto = null,
            CreatedBy = 921946681335812,
            ModifiedBy = 921946681335812,
            ModifiedTime = DateTime.UtcNow,
            CreatedTime = DateTime.UtcNow,
            EmailConfirmed = true,
            FullName = "Nguyen Van An",
            PhoneNumber = "0966093801",
            UserName = "admin",
            PasswordHash = "AMJoiJQ9xLazxisVPXx+lBDRw7wfWBerhXipsLpHNGLXGAAKIeCnwi5XhIRbTbqovA==", //Abc@123456
            NormalizedUserName = "NVA3801",
            AccessFailedCount = 0,
            LockoutEnabled = true,
            PasswordChangeRequired = false,
            SecurityStamp = Guid.NewGuid().ToString().ToUpper(),
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
        };
        records.Add(accountSystem);
        builder.HasData(records);
    }
}
