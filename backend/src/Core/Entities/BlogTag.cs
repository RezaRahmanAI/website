namespace TechNova.Core.Entities;

public class BlogTag : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid BlogPostId { get; set; }
    public BlogPost? BlogPost { get; set; }
}
