using System.ComponentModel.DataAnnotations;

namespace StudentCourseManagement.Models.Courses;

public class CourseRequestModel
{
    public int Id { get; set; }
    public string CourseCode { get; set; }

    public string CourseTitle { get; set; }

    public string CourseDescription { get; set; }
}
