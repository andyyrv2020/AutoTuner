using AutoTuner.Data;
using AutoTuner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Controllers;

[Authorize]
public class TuningPartController : Controller
{
    private readonly ApplicationDbContext _context;

    public TuningPartController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var parts = await _context.TuningParts.ToListAsync();
        return View(parts);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var part = await _context.TuningParts.FirstOrDefaultAsync(m => m.Id == id);
        if (part == null)
        {
            return NotFound();
        }

        return View(part);
    }

    public IActionResult Create()
    {
        return View(new TuningPart());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TuningPart tuningPart)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tuningPart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tuningPart);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tuningPart = await _context.TuningParts.FindAsync(id);
        if (tuningPart == null)
        {
            return NotFound();
        }
        return View(tuningPart);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TuningPart tuningPart)
    {
        if (id != tuningPart.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(tuningPart);
        }

        try
        {
            _context.Update(tuningPart);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TuningPartExists(tuningPart.Id))
            {
                return NotFound();
            }
            throw;
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tuningPart = await _context.TuningParts.FirstOrDefaultAsync(m => m.Id == id);
        if (tuningPart == null)
        {
            return NotFound();
        }

        return View(tuningPart);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tuningPart = await _context.TuningParts.FindAsync(id);
        if (tuningPart == null)
        {
            return NotFound();
        }

        _context.TuningParts.Remove(tuningPart);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TuningPartExists(int id)
    {
        return _context.TuningParts.Any(e => e.Id == id);
    }
}
