namespace Notes.Api.Dto;

public class FileResponse
{
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public string Content { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
}