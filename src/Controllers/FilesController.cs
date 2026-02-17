using Microsoft.AspNetCore.Mvc;
using Notes.Api.Models;
using Notes.Api.Services;

namespace Notes.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController(IFilesService service): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Files>>> GetAllFiles()
        => await service.GetAllFilesAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Files>> GetFileById(int id)
    {
        var file = await service.GetFileByIdAsync(id);
        if (file is null)
        {
            return NotFound(new { message = "File not found" });
        }
        return file;
    }
}