namespace TechNova.Core.Entities;

public class InstructorProfile : AuditableEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Expertise { get; set; } = string.Empty;
    public string ExperienceLevel { get; set; } = string.Empty;
    public string? LinkedInUrl { get; set; }
    public string? TwitterUrl { get; set; }
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
