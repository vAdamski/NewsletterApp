using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Shared.Models.DTO;

namespace NewsletterApp.Controllers;

[Authorize]
public class NewsContentController : Controller
{
    private readonly INewsLetterContentsService _newsLetterContentsService;
    private readonly IHttpService _httpService;
    private readonly ILogger<NewsContentController> _logger;

    public NewsContentController(INewsLetterContentsService newsLetterContentsService,
        IHttpService httpService,
        ILogger<NewsContentController> logger)
    {
        _newsLetterContentsService = newsLetterContentsService;
        _httpService = httpService;
        _logger = logger;
    }
    
    public async Task<IActionResult> Index(Guid id)
    {
        var response = await _newsLetterContentsService.GetNewsLettersContentForNewsLetter(id);
        
        return View(response);
    }
    
    public async Task<IActionResult> CreateGetView(Guid newsLetterId)
    {
        ViewBag.NewsLetterId = newsLetterId;
        
        return View("Create");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateNewsLetterContentDto model)
    {
        var response = await _newsLetterContentsService.Create(model);
        
        if (response.StatusCode == 1)
        {
            return RedirectToAction("Index", new { id = model.NewsLetterId });
        }
        
        ViewBag.NewsLetterId = model.NewsLetterId;
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var response = await _newsLetterContentsService.GetUpdateModel(id);
        
        return View(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateNewsLetterContentDto model)
    {
        var response = await _newsLetterContentsService.Update(model);
        
        if (response.StatusCode == 1)
        {
            return RedirectToAction("Index", new { id = model.NewsLetterId });
        }
        
        return View(model);
    }
    
    public async Task<IActionResult> Delete(Guid id, Guid newsLetterId)
    {
        var response = await _newsLetterContentsService.Delete(id);
        
        return RedirectToAction("Index", new { id = newsLetterId });
    }

    [HttpGet]
    public async Task<IActionResult> Send(Guid id, Guid newsLetterId)
    {
        var url = $"{Environment.GetEnvironmentVariable("MAIL_SENDER_URL")}/MailSender/SendMails?id={id}";

        try
        {
            var response = await _httpService.GetAsync(url);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
        
        return RedirectToAction("Index", "NewsLetter", new { id = newsLetterId });
    }
}