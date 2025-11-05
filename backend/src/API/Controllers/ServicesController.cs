using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ServicesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetServicesAsync(CancellationToken cancellationToken)
    {
        var services = await _unitOfWork.Services.ListAsync(cancellationToken);
        return Ok(services);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateServiceAsync([FromBody] ServiceOffering service, CancellationToken cancellationToken)
    {
        service.CreatedBy = User?.Identity?.Name ?? "system";
        await _unitOfWork.Services.AddAsync(service, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return CreatedAtAction(nameof(GetServicesAsync), new { id = service.Id }, service);
    }

    [HttpPost("inquiries")]
    public async Task<IActionResult> CreateInquiryAsync([FromBody] ServiceInquiry inquiry, CancellationToken cancellationToken)
    {
        inquiry.CreatedBy = User?.Identity?.Name ?? "public";
        await _unitOfWork.ServiceInquiries.AddAsync(inquiry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Accepted(inquiry);
    }
}
