using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsletterApp.Logic.Database;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Logic.Services;
using NewsletterApp.Logic.Services.NewsLetterContents;
using NewsletterApp.Logic.Services.NewsLetters;
using NewsletterApp.Shared.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options => options.
    UseSqlServer(connectionString));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

// Add services to the container.
builder.Services.AddControllersWithViews();

//DI
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IAppAppDbContext, AppDbContext>();
builder.Services.AddScoped<INewLettersService, NewLettersService>();
builder.Services.AddScoped<INewsLetterContentsService, NewsLetterContentsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using(var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();