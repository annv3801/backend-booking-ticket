using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Databases;
public class ApplicationDbContext : DbContext
{
    private readonly ICurrentAccountService _currentAccountService;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentAccountService currentAccountService) : base(options)
    {
        _currentAccountService = currentAccountService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountToken> AccountTokens { get; set; }
    public DbSet<AccountLogin> AccountLogins { get; set; }
}
