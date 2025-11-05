namespace TechNova.Core.Entities;

public class PortfolioMedia : AuditableEntity
{
    public string Url { get; set; } = string.Empty;
    public string MediaType { get; set; } = "image";
    public Guid PortfolioItemId { get; set; }
    public PortfolioItem? PortfolioItem { get; set; }
}
