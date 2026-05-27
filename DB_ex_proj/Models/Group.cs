namespace DB_ex_proj.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public int AuditoriumId { get; set; }
        public Auditorium Auditorium { get; set; }

        public List<Student> Students { get; set; }
        public List<CourseAssignment> CourseAssignments { get; set; }
    }
}
