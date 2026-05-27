using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DB_ex_proj.Models;

public class StudentsController : Controller
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        ModelState.Remove("Group");
        ModelState.Remove("Certificates");

        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Groups = new SelectList(_context.Groups, "Id", "Name", student.GroupId);
        return View(student);
    }
}