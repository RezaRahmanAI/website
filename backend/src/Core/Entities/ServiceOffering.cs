namespace TechNova.Core.Entities;

public class ServiceOffering : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<ServicePackage> Packages { get; set; } = new List<ServicePackage>();
    public ICollection<ServiceHighlight> Highlights { get; set; } = new List<ServiceHighlight>();
}
