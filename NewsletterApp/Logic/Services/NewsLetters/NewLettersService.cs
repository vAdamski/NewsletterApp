using Microsoft.EntityFrameworkCore;
using NewsletterApp.Logic.Database;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Models.DTO;
using NewsletterApp.Models.Entities;
using NewsletterApp.Models.ViewModels;

namespace NewsletterApp.Logic.Services.NewsLetters;

public class NewLettersService : INewLettersService
{
    private readonly IAppAppDbContext _ctx;

    public NewLettersService(IAppAppDbContext _ctx)
    {
        this._ctx = _ctx;
    }
    
    public async Task<Status> Create(CreateNewsLetterDto model)
    {
        var newsLetter = new NewsLetter
        {
            Name = model.Name
        };
        
        await _ctx.NewsLetters.AddAsync(newsLetter);
        
        await _ctx.SaveChangesAsync();

        return new Status()
        {
            Message = "NewsLetter created successfully",
            StatusCode = 1
        };
    }

    public async Task<Status> Delete(Guid id)
    {
        var newsLetter = await _ctx.NewsLetters.Where(x => x.Id == id)
            .Include(x => x.NewsContents)
            .FirstOrDefaultAsync();
        
        if (newsLetter == null)
        {
            return new Status()
            {
                Message = "NewsLetter not found",
                StatusCode = 0
            };
        }
        
        _ctx.NewsLetters.Remove(newsLetter);
        
        await _ctx.SaveChangesAsync();
        
        return new Status()
        {
            Message = "NewsLetter deleted successfully",
            StatusCode = 1
        };
    }

    public async Task<NewsLettersListVm> GetList()
    {
        var list = await _ctx.NewsLetters
            .Where(x => x.StatusId == 1)
            .ToListAsync();

        return new NewsLettersListVm
        {
            NewsLetterDtos = list.Select(x => new NewsLetterDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList()
        };
    }

    public async Task<EditNewsLetterDto> GetEditDto(Guid id)
    {
        var response = await _ctx.NewsLetters.FindAsync(id);
        
        if (response == null)
        {
            return null;
        }
        
        return new EditNewsLetterDto
        {
            Id = response.Id,
            Name = response.Name
        };
    }

    public async Task<Status> SaveEditedNewsLetter(EditNewsLetterDto dto)
    {
        var newsLetter = await _ctx.NewsLetters.FindAsync(dto.Id);
        
        if (newsLetter == null)
        {
            return new Status()
            {
                Message = "NewsLetter not found",
                StatusCode = 0
            };
        }
        
        newsLetter.Name = dto.Name;
        
        await _ctx.SaveChangesAsync();
        
        return new Status()
        {
            Message = "NewsLetter edited successfully",
            StatusCode = 1
        };
    }

    public async Task<Status> Subscribe(Guid newsletterId, string email)
    {
        var newsLetter = await _ctx.NewsLetters.FindAsync(newsletterId);
        
        if (newsLetter == null)
        {
            return new Status()
            {
                Message = "NewsLetter not found",
                StatusCode = 0
            };
        }
        
        var entity = new NewsLetterSubscriber()
        {
            Email = email
        };
        
        newsLetter.NewsLetterSubscribers.Add(entity);
        
        await _ctx.SaveChangesAsync();
        
        return new Status()
        {
            Message = "Subscribed successfully",
            StatusCode = 1
        };
    }
    
    public async Task<Status> Unsubscribe(Guid newsletterId, string email)
    {
        var newsLetter = await _ctx.NewsLetters.Where(x => x.Id == newsletterId)
            .Include(x => x.NewsLetterSubscribers)
            .FirstOrDefaultAsync();
        
        if (newsLetter == null)
        {
            return new Status()
            {
                Message = "NewsLetter not found",
                StatusCode = 0
            };
        }
        
        var entity = newsLetter.NewsLetterSubscribers.FirstOrDefault(x => x.Email == email);
        
        if (entity == null)
        {
            return new Status()
            {
                Message = "Email not found",
                StatusCode = 0
            };
        }
        
        newsLetter.NewsLetterSubscribers.Remove(entity);
        
        await _ctx.SaveChangesAsync();
        
        return new Status()
        {
            Message = "Unsubscribed successfully",
            StatusCode = 1
        };
    }
}