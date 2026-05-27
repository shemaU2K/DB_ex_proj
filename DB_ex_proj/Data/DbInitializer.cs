using DB_ex_proj.Models;

namespace DB_ex_proj
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var aud1 = new Auditorium { RoomNumber = "101-A" };
            var aud2 = new Auditorium { RoomNumber = "202-B" };
            var aud3 = new Auditorium { RoomNumber = "Лабораторія 3" };
            context.Auditoriums.AddRange(aud1, aud2, aud3);
            context.SaveChanges();

            var group1 = new Group { Name = "CS-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud1.Id };
            var group2 = new Group { Name = "WEB-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud2.Id };
            context.Groups.AddRange(group1, group2);
            context.SaveChanges();

            var course1 = new Course { Title = "Основи C#", Description = "Базовий синтаксис", Duration = 40 };
            var course2 = new Course { Title = "ASP.NET Core", Description = "Створення веб-додатків", Duration = 60 };
            var course3 = new Course { Title = "Бази даних", Description = "SQL та PostgreSQL", Duration = 50 };
            context.Courses.AddRange(course1, course2, course3);
            context.SaveChanges();

            var prereq = new CoursePrerequisite { CourseId = course2.Id, PrerequisiteId = course1.Id };
            context.AddRange(prereq);

            var mod1 = new Module { CourseId = course1.Id, SectionName = "Типи даних та змінні", Hours = 10 };
            var mod2 = new Module { CourseId = course1.Id, SectionName = "ООП", Hours = 15 };
            var mod3 = new Module { CourseId = course2.Id, SectionName = "Паттерн MVC", Hours = 20 };
            context.Modules.AddRange(mod1, mod2, mod3);

            var teacher1 = new Teacher { FullName = "Олег Іваненко", Email = "oleg@test.com", Password = "123", Passport = "АА123456", Specialization = "Backend", Biography = "Senior .NET Developer" };
            var teacher2 = new Teacher { FullName = "Ірина Петренко", Email = "ira@test.com", Password = "123", Passport = "ВВ654321", Specialization = "Databases", Biography = "Адміністратор БД" };
            context.Teachers.AddRange(teacher1, teacher2);

            var student1 = new Student { FullName = "Анна Коваль", Email = "anna@test.com", Password = "123", Passport = "СС111111", EnrollmentDate = DateTime.UtcNow, Rating = 95.5, GroupId = group1.Id };
            var student2 = new Student { FullName = "Максим Сидоренко", Email = "max@test.com", Password = "123", Passport = "СС222222", EnrollmentDate = DateTime.UtcNow, Rating = 88.0, GroupId = group1.Id };
            var student3 = new Student { FullName = "Віктор Бондар", Email = "viktor@test.com", Password = "123", Passport = "СС333333", EnrollmentDate = DateTime.UtcNow, Rating = 74.0, GroupId = group2.Id };
            context.Students.AddRange(student1, student2, student3);
            context.SaveChanges();

            var assignment1 = new CourseAssignment { TeacherId = teacher1.Id, CourseId = course1.Id, GroupId = group1.Id };
            var assignment2 = new CourseAssignment { TeacherId = teacher2.Id, CourseId = course3.Id, GroupId = group2.Id };
            context.AddRange(assignment1, assignment2);

            var cert1 = new Certificate { CertificateCode = "CERT-2026-001", IssueDate = DateTime.UtcNow, StudentId = student1.Id, CourseId = course1.Id };
            var cert2 = new Certificate { CertificateCode = "CERT-2026-002", IssueDate = DateTime.UtcNow, StudentId = student2.Id, CourseId = course1.Id };
            context.Certificates.AddRange(cert1, cert2);

            context.SaveChanges();
        }
    }
}