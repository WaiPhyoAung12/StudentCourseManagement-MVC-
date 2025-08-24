namespace StudentCourseManagement.Models.Batches;

public class BatchResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal CreditHour { get; set; }
    public decimal Capacity { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; }

}
