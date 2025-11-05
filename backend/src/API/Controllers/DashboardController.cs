using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechNova.Core.Interfaces;
using System.Linq;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin")]
public class DashboardController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public DashboardController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStatsAsync(CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.ListAsync(cancellationToken);
        var courses = await _unitOfWork.Courses.ListAsync(cancellationToken);
        var enrollments = await _unitOfWork.Enrollments.ListAsync(cancellationToken);
        var payments = await _unitOfWork.Payments.ListAsync(cancellationToken);

        var stats = new
        {
            TotalUsers = users.Count,
            TotalCourses = courses.Count,
            TotalEnrollments = enrollments.Count,
            TotalRevenue = payments.Sum(payment => payment.Amount)
        };

        return Ok(stats);
    }
}
