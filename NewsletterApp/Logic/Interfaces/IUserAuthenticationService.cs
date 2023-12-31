using NewsletterApp.Shared.Models.DTO;

namespace NewsletterApp.Logic.Interfaces;

public interface IUserAuthenticationService
{
    Task<Status> LoginAsync(LoginModel model);
    Task LogoutAsync();
    Task<Status> RegisterAsync(RegistrationModel model);
    Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
}