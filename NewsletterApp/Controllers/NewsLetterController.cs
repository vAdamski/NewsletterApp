using Microsoft.AspNetCore.Mvc;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Models.DTO;

namespace NewsletterApp.Controllers;

public class NewsLetterController : Controller
{
    private readonly INewLettersService _newLettersService;

    public NewsLetterController(INewLettersService newLettersService)
    {
        _newLettersService = newLettersService;
    }
    
    public async Task<IActionResult> Index()
    {
        var model = await _newLettersService.GetList();
        
        return View(model);
    }
    
    public async Task<IActionResult> Subscribe(string email, Guid newsLetterId)
    {
        var result = await _newLettersService.Subscribe(newsLetterId, email);
        
        return RedirectToAction("Index", "Home");
    }
    
    public async Task<IActionResult> Unsubscribe(string email, Guid newsLetterId)
    {
        return RedirectToAction("Index", "Home");
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateNewsLetterDto model)
    {
        var result = await _newLettersService.Create(model);
        
        if (result.StatusCode == 1)
        {
            return RedirectToAction("Index");
        }

        return View(model);
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _newLettersService.Delete(id);
        
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _newLettersService.GetEditDto(id);
        
        if (result == null)
        {
            return RedirectToAction("Index");
        }
        
        return View(result);
    }
    
    public async Task<IActionResult> SaveEdit(EditNewsLetterDto dto)
    {
        var result = await _newLettersService.SaveEditedNewsLetter(dto);
        
        if (result.StatusCode == 1)
        {
            return RedirectToAction("Index");
        }
        
        return RedirectToAction("Edit", dto);
    }
}