using DB_ex_proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DB_ex_proj.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;
        
        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            ModelState.Remove("Modules");
            ModelState.Remove("Prerequisites");
            ModelState.Remove("ConsequentCourses");
            ModelState.Remove("CourseAssignments");
            ModelState.Remove("Certificates");

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddModule([Bind("CourseId,SectionName,Hours")] Module module)
        {
            ModelState.Remove("Course");

            if (ModelState.IsValid)
            {
                _context.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = module.CourseId });
            }

            return RedirectToAction(nameof(Details), new { id = module.CourseId });
        }
    }
}