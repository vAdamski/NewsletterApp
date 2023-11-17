using NewsletterApp.Models.DTO;

namespace NewsletterApp.Models.ViewModels;

public class NewsLettersListVm
{
    public List<NewsLetterDto> NewsLetterDtos { get; set; } = new();
}