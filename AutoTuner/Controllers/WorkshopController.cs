using AutoTuner.Data;
using AutoTuner.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Controllers;

public class WorkshopController : Controller
{
    private readonly ApplicationDbContext _context;

    public WorkshopController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? specialization)
    {
        var query = _context.Workshops.AsQueryable();
        if (!string.IsNullOrWhiteSpace(specialization))
        {
            query = query.Where(w => w.Specialization != null && w.Specialization.Contains(specialization));
        }

        var workshops = await query.ToListAsync();
        ViewData["Specialization"] = specialization;
        return View(workshops);
    }
}
