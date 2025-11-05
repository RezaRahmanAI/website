using TechNova.API.Models.Responses;
using TechNova.Core.Entities;

namespace TechNova.API.Extensions;

public static class MappingExtensions
{
    public static CourseResponse ToResponse(this Course course)
    {
        return new CourseResponse
        {
            Id = course.Id,
            Title = course.Title,
            Summary = course.Summary,
            Description = course.Description,
            Level = course.Level,
            Price = course.Price,
            Duration = course.Duration,
            ThumbnailUrl = course.ThumbnailUrl,
            InstructorName = course.Instructor?.User is null ? string.Empty : $"{course.Instructor.User.FirstName} {course.Instructor.User.LastName}",
            Categories = course.Categories.Select(category => category.Name)
        };
    }
}
