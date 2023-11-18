using NewsletterApp.Models.DTO;
using NewsletterApp.Models.ViewModels;

namespace NewsletterApp.Logic.Interfaces;

public interface INewLettersService
{
    Task<Status> Create(CreateNewsLetterDto model);
    Task<Status> Delete(Guid id);
    Task<NewsLettersListVm> GetList();
    Task<EditNewsLetterDto> GetEditDto(Guid id);
    Task<Status> SaveEditedNewsLetter(EditNewsLetterDto dto);
    Task<Status> Unsubscribe(Guid newsletterId, Guid subscriberId);
    Task<Status> Subscribe(Guid newsletterId, string email);
}