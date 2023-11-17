using NewsletterApp.Models.DTO;

namespace NewsletterApp.Models.ViewModels;

public class NewsLetterContentVm
{
    public Guid NewsLetterId { get; set; }
    public string NewsLetterTitle { get; set; }
    public List<NewsLetterContentDto> NewsLetterContentDtos { get; set; } = new();
}