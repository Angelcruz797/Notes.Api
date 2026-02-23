namespace Notes.Api.Models;

public class Files
{
    public Guid Id { get; set; }
    public int UserId { get; set; } 
    public string FileName { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FileType { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
}