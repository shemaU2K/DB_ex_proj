using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DB_ex_proj.Models;

namespace DB_ex_proj.Controllers
{
    public class CourseAssignmentsController : Controller
    {
        private readonly AppDbContext _context;

        public CourseAssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName");
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Title");
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAssignment assignment)
        {
            ModelState.Remove("Teacher");
            ModelState.Remove("Course");
            ModelState.Remove("Group");

            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Teachers = new SelectList(_context.Teachers, "Id", "FullName", assignment.TeacherId);
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Title", assignment.CourseId);
            ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name", assignment.GroupId);
            return View(assignment);
        }
    }
}