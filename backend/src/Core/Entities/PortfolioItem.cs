namespace TechNova.Core.Entities;

public class PortfolioItem : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ResultSummary { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public ICollection<PortfolioMedia> Media { get; set; } = new List<PortfolioMedia>();
    public ICollection<PortfolioTag> Tags { get; set; } = new List<PortfolioTag>();
}
