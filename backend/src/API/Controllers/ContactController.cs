using Microsoft.AspNetCore.Mvc;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ContactController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> SubmitInquiryAsync([FromBody] ServiceInquiry inquiry, CancellationToken cancellationToken)
    {
        inquiry.CreatedBy = "public";
        await _unitOfWork.ServiceInquiries.AddAsync(inquiry, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Accepted(new { message = "Thank you for reaching out. Our team will respond shortly." });
    }
}
