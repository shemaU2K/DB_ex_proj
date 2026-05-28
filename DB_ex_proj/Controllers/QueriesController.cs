using DB_ex_proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DB_ex_proj.Controllers
{
    public class QueriesController : Controller
    {
        private readonly AppDbContext _context;

        public QueriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Query1(string? groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return View(new List<Student>());
            }

            var students = await _context.Students
                .Include(s => s.Group)
                .Where(s => s.Group.Name.Contains(groupName))
                .ToListAsync();

            ViewBag.SearchParameter = groupName;
            return View(students);
        }

        public async Task<IActionResult> Query2(string? courseTitle)
        {
            if (string.IsNullOrEmpty(courseTitle))
            {
                return View(new List<Certificate>());
            }

            var certs = await _context.Certificates
                .Include(c => c.Course)
                .Include(c => c.Student)
                .Where(c => c.Course.Title.Contains(courseTitle))
                .ToListAsync();

            ViewBag.SearchParameter = courseTitle;
            return View(certs);
        }

        public async Task<IActionResult> Query3(int? minHours)
        {
            if (minHours == null)
            {
                return View(new List<Module>());
            }

            var modules = await _context.Modules
                .Include(m => m.Course)
                .Where(m => m.Hours >= minHours)
                .OrderBy(m => m.Hours)
                .ToListAsync();

            ViewBag.SearchParameter = minHours;
            return View(modules);
        }

        public async Task<IActionResult> Query4(string? roomNumber)
        {
            if (string.IsNullOrEmpty(roomNumber))
            {
                return View(new List<Group>());
            }

            var groups = await _context.Groups
                .Include(g => g.Auditorium)
                .Where(g => g.Auditorium.RoomNumber.Contains(roomNumber))
                .ToListAsync();

            ViewBag.SearchParameter = roomNumber;
            return View(groups);
        }

        public async Task<IActionResult> Query5(string? courseTitle)
        {
            if (string.IsNullOrEmpty(courseTitle))
            {
                return View(new List<CourseAssignment>());
            }

            var assignments = await _context.CourseAssignments
                .Include(ca => ca.Teacher)
                .Include(ca => ca.Course)
                .Include(ca => ca.Group)
                .Where(ca => ca.Course.Title.Contains(courseTitle))
                .ToListAsync();

            ViewBag.SearchParameter = courseTitle;
            return View(assignments);
        }

        public async Task<IActionResult> Multiple1(int? teacherId)
        {
            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName", teacherId);

            if (teacherId == null) return View(new List<Teacher>());

            var targetCourseIds = await _context.CourseAssignments
                .Where(ca => ca.TeacherId == teacherId)
                .Select(ca => ca.CourseId)
                .Distinct()
                .ToListAsync();

            if (!targetCourseIds.Any())
            {
                ViewBag.Message = "Цей викладач ще не читає жодного курсу.";
                return View(new List<Teacher>());
            }

            var similarTeachers = await _context.Teachers
                .Where(t => t.Id != teacherId)
                .Where(t =>
                    _context.CourseAssignments.Where(ca => ca.TeacherId == t.Id).Select(ca => ca.CourseId).Distinct().Count() == targetCourseIds.Count
                    &&
                    targetCourseIds.All(targetId =>
                        _context.CourseAssignments.Where(ca => ca.TeacherId == t.Id).Select(ca => ca.CourseId).Contains(targetId)
                    )
                )
                .ToListAsync();

            return View(similarTeachers);
        }

        public async Task<IActionResult> Multiple2(int? studentId)
        {
            ViewBag.Students = new SelectList(_context.Students, "Id", "FullName", studentId);

            if (studentId == null) return View(new List<Student>());

            var targetCertCourseIds = await _context.Certificates
                .Where(c => c.StudentId == studentId)
                .Select(c => c.CourseId)
                .Distinct()
                .ToListAsync();

            if (!targetCertCourseIds.Any())
            {
                ViewBag.Message = "Цей студент ще не отримав жодного сертифіката.";
                return View(new List<Student>());
            }

            var similarStudents = await _context.Students
                .Where(s => s.Id != studentId)
                .Where(s =>
                    _context.Certificates.Where(c => c.StudentId == s.Id).Select(c => c.CourseId).Distinct().Count() == targetCertCourseIds.Count
                    &&
                    targetCertCourseIds.All(targetId =>
                        _context.Certificates.Where(c => c.StudentId == s.Id).Select(c => c.CourseId).Contains(targetId)
                    )
                )
                .ToListAsync();

            return View(similarStudents);
        }
    }
}