

using NewsletterApp.Shared.Models.DTO;
using NewsletterApp.Shared.Models.ViewModels;

namespace NewsletterApp.Logic.Interfaces;

public interface INewsLetterContentsService
{
    Task<NewsLetterContentVm> GetNewsLettersContentForNewsLetter(Guid id);
    Task<Status> Create(CreateNewsLetterContentDto dto);
    Task<UpdateNewsLetterContentDto> GetUpdateModel(Guid id);
    Task<Status> Update(UpdateNewsLetterContentDto dto);
    Task<Status> Delete(Guid id);
}