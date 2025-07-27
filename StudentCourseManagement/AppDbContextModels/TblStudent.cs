using System;
using System.Collections.Generic;

namespace StudentCourseManagement.AppDbContextModels;

public partial class TblStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? DeleteFlag { get; set; }
}
