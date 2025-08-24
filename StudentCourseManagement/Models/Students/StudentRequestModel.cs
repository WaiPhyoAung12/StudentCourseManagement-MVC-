namespace StudentCourseManagement.Models.Students
{
    public class StudentRequestModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public int Gender { get; set; }

        public bool? DeleteFlag { get; set; }

    }
}
