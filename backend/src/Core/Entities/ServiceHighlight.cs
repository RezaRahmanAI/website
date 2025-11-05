namespace TechNova.Core.Entities;

public class ServiceHighlight : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid ServiceOfferingId { get; set; }
    public ServiceOffering? ServiceOffering { get; set; }
}
