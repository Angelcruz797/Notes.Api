namespace Notes.Api.Models;

public class Files
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string FileName { get; set; }
    public string Content { get; set; }
    public DateOnly CreateDate { get; set; }
    public DateOnly LastUpdatedDate { get; set; }
    
}