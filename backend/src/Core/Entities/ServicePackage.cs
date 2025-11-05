namespace TechNova.Core.Entities;

public class ServicePackage : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string BillingCycle { get; set; } = "One-time";
    public Guid ServiceOfferingId { get; set; }
    public ServiceOffering? ServiceOffering { get; set; }
}
