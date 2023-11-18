namespace NewsletterApp.Models.DTO;

public class UnsubscribeNewsLetterDto
{
    public Guid NewsLetterId { get; set; }
    public string Email { get; set; }
}