using NewsletterApp.Shared.Models.Common;

namespace NewsletterApp.Shared.Models.Entities;

public class NewsLetterSubscriber : AuditableEntity
{
    public string Email { get; set; }

    public List<NewsLetter> NewsLetters { get; set; } = new();
}