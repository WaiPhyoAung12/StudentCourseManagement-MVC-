namespace StudentCourseManagement.Models.Courses;

public class CourseListResponseModel
{
    public List<CourseModel> CourseModels { get; set; } = new();
    public int TotalCount { get; set; }
}
