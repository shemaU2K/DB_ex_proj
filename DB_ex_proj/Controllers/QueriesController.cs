using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_ex_proj.Models;

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
    }
}