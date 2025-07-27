namespace StudentCourseManagement.Models.Students;

public class StudentModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PhoneNo { get; set; }
    public char Gender { get; set; }
    public string Password { get; set; }
    public bool DeleteFlag { get; set; }
}
