using Microsoft.EntityFrameworkCore;
using NewsletterApp.Shared.Models.Entities;

namespace NewsletterApp.Shared.Interfaces;

public interface IAppAppDbContext
{
    DbSet<NewsLetter> NewsLetters { get; set; }
    DbSet<NewsContent> NewsContents { get; set; }
    DbSet<NewsLetterSubscriber> NewsLetterSubscribers { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}