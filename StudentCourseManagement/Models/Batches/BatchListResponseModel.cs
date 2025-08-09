using StudentCourseManagement.Models.Courses;

namespace StudentCourseManagement.Models.Batches;

public class BatchListResponseModel
{
    public List<BatchModel> BatchModels { get; set; } = new();
    public int TotalCount { get; set; }
}
