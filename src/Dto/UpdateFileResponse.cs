namespace Notes.Api.Dto;

public class UpdateFileResponse
{
    public Guid  Id { get; set; }
    public required string FileName { get; set; }
    public string Content { get; set; }= string.Empty;
}