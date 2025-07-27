namespace StudentCourseManagement.Models.Enrollments
{
    public class EnrollmentModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int BatchId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
    }
}
