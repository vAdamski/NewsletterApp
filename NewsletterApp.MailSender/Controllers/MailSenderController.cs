using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsletterApp.Shared.Interfaces;

namespace NewsletterApp.MailSender.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class MailSenderController : ControllerBase
{
    private readonly IAppDbContext _ctx;
    private readonly IMailSenderService _mailSenderService;
    private readonly ILogger<MailSenderController> _logger;

    public MailSenderController(IAppDbContext ctx, IMailSenderService mailSenderService, ILogger<MailSenderController> logger)
    {
        _ctx = ctx;
        _mailSenderService = mailSenderService;
        _logger = logger;
    }

    [HttpGet]
    [Route("SendMails/{id}")]
    public async Task<IActionResult> SendMails(Guid id)
    {
        var newsLetter = await _ctx.NewsContents
            .Include(x => x.NewsLetter)
            .ThenInclude(x => x.NewsLetterSubscribers)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (newsLetter == null)
            return NotFound("NewsLetter not found");
        
        var subscribers = newsLetter.NewsLetter.NewsLetterSubscribers.Select(x => new {x.Email, x.Id}).ToList();
        var title = newsLetter.Title;
        var contnet = newsLetter.Content;
        
        foreach (var subscriber in subscribers)
        {
            _logger.LogInformation("Sending mail to {subscriber}");
            await _mailSenderService.SendMail(subscriber.Email, title, ContentBuilder(contnet, newsLetter.NewsLetterId, subscriber.Id));
        }
        
        return Ok();
    }

    private string ContentBuilder(string content, Guid newsLetterId, Guid subscriberId)
    {
        content += '\n';
        content += '\n';
        content += $"Unsubscribe: {Environment.GetEnvironmentVariable("")}/NewsLetter?subscriberId={subscriberId}&newsLetterId={newsLetterId}";
        
        return content;
    }
}