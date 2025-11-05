using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PortfolioController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetPortfolioAsync(CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.PortfolioItems.ListAsync(cancellationToken);
        return Ok(items);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreatePortfolioItemAsync([FromBody] PortfolioItem item, CancellationToken cancellationToken)
    {
        item.CreatedBy = User?.Identity?.Name ?? "system";
        await _unitOfWork.PortfolioItems.AddAsync(item, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetPortfolioAsync), new { id = item.Id }, item);
    }
}
