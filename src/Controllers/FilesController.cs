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
    public async Task<ActionResult<FileResponse?>> GetFileByGuid(Guid id)
    {
        var file = await service.GetFileByGuidAsync(id);
        if (file is null)
        {
            return NotFound(new { message = "File not found" });
        }
        return Ok(file);
    }
    
    [HttpPost]
    public async Task<ActionResult<FileResponse>> AddFile(AddFileResponse file)
        => await service.AddFileAsync(file);

    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateFile(Guid id, UpdateFileResponse file)
        => await service.UpdateFileAsync(id, file);
}