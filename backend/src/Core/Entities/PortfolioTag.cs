namespace TechNova.Core.Entities;

public class PortfolioTag : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid PortfolioItemId { get; set; }
    public PortfolioItem? PortfolioItem { get; set; }
}
