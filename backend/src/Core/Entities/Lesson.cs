namespace TechNova.Core.Entities;

public class Lesson : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string ContentUrl { get; set; } = string.Empty;
    public string ContentType { get; set; } = "video";
    public int Order { get; set; }
    public TimeSpan Duration { get; set; }
    public Guid SectionId { get; set; }
    public CourseSection? Section { get; set; }
}
