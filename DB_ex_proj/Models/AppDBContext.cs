using Microsoft.EntityFrameworkCore;

namespace DB_ex_proj.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Auditorium> Auditoriums { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Student>().ToTable("Students");

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Passport).IsUnique();
            modelBuilder.Entity<Course>().HasIndex(c => c.Title).IsUnique();
            modelBuilder.Entity<Certificate>().HasIndex(c => c.CertificateCode).IsUnique();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Auditorium)
                .WithOne(a => a.Group)
                .HasForeignKey<Group>(g => g.AuditoriumId);

            modelBuilder.Entity<CoursePrerequisite>()
                .HasKey(cp => new { cp.CourseId, cp.PrerequisiteId });

            modelBuilder.Entity<CoursePrerequisite>()
                .HasOne(cp => cp.Course)
                .WithMany(c => c.Prerequisites)
                .HasForeignKey(cp => cp.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePrerequisite>()
                .HasOne(cp => cp.Prerequisite)
                .WithMany(c => c.ConsequentCourses)
                .HasForeignKey(cp => cp.PrerequisiteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(ca => new { ca.TeacherId, ca.CourseId, ca.GroupId });

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(ca => ca.Teacher)
                .WithMany(t => t.CourseAssignments)
                .HasForeignKey(ca => ca.TeacherId);

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(ca => ca.Course)
                .WithMany(c => c.CourseAssignments)
                .HasForeignKey(ca => ca.CourseId);

            modelBuilder.Entity<CourseAssignment>()
                .HasOne(ca => ca.Group)
                .WithMany(g => g.CourseAssignments)
                .HasForeignKey(ca => ca.GroupId);
        }
    }
}
