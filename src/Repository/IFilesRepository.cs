using Notes.Api.Dto;

namespace Notes.Api.Repository;

public interface IFilesRepository
{
    Task<List<FileResponse>> GetAllFiles();
    Task<FileResponse?> GetFileById(Guid id);
    Task<FileResponse> AddFile(AddFileResponse file);
    Task<bool>  UpdateFile(Guid id,UpdateFileResponse file);
    Task<bool>  DeleteFile(Guid id);
}