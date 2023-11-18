using NewsletterApp.Shared.Models.DTO;

namespace NewsletterApp.Shared.Models.ViewModels;

public class NewsLettersListVm
{
    public List<NewsLetterDto> NewsLetterDtos { get; set; } = new();
}