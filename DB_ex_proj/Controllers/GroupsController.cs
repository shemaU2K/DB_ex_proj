using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DB_ex_proj.Models;

namespace DB_ex_proj.Controllers
{
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Auditoriums = new SelectList(_context.Auditoriums, "Id", "RoomNumber");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            ModelState.Remove("Auditorium");
            ModelState.Remove("Students");
            ModelState.Remove("CourseAssignments");

            if (ModelState.IsValid)
            {
                _context.Add(group);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Auditoriums = new SelectList(_context.Auditoriums, "Id", "RoomNumber", group.AuditoriumId);
            return View(group);
        }
    }
}