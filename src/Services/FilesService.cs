using Notes.Api.Models;

namespace Notes.Api.Services;

public class FilesService : IFilesService
{
    private static List<Files> files = new List<Files>
    {
        new Models.Files
        {
            Id = 1, 
            FileName = "file1", 
            Content = "{\"content\":\"content\"}",
            CreateDate = DateOnly.FromDateTime(DateTime.Now)
        },
        new Models.Files
        {
            Id = 2, 
            FileName = "file2", 
            Content = "{\"content\":\"content\"}",
            CreateDate = DateOnly.FromDateTime(DateTime.Now)
        },
    };
    
    public async Task<List<Files>> GetAllFilesAsync()
        =>await Task.FromResult(files);

    public async Task<Files?> GetFileByIdAsync(int id)
        => await Task.FromResult(files.Find(f => f.Id == id));
    
    public Task<Files>  AddFileAsync(Files file)
    {
        throw new NotImplementedException();
    }
    public Task<bool>  UpdateFileAsync(int id, Files file)
    {
        throw new NotImplementedException();
    }
    public Task<bool>  DeleteFileAsync(int id)
    {
        throw new NotImplementedException();
    }
}