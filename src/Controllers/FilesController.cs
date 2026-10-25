using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Dto;
using Notes.Api.Services;

namespace Notes.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class FilesController(IFilesService service): ControllerBase
{
    private Guid GetUserID()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
    [HttpGet]
    public async Task<IActionResult> GetAllFiles()
        => Ok(await service.GetAllFilesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFileByGuid(string id)
    {
        Guid UserId = GetUserID();
        if (UserId == Guid.Empty)
        {
            return NotFound(new { message = "User not found" });
        }
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "Invalid guid" });
        }
        var file = await service.GetFileByGuidAsync(guidId);
        if (file is null)
        {
            return NotFound(new { message = "File not found" });
        }
        return Ok(file);
    }

    [HttpPost]
    public async Task<IActionResult> AddFile(AddFileResponse file)
    {
        if (file.FileName == string.Empty || file.FileType == string.Empty)
        {
            return BadRequest(new  { message = "File name and type must not be empty" });
        }
        if (file.FileType == "Draw" || file.FileType == "Text")
        {
            return Ok(await service.AddFileAsync(file));
        }
        else
        {
            return BadRequest(new  { message = "File type must be 'Draw' or 'Text'" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFile(string id, UpdateFileResponse file)
    {
        if (file.FileType == string.Empty ||file.FileName == string.Empty)
        {
            return BadRequest(new  { message = "File fields must not be empty" });
        }
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "Invalid guid" });
        }
        if (file.FileType == "Draw" || file.FileType == "Text")
        {
            return Ok(await service.UpdateFileAsync(guidId, file));
        }
        else
        {
            return BadRequest(new  { message = "File type must be 'Draw' or 'Text'" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(string id)
    {
        if (!Guid.TryParse(id,out var guidId))
        {
            return BadRequest(new  { message = "Invalid guid" });
        }
        return Ok(await service.DeleteFileAsync(guidId));
    }
}