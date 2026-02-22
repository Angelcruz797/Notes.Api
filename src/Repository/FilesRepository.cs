using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;
using Notes.Api.Dto;
using Notes.Api.Models;

namespace Notes.Api.Repository;

public class FilesRepository: IFilesRepository
{
    private readonly AppDbContext _context;
    public FilesRepository(AppDbContext context)
    {
        _context =  context;
    }

    public async Task<List<FileResponse>> GetAllFiles()
    {
        return await _context.Files.Select(c=> new FileResponse
        {
            Id = c.Id,
            FileName = c.FileName,
            Content = c.Content,
            FileType = c.FileType,
            CreateDate = c.CreateDate,
            LastUpdatedDate = c.LastUpdatedDate,
        }).ToListAsync();
    }
    public async Task<FileResponse?> GetFileById(Guid id)
    {
        return await _context.Files
            .Where(c => c.Id == id)
            .Select(c=>new FileResponse
            {
                Id=c.Id,
                FileName = c.FileName,
                Content = c.Content,
                FileType = c.FileType,
                CreateDate = c.CreateDate,
                LastUpdatedDate = c.LastUpdatedDate,
            }).FirstOrDefaultAsync();
    }

    public async Task<FileResponse> AddFile(AddFileResponse file)
    {
        var newCharacter = new Files
        {
            UserId = 1,
            FileName = file.FileName,
            FileType = file.FileType,
            Content = "",
        };
        
        _context.Files.Add(newCharacter);
        await _context.SaveChangesAsync();
        return (new FileResponse
        {
            Id = newCharacter.Id,
            Content = newCharacter.Content,
            FileName = newCharacter.FileName,
            FileType = newCharacter.FileType,
            CreateDate = newCharacter.CreateDate,
            LastUpdatedDate = newCharacter.LastUpdatedDate,
        });
    }

    public async Task<bool> UpdateFile(Guid id, UpdateFileResponse file)
    {
        var currentFile = await _context.Files.FindAsync(id);

        if (currentFile == null) return false;
        currentFile.FileName = file.FileName;
        currentFile.Content = file.Content;
        currentFile.FileType = file.FileType;
        currentFile.LastUpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteFile(Guid id)
    {
        var currentFile = await _context.Files.FindAsync(id);
        if (currentFile == null) return false;
        _context.Files.Remove(currentFile);
        await _context.SaveChangesAsync();
        return true;
    }
}