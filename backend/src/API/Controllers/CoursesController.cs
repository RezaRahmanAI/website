using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TechNova.API.Extensions;
using TechNova.API.Models.Requests;
using TechNova.API.Models.Responses;
using TechNova.Core.Entities;
using TechNova.Core.Interfaces;

namespace TechNova.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CoursesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<CourseResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCoursesAsync([FromQuery] int pageIndex = 0, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
    {
        var courses = await _unitOfWork.Courses.ListAsync(cancellationToken);
        var totalCount = courses.Count;
        var paginated = courses.Skip(pageIndex * pageSize).Take(pageSize).Select(course => course.ToResponse());

        var response = new PagedResponse<CourseResponse>
        {
            Items = paginated.ToList(),
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCourseByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id, cancellationToken);
        if (course is null)
        {
            return NotFound();
        }

        return Ok(course.ToResponse());
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpPost]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCourseAsync([FromBody] CourseRequest request, CancellationToken cancellationToken)
    {
        var instructor = await _unitOfWork.Instructors.GetByIdAsync(request.InstructorId, cancellationToken);
        if (instructor is null)
        {
            return BadRequest("Invalid instructor identifier.");
        }

        var course = new Course
        {
            Title = request.Title,
            Summary = request.Summary,
            Description = request.Description,
            Level = request.Level,
            Duration = request.Duration,
            Price = request.Price,
            InstructorId = request.InstructorId,
            CreatedBy = User?.Identity?.Name ?? "system"
        };

        await _unitOfWork.Courses.AddAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetCourseByIdAsync), new { id = course.Id }, course.ToResponse());
    }

    [Authorize(Roles = "Admin,Instructor")]
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CourseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCourseAsync(Guid id, [FromBody] CourseRequest request, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id, cancellationToken);
        if (course is null)
        {
            return NotFound();
        }

        course.Title = request.Title;
        course.Summary = request.Summary;
        course.Description = request.Description;
        course.Level = request.Level;
        course.Duration = request.Duration;
        course.Price = request.Price;
        course.InstructorId = request.InstructorId;
        course.UpdatedAt = DateTime.UtcNow;
        course.UpdatedBy = User?.Identity?.Name ?? "system";

        await _unitOfWork.Courses.UpdateAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(course.ToResponse());
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id, cancellationToken);
        if (course is null)
        {
            return NotFound();
        }

        await _unitOfWork.Courses.DeleteAsync(course, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}
