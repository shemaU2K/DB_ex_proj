namespace DB_ex_proj.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string CertificateCode { get; set; }
        public DateTime IssueDate { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

    public class CourseAssignment
    {
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
