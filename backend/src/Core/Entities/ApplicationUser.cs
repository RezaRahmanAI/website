using TechNova.Core.Enums;

namespace TechNova.Core.Entities;

public class ApplicationUser : AuditableEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string NormalizedEmail { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Client;
    public string? PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public InstructorProfile? InstructorProfile { get; set; }
}
