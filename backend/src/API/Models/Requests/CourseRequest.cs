using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TechNova.API.Models.Requests;

public class CourseRequest
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Summary { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string Level { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public Guid InstructorId { get; set; }

    public TimeSpan Duration { get; set; }
    public IEnumerable<string> CategoryIds { get; set; } = Array.Empty<string>();
}
