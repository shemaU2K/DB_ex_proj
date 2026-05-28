using Microsoft.AspNetCore.Mvc;
using DB_ex_proj.Models;

namespace DB_ex_proj.Controllers
{
    public class TeachersController : Controller
    {
        private readonly AppDbContext _context;

        public TeachersController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            ModelState.Remove("CourseAssignments");

            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(teacher);
        }
    }
}