namespace TechNova.Core.Entities;

public class CourseCategory : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
