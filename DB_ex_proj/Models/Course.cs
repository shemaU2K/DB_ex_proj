using System.Reflection;

namespace DB_ex_proj.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public List<Module> Modules { get; set; }
        public List<CoursePrerequisite> Prerequisites { get; set; }
        public List<CoursePrerequisite> ConsequentCourses { get; set; }

        public List<CourseAssignment> CourseAssignments { get; set; }
        public List<Certificate> Certificates { get; set; }
    }

    public class Module
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string SectionName { get; set; }
        public int Hours { get; set; }
    }

    public class CoursePrerequisite
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int PrerequisiteId { get; set; }
        public Course Prerequisite { get; set; }
    }
}
