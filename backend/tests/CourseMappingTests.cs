using System;
using System.Collections.Generic;
using TechNova.API.Extensions;
using TechNova.Core.Entities;
using Xunit;

namespace TechNova.Tests;

public class CourseMappingTests
{
    [Fact]
    public void ToResponse_MapsCourseFields()
    {
        var course = new Course
        {
            Title = "Test Course",
            Summary = "Summary",
            Description = "Description",
            Level = "Beginner",
            Price = 199,
            Duration = TimeSpan.FromHours(10),
            ThumbnailUrl = "https://example.com/image.png",
            Instructor = new InstructorProfile
            {
                User = new ApplicationUser { FirstName = "Jane", LastName = "Doe" }
            },
            Categories = new List<CourseCategory>
            {
                new() { Name = "Programming" }
            }
        };

        var response = course.ToResponse();

        Assert.Equal(course.Title, response.Title);
        Assert.Equal("Jane Doe", response.InstructorName);
        Assert.Contains("Programming", response.Categories);
    }
}
