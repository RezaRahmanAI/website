using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public BlogController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetPostsAsync(CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.BlogPosts.ListAsync(cancellationToken);
        return Ok(posts);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetPostBySlugAsync(string slug, CancellationToken cancellationToken)
    {
        var post = (await _unitOfWork.BlogPosts.SearchAsync(p => p.Slug == slug, cancellationToken)).FirstOrDefault();
        if (post is null)
        {
            return NotFound();
        }

        return Ok(post);
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpPost]
    public async Task<IActionResult> CreatePostAsync([FromBody] BlogPost post, CancellationToken cancellationToken)
    {
        post.CreatedBy = User?.Identity?.Name ?? "system";
        await _unitOfWork.BlogPosts.AddAsync(post, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetPostBySlugAsync), new { slug = post.Slug }, post);
    }
}
