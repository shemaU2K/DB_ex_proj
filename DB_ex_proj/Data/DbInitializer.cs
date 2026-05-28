using DB_ex_proj.Models;

namespace DB_ex_proj.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any()) return;

            var aud1 = new Auditorium { RoomNumber = "101-A" };
            var aud2 = new Auditorium { RoomNumber = "202-B" };
            var aud3 = new Auditorium { RoomNumber = "Лабораторія 3" };
            var aud4 = new Auditorium { RoomNumber = "Лекційна 1" };
            var aud5 = new Auditorium { RoomNumber = "404-IT" };
            context.Auditoriums.AddRange(aud1, aud2, aud3, aud4, aud5);
            context.SaveChanges();

            var g1 = new Group { Name = "CS-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud1.Id };
            var g2 = new Group { Name = "WEB-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud2.Id };
            var g3 = new Group { Name = "QA-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud3.Id };
            var g4 = new Group { Name = "DEV-2025", CreatedAt = DateTime.UtcNow, AuditoriumId = aud4.Id };
            var g5 = new Group { Name = "DS-2026", CreatedAt = DateTime.UtcNow, AuditoriumId = aud5.Id };
            context.Groups.AddRange(g1, g2, g3, g4, g5);
            context.SaveChanges();

            var c1 = new Course { Title = "Основи C#", Description = "Базовий синтаксис та ООП", Duration = 40 };
            var c2 = new Course { Title = "ASP.NET Core", Description = "Створення веб-додатків", Duration = 60 };
            var c3 = new Course { Title = "Бази даних", Description = "SQL та PostgreSQL", Duration = 50 };
            var c4 = new Course { Title = "Python для Data Science", Description = "Аналіз даних з Pandas", Duration = 45 };
            var c5 = new Course { Title = "Тестування ПЗ (QA)", Description = "Мануальне та автотестування", Duration = 35 };
            context.Courses.AddRange(c1, c2, c3, c4, c5);
            context.SaveChanges();

            context.Modules.AddRange(
                new Module { CourseId = c1.Id, SectionName = "Типи даних та змінні", Hours = 10 },
                new Module { CourseId = c1.Id, SectionName = "Класи та Інтерфейси", Hours = 15 },
                new Module { CourseId = c2.Id, SectionName = "Паттерн MVC", Hours = 20 },
                new Module { CourseId = c2.Id, SectionName = "Entity Framework Core", Hours = 25 },
                new Module { CourseId = c3.Id, SectionName = "Нормалізація БД", Hours = 10 },
                new Module { CourseId = c3.Id, SectionName = "Складні SQL запити", Hours = 15 },
                new Module { CourseId = c4.Id, SectionName = "Основи Python", Hours = 10 },
                new Module { CourseId = c4.Id, SectionName = "Робота з Pandas", Hours = 20 },
                new Module { CourseId = c5.Id, SectionName = "Теорія тестування", Hours = 10 },
                new Module { CourseId = c5.Id, SectionName = "Автоматизація (Selenium)", Hours = 15 }
            );

            var t1 = new Teacher { FullName = "Олег Іваненко", Email = "oleg@test.com", Password = "123", Passport = "АА111", Specialization = "Backend", Biography = "Senior .NET" };
            var t2 = new Teacher { FullName = "Марія Ткаченко", Email = "maria@test.com", Password = "123", Passport = "АА222", Specialization = "Backend", Biography = "Middle .NET" };
            var t3 = new Teacher { FullName = "Ігор Петренко", Email = "igor@test.com", Password = "123", Passport = "АА333", Specialization = "Databases", Biography = "DBA" };
            var t4 = new Teacher { FullName = "Олена Коваленко", Email = "olena@test.com", Password = "123", Passport = "АА444", Specialization = "QA", Biography = "QA Lead" };
            context.Teachers.AddRange(t1, t2, t3, t4);
            context.SaveChanges();

            var s1 = new Student { FullName = "Анна Коваль", Email = "anna@test.com", Password = "123", Passport = "СС111", EnrollmentDate = DateTime.UtcNow, Rating = 95.5, GroupId = g1.Id };
            var s2 = new Student { FullName = "Максим Сидоренко", Email = "max@test.com", Password = "123", Passport = "СС222", EnrollmentDate = DateTime.UtcNow, Rating = 88.0, GroupId = g1.Id };
            var s3 = new Student { FullName = "Віктор Бондар", Email = "viktor@test.com", Password = "123", Passport = "СС333", EnrollmentDate = DateTime.UtcNow, Rating = 74.0, GroupId = g2.Id };
            var s4 = new Student { FullName = "Софія Мельник", Email = "sofia@test.com", Password = "123", Passport = "СС444", EnrollmentDate = DateTime.UtcNow, Rating = 91.2, GroupId = g3.Id };
            var s5 = new Student { FullName = "Дмитро Лисенко", Email = "dima@test.com", Password = "123", Passport = "СС555", EnrollmentDate = DateTime.UtcNow, Rating = 82.4, GroupId = g4.Id };
            var s6 = new Student { FullName = "Катерина Руденко", Email = "katya@test.com", Password = "123", Passport = "СС666", EnrollmentDate = DateTime.UtcNow, Rating = 89.9, GroupId = g5.Id };
            context.Students.AddRange(s1, s2, s3, s4, s5, s6);
            context.SaveChanges();

            context.AddRange(
                new CourseAssignment { TeacherId = t1.Id, CourseId = c1.Id, GroupId = g1.Id },
                new CourseAssignment { TeacherId = t1.Id, CourseId = c2.Id, GroupId = g1.Id },
                new CourseAssignment { TeacherId = t2.Id, CourseId = c1.Id, GroupId = g2.Id },
                new CourseAssignment { TeacherId = t2.Id, CourseId = c2.Id, GroupId = g2.Id },
                new CourseAssignment { TeacherId = t3.Id, CourseId = c3.Id, GroupId = g2.Id },
                new CourseAssignment { TeacherId = t4.Id, CourseId = c5.Id, GroupId = g3.Id }
            );

            context.Certificates.AddRange(
                new Certificate { CertificateCode = "CERT-001", IssueDate = DateTime.UtcNow, StudentId = s1.Id, CourseId = c1.Id },
                new Certificate { CertificateCode = "CERT-002", IssueDate = DateTime.UtcNow, StudentId = s1.Id, CourseId = c3.Id },
                new Certificate { CertificateCode = "CERT-003", IssueDate = DateTime.UtcNow, StudentId = s2.Id, CourseId = c1.Id },
                new Certificate { CertificateCode = "CERT-004", IssueDate = DateTime.UtcNow, StudentId = s2.Id, CourseId = c3.Id },
                new Certificate { CertificateCode = "CERT-005", IssueDate = DateTime.UtcNow, StudentId = s3.Id, CourseId = c2.Id },
                new Certificate { CertificateCode = "CERT-006", IssueDate = DateTime.UtcNow, StudentId = s4.Id, CourseId = c5.Id }
            );

            context.SaveChanges();
        }
    }
}