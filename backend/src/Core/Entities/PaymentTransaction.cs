namespace TechNova.Core.Entities;

public class PaymentTransaction : AuditableEntity
{
    public Guid UserId { get; set; }
    public ApplicationUser? User { get; set; }
    public Guid? CourseId { get; set; }
    public Course? Course { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public string PaymentGateway { get; set; } = string.Empty;
    public string TransactionReference { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
}
