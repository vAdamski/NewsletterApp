namespace NewsletterApp.Logic.Interfaces;

public interface IHttpService
{
    Task<string> GetAsync(string uri);
}