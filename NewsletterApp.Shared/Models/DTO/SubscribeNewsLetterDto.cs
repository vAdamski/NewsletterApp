namespace NewsletterApp.Shared.Models.DTO;

public class SubscribeNewsLetterDto
{
    public Guid NewsLetterId { get; set; }
    public string Email { get; set; }
}