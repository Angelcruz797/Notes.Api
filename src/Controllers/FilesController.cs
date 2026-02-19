using Microsoft.AspNetCore.Mvc;
using Notes.Api.Dto;
using Notes.Api.Models;
using Notes.Api.Services;

namespace Notes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController(IFilesService service): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<FileResponse>>> GetAllFiles()
        => await service.GetAllFilesAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<FileResponse?>> GetFileByGuid(string id)
    {
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "File not found" });
        }
        var file = await service.GetFileByGuidAsync(guidId);
        if (file is null)
        {
            return NotFound(new { message = "File not found" });
        }
        return file;
    }
    
    [HttpPost]
    public async Task<ActionResult<FileResponse>> AddFile(AddFileResponse file)
        => await service.AddFileAsync(file);

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateFile(string id, UpdateFileResponse file)
    {
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "File not found" });
        }
        return await service.UpdateFileAsync(guidId, file);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteFile(string id)
    {
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "File not found" });
        }
        return await service.DeleteFileAsync(guidId);
    }
}