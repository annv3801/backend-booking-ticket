using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Databases;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountToken> AccountTokens { get; set; }
    public DbSet<AccountLogin> AccountLogins { get; set; }
    public DbSet<AccountCategoryEntity> AccountCategory { get; set; }
    public DbSet<AccountFavoritesEntity> AccountFavorites { get; set; }
    public DbSet<FilmFeedbackEntity> FilmFeedback { get; set; }
    public DbSet<CategoryEntity> Category { get; set; }
    public DbSet<GroupEntity> Group { get; set; }
    public DbSet<TheaterEntity> Theater { get; set; }
    public DbSet<FilmEntity> Film { get; set; }
}
