using NewsletterApp.Models.Common;

namespace NewsletterApp.Models.Entities;

public class NewsLetterSubscriber : AuditableEntity
{
    public string Email { get; set; }

    public List<NewsLetter> NewsLetters { get; set; } = new();
}