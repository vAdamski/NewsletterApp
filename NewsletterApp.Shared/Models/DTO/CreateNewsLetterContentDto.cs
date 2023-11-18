namespace NewsletterApp.Shared.Models.DTO;

public class CreateNewsLetterContentDto
{
    public Guid NewsLetterId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}