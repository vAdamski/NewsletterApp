namespace NewsletterApp.Models.DTO;

public class UpdateNewsLetterContentDto
{
    public Guid Id { get; set; }
    public Guid NewsLetterId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}