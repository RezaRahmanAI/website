namespace TechNova.Core.Entities;

public class Enrollment : AuditableEntity
{
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string ProgressStatus { get; set; } = "NotStarted";
}
