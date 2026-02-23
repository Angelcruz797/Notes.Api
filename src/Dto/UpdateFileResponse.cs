namespace Notes.Api.Dto;

public class UpdateFileResponse
{
    public required string FileName { get; set; }
    public string Content { get; set; }= string.Empty;
    public string FileType { get; set; } = string.Empty;
}