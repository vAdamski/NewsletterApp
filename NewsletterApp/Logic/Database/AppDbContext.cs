using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsletterApp.Models.Common;
using NewsletterApp.Models.Entities;

namespace NewsletterApp.Logic.Database;

public interface IAppAppDbContext
{
    DbSet<NewsLetter> NewsLetters { get; set; }
    DbSet<NewsContent> NewsContents { get; set; }
    DbSet<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}

public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppAppDbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public DbSet<NewsLetter> NewsLetters { get; set; }
    public DbSet<NewsContent> NewsContents { get; set; }
    public DbSet<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NewsLetter>(x =>
        {
            x.HasKey(x => x.Id);

            x.HasMany(x => x.NewsContents)
                .WithOne(x => x.NewsLetter)
                .HasForeignKey(x => x.NewsLetterId)
                .OnDelete(DeleteBehavior.Cascade);

            x.HasMany(x => x.NewsLetterSubscribers)
                .WithMany(x => x.NewsLetters);
        });
        
        modelBuilder.Entity<NewsContent>(x =>
        {
            x.HasKey(x => x.Id);
        });
        
        modelBuilder.Entity<NewsLetterSubscriber>(x =>
        {
            x.HasKey(x => x.Id);
        });
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.Entity.ModifiedBy = GetCurrentUser();
                    entry.Entity.Modified = DateTime.Now;
                    entry.Entity.Inactivated = DateTime.Now;
                    entry.Entity.InactivatedBy = GetCurrentUser();
                    entry.Entity.StatusId = 0;
                    entry.State = EntityState.Modified;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = GetCurrentUser();
                    entry.Entity.Modified = DateTime.Now;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedBy = GetCurrentUser();
                    entry.Entity.Created = DateTime.Now;
                    entry.Entity.StatusId = 1;
                    break;
                default:
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    private string GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonymous";
    }
}
