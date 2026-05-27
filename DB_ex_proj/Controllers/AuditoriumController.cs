using Microsoft.AspNetCore.Mvc;
using DB_ex_proj.Models;

namespace DB_ex_proj.Controllers
{
    public class AuditoriumsController : Controller
    {
        private readonly AppDbContext _context;

        public AuditoriumsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Auditorium auditorium)
        {
            ModelState.Remove("Group");

            if (ModelState.IsValid)
            {
                _context.Add(auditorium);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(auditorium);
        }
    }
}