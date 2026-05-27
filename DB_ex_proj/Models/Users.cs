using System.Text.RegularExpressions;

namespace DB_ex_proj.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Passport { get; set; }
    }

    public class Teacher : User
    {
        public string Specialization { get; set; }
        public string Biography { get; set; } 

        public List<CourseAssignment> CourseAssignments { get; set; }
    }

    public class Student : User
    {
        public DateTime EnrollmentDate { get; set; }
        public double Rating { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public List<Certificate> Certificates { get; set; } 
    }
}
