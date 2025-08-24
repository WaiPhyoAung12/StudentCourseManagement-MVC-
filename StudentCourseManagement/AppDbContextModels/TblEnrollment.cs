using System;
using System.Collections.Generic;

namespace StudentCourseManagement.AppDbContextModels;

public partial class TblEnrollment
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int BatchId { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime? UpdatedDateTime { get; set; }

    public string? UpdatedUserId { get; set; }
}
