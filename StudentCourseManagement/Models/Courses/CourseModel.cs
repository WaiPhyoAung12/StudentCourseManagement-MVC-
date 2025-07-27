namespace StudentCourseManagement.Models.Courses;

public class CourseModel
{
    public int Id { get; set; }
    public string CourseCode { get; set; }
    public string CourseTitle { get; set; }
    public string CourseDescription { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public string CreatedUserId { get; set; }
    public string UpdatedUserId { get; set; }
    public bool DeleteFlag { get; set; }
}
