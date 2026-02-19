using Notes.Api.Dto;
using Notes.Api.Models;

namespace Notes.Api.Services;

public interface IFilesService
{
    Task<List<FileResponse>>  GetAllFilesAsync();
    Task<List<FileResponse?>> GetFileByGuidAsync(Guid id);
    Task<FileResponse> AddFileAsync(AddFileResponse file);
    Task<bool>  UpdateFileAsync(Guid id, UpdateFileResponse file);
    Task<bool>  DeleteFileAsync(Guid id);
}