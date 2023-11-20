using System.Net;
using System.Text;
using NewsletterApp.Logic.Interfaces;

namespace NewsletterApp.Logic.Services;

public class HttpService : IHttpService
{
    private readonly HttpClient _client;

    public HttpService()
    {
        HttpClientHandler handler = new HttpClientHandler 
        { 
            AutomaticDecompression = DecompressionMethods.All 
        };
        
        _client = new HttpClient();
    }

    public async Task<string> GetAsync(string uri)
    {
        using HttpResponseMessage response = await _client.GetAsync(uri);

        return await response.Content.ReadAsStringAsync();
    }
}