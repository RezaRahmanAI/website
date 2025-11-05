namespace TechNova.Core.Entities;

public class Certificate : AuditableEntity
{
    public Guid EnrollmentId { get; set; }
    public Enrollment? Enrollment { get; set; }
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public string DownloadUrl { get; set; } = string.Empty;
}
