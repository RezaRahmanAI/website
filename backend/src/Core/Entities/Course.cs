namespace TechNova.Core.Entities;

public class Course : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Level { get; set; } = "Beginner";
    public TimeSpan Duration { get; set; }
    public decimal Price { get; set; }
    public string Language { get; set; } = "English";
    public string ThumbnailUrl { get; set; } = string.Empty;
    public Guid InstructorId { get; set; }
    public InstructorProfile? Instructor { get; set; }
    public ICollection<CourseSection> Sections { get; set; } = new List<CourseSection>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
    public ICollection<CourseCategory> Categories { get; set; } = new List<CourseCategory>();
}
