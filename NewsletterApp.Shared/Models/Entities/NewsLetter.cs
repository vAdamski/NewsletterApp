using NewsletterApp.Shared.Models.Common;

namespace NewsletterApp.Shared.Models.Entities;

public class NewsLetter : AuditableEntity
{
    public string Name { get; set; }

    public List<NewsContent> NewsContents { get; set; } = new();
    public List<NewsLetterSubscriber> NewsLetterSubscribers { get; set; } = new();
}