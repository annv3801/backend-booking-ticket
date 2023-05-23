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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<FilmSchedule> FilmSchedules { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Slider> Sliders { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = _currentAccountService.Id;
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.LastModifiedById = _currentAccountService.Id;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedById = _currentAccountService.Id;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    public async Task<int> SaveChangesAsync(Account account, CancellationToken cancellationToken = default(CancellationToken))
    {
        foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedById = account.Id;
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.LastModifiedById = account.Id;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedById = account.Id;
                    entry.Entity.LastModified = DateTime.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }
}
