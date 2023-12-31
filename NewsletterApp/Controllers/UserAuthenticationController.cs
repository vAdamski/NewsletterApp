using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Shared.Models.DTO;

namespace NewsletterApp.Controllers;

public class UserAuthenticationController : Controller
{
    private readonly IUserAuthenticationService _authService;

    public UserAuthenticationController(IUserAuthenticationService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> RegisterAdmin()
    {
        RegistrationModel model = new RegistrationModel
        {
            Username = "admin",
            Email = "admin@gmail.com",
            FirstName = "Adam",
            LastName = "Ludwiczak",
            Password = "Pass123$"
        };
        model.Role = "admin";
        var result = await _authService.RegisterAsync(model);
        return RedirectToAction("Login");
    }

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _authService.LoginAsync(model);
        if (result.StatusCode == 1)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["msg"] = result.Message;
            return RedirectToAction(nameof(Login));
        }
    }

    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration(RegistrationModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Role = "user";
        var result = await _authService.RegisterAsync(model);
        TempData["msg"] = result.Message;
        return RedirectToAction(nameof(Registration));
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var result = await _authService.ChangePasswordAsync(model, User.Identity.Name);
        TempData["msg"] = result.Message;
        return RedirectToAction(nameof(ChangePassword));
    }
}