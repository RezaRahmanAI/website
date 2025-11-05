using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("webhook")]
    [AllowAnonymous]
    public IActionResult ReceiveWebhook()
    {
        // Placeholder for Stripe/PayPal webhook handling
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> RecordPaymentAsync([FromBody] PaymentTransaction transaction, CancellationToken cancellationToken)
    {
        transaction.CreatedBy = User?.Identity?.Name ?? "system";
        await _unitOfWork.Payments.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Ok(transaction);
    }
}
