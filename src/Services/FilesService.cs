using Notes.Api.Dto;
using Notes.Api.Repository;

namespace Notes.Api.Services;

public class FilesService : IFilesService
{
    private readonly IFilesRepository _repository;
    
    public FilesService(IFilesRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<FileResponse>> GetAllFilesAsync()
    {
        return await _repository.GetAllFiles();
    }

    public async Task<FileResponse?> GetFileByGuidAsync(Guid id)
    {
        var result = await _repository.GetFileById(id);
        return result;
    }
    
    public async Task<FileResponse>  AddFileAsync(AddFileResponse file)
    {
        return  await _repository.AddFile(file);
    }
    public async Task<bool> UpdateFileAsync(Guid id, UpdateFileResponse file)
    {
        return await _repository.UpdateFile(id,file);
    }

    public async Task<bool> DeleteFileAsync(Guid id)
    {
        return  await _repository.DeleteFile(id);
    }
}