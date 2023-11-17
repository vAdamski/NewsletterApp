using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Models;

namespace NewsletterApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INewLettersService _newLettersService;

    public HomeController(ILogger<HomeController> logger, INewLettersService newLettersService)
    {
        _logger = logger;
        _newLettersService = newLettersService;
    }

    public IActionResult Index()
    {
        var response = _newLettersService.GetList();
        
        return View(response);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}