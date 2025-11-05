namespace TechNova.Core.Entities;

public class BlogPost : AuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string FeaturedImageUrl { get; set; } = string.Empty;
    public bool IsPublished { get; set; }
    public DateTime? PublishedAt { get; set; }
    public Guid AuthorId { get; set; }
    public ApplicationUser? Author { get; set; }
    public ICollection<BlogTag> Tags { get; set; } = new List<BlogTag>();
}
