namespace StudentCourseManagement.Models.Batches;

public class BatchModel
{
    public int Id { get; set; }
    public string BatchName { get; set; }
    public string Description { get; set; }
    public decimal CreditHour { get; set; }
    public decimal Capacity { get; set; }
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
    public string CreatedUserId { get; set; }
    public string UpdatedUserId { get; set; }
    public bool DeleteFlag { get; set; }
}
