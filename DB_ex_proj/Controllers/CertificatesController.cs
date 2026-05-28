using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DB_ex_proj.Models;

namespace DB_ex_proj.Controllers
{
    public class CertificatesController : Controller
    {
        private readonly AppDbContext _context;

        public CertificatesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(_context.Students, "Id", "FullName");
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Certificate certificate)
        {
            ModelState.Remove("Student");
            ModelState.Remove("Course");

            certificate.IssueDate = DateTime.SpecifyKind(certificate.IssueDate, DateTimeKind.Utc);

            if (ModelState.IsValid)
            {
                _context.Add(certificate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Students = new SelectList(_context.Students, "Id", "FullName", certificate.StudentId);
            ViewBag.Courses = new SelectList(_context.Courses, "Id", "Title", certificate.CourseId);
            return View(certificate);
        }
    }
}