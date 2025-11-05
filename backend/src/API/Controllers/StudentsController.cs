using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student,Instructor,Admin")]
public class StudentsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardAsync(CancellationToken cancellationToken)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdValue, out var userId))
        {
            return Unauthorized();
        }

        var enrollments = await _unitOfWork.Enrollments.SearchAsync(e => e.UserId == userId, cancellationToken);
        var certificates = await _unitOfWork.Certificates.SearchAsync(c => enrollments.Select(e => e.Id).Contains(c.EnrollmentId), cancellationToken);
        var notifications = await _unitOfWork.Notifications.SearchAsync(n => n.UserId == userId, cancellationToken);

        var response = new
        {
            Enrollments = enrollments.Select(e => new
            {
                e.Id,
                e.CourseId,
                e.EnrollmentDate,
                e.IsCompleted,
                e.ProgressStatus
            }),
            Certificates = certificates.Select(c => new
            {
                c.CertificateNumber,
                c.IssuedAt,
                c.DownloadUrl
            }),
            Notifications = notifications.OrderByDescending(n => n.SentAt).Take(10)
        };

        return Ok(response);
    }
}
