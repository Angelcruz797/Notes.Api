using Microsoft.EntityFrameworkCore;
using Notes.Api.Data;
using Notes.Api.Dto;
using Notes.Api.Handler;
using Notes.Api.Models;

namespace Notes.Api.Repository;

public class FilesRepository: IFilesRepository
{
    private readonly AppDbContext _context;
    private readonly Guid _userId;
    public FilesRepository(AppDbContext context, IJwtHandler jwtHandler)
    {
        _context =  context;
        _userId = jwtHandler.GetIdByToken();
    }

    public async Task<List<FileResponse>> GetAllFiles()
    {
        return await _context.Files
            .Where(c=>c.UserId == _userId)
            .Select(c=> new FileResponse
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
            .Where(c => c.Id == id && c.UserId == _userId)
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
            UserId = _userId,
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
        var currentFile = await _context.Files
            .Where(c=>c.Id == id && c.UserId == _userId)
            .SingleOrDefaultAsync();

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
        var currentFile = await _context.Files
            .Where(c=>c.Id == id && c.UserId == _userId)
            .SingleOrDefaultAsync();
        if (currentFile == null) return false;
        _context.Files.Remove(currentFile);
        await _context.SaveChangesAsync();
        return true;
    }
}