using Microsoft.EntityFrameworkCore;
using NewsletterApp.Logic.Database;
using NewsletterApp.Logic.Interfaces;
using NewsletterApp.Models.DTO;
using NewsletterApp.Models.Entities;
using NewsletterApp.Models.ViewModels;

namespace NewsletterApp.Logic.Services.NewsLetterContents;

public class NewsLetterContentsService : INewsLetterContentsService
{
    private readonly IAppAppDbContext _ctx;

    public NewsLetterContentsService(IAppAppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<NewsLetterContentVm> GetNewsLettersContentForNewsLetter(Guid id)
    {
        var response = await _ctx.NewsLetters
            .Where(x => x.Id == id)
            .Include(x => x.NewsContents)
            .FirstOrDefaultAsync();

        if (response == null)
        {
            return null;
        }

        var reponse = new NewsLetterContentVm()
        {
            NewsLetterId = response.Id,
            NewsLetterTitle = response.Name,
            NewsLetterContentDtos = response.NewsContents
                .Where(x => x.StatusId == 1)
                .Select(x => new NewsLetterContentDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Created = x.Created
                })
                .OrderByDescending(x => x.Created)
                .ToList()
        };

        return reponse;
    }

    public async Task<Status> Create(CreateNewsLetterContentDto dto)
    {
        var newsLetter = await _ctx.NewsLetters
            .Where(x => x.Id == dto.NewsLetterId)
            .FirstOrDefaultAsync();

        if (newsLetter == null)
        {
            return new Status()
            {
                StatusCode = 0,
                Message = "NewsLetter not found"
            };
        }

        var entity = new NewsContent()
        {
            Title = dto.Title,
            Content = dto.Content,
        };

        newsLetter.NewsContents.Add(entity);

        _ctx.NewsLetters.Update(newsLetter);

        await _ctx.SaveChangesAsync();

        return new Status()
        {
            StatusCode = 1,
            Message = "NewsLetterContent created"
        };
    }

    public async Task<UpdateNewsLetterContentDto> GetUpdateModel(Guid id)
    {
        var entity = await _ctx.NewsContents
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        return new UpdateNewsLetterContentDto()
        {
            Id = entity.Id,
            NewsLetterId = entity.NewsLetterId,
            Title = entity.Title,
            Content = entity.Content
        };
    }

    public async Task<Status> Update(UpdateNewsLetterContentDto dto)
    {
        var entity = await _ctx.NewsContents
            .Where(x => x.Id == dto.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return new Status
            {
                StatusCode = 0,
                Message = "NewsLetterContent not found"
            };
        }

        entity.Title = dto.Title;
        entity.Content = dto.Content;

        _ctx.NewsContents.Update(entity);

        await _ctx.SaveChangesAsync();

        return new Status
        {
            StatusCode = 1,
            Message = "NewsLetterContent updated"
        };
    }

    public async Task<Status> Delete(Guid id)
    {
        var entity = await _ctx.NewsContents
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return new Status
            {
                StatusCode = 0,
                Message = "NewsLetterContent not found"
            };
        }

        _ctx.NewsContents.Remove(entity);

        await _ctx.SaveChangesAsync();

        return new Status
        {
            StatusCode = 1,
            Message = "NewsLetterContent deleted"
        };
    }
}