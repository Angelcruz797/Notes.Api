using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;
using Notes.Api.Dto;
using Notes.Api.Models;

namespace Notes.Api.Services;

public class FilesService(AppDbContext context) : IFilesService
{
    public async Task<List<FileResponse>> GetAllFilesAsync()
    {
        return await context.Files.Select(c=> new FileResponse
        {
            Id = c.Id,
            FileName = c.FileName,
            Content = c.Content,
            CreateDate = c.CreateDate,
            LastUpdatedDate = c.LastUpdatedDate,
        }).ToListAsync();
    }

    public async Task<FileResponse?> GetFileByGuidAsync(Guid id)
    {
        var result =await context.Files
            .Where(c => c.Id == id)
            .Select(c=>new FileResponse
            {
                Id=c.Id,
                FileName = c.FileName,
                Content = c.Content,
                CreateDate = c.CreateDate,
                LastUpdatedDate = c.LastUpdatedDate,
            }).FirstOrDefaultAsync();
        return result;
    }
    
    public async Task<FileResponse>  AddFileAsync(AddFileResponse file)
    {
        var newCharacter = new Files
        {
            UserId = 1,
            FileName = file.FileName,
            Content = "",
        };
        context.Files.Add(newCharacter);
        await context.SaveChangesAsync();

        return (new FileResponse
        {
            Id = newCharacter.Id,
            Content = newCharacter.Content,
            FileName = newCharacter.FileName,
            CreateDate = newCharacter.CreateDate,
            LastUpdatedDate = newCharacter.LastUpdatedDate,
        });
    }
    public async Task<bool> UpdateFileAsync(Guid id, UpdateFileResponse file)
    {
        var currentFile = await context.Files.FindAsync(id);

        if (currentFile == null) return false;
        currentFile.FileName = file.FileName;
        currentFile.Content = file.Content;
        currentFile.LastUpdatedDate = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFileAsync(Guid id)
    {
        var currentFile = await context.Files.FindAsync(id);
        if (currentFile == null) return false;
        context.Files.Remove(currentFile);
        await context.SaveChangesAsync();
        return true;
    }
}