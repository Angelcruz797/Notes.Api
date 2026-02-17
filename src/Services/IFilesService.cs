using Notes.Api.Models;

namespace Notes.Api.Services;

public interface IFilesService
{
    Task<List<Files>>  GetAllFilesAsync();
    Task<Files?>  GetFileByIdAsync(int id);
    Task<Files>  AddFileAsync(Files file);
    Task<bool>  UpdateFileAsync(int id, Files file);
    Task<bool>  DeleteFileAsync(int id);
}