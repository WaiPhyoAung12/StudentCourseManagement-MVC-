using System;
using System.Collections.Generic;

namespace StudentCourseManagement.AppDbContextModels;

public partial class TblCourse
{
    public int Id { get; set; }

    public string CourseCode { get; set; } = null!;

    public string CourseTitle { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime? UpdatedDateTime { get; set; }

    public string? UpdatedUserId { get; set; }

    public bool? DeleteFlag { get; set; }
}
