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
    {
        if (file.FileName == string.Empty || file.FileType == string.Empty)
        {
            return BadRequest(new  { message = "File name and type must not be empty" });
        }
        if (file.FileType == "Draw" || file.FileType == "Text")
        {
            return  await service.AddFileAsync(file);
        }
        else
        {
            return BadRequest(new  { message = "File type must be 'Draw' or 'Text'" });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateFile(string id, UpdateFileResponse file)
    {
        if (file.FileType == string.Empty ||file.FileName == string.Empty)
        {
            return BadRequest(new  { message = "File fields must not be empty" });
        }
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "File id not found" });
        }
        if (file.FileType == "Draw" || file.FileType == "Text")
        {
            return await service.UpdateFileAsync(guidId, file);
        }
        else
        {
            return BadRequest(new  { message = "File type must be 'Draw' or 'Text'" });
        }
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