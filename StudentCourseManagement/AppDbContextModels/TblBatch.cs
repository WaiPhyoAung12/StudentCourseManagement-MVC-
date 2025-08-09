using System;
using System.Collections.Generic;

namespace StudentCourseManagement.AppDbContextModels;

public partial class TblBatch
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Capacity { get; set; }

    public decimal CreditHour { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string CreatedUserId { get; set; } = null!;

    public DateTime? UpdatedDateTime { get; set; }

    public string? UpdatedUserId { get; set; }

    public bool? DeleteFlag { get; set; }
}
