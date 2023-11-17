using NewsletterApp.Models.Common;

namespace NewsletterApp.Models.Entities;

public class NewsContent : AuditableEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    
    public Guid NewsLetterId { get; set; }
    public NewsLetter NewsLetter { get; set; }
}