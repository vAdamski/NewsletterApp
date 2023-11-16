using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsletterApp.Models.Entities;

namespace NewsletterApp.Logic.Database;

public interface IAppAppDbContext
{
    
}

public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
