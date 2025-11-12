using AutoTuner.Data;
using AutoTuner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Controllers;

[Authorize]
public class CarController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CarController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // 🧭 Списък с коли
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var cars = await _context.Cars
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Brand)
            .ToListAsync();

        return View(cars);
    }

    // 🔍 Детайли за конкретна кола
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var userId = _userManager.GetUserId(User);
        var car = await _context.Cars
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (car == null)
            return NotFound();

        return View(car);
    }

    // ➕ Форма за добавяне
    public IActionResult Create()
    {
        return View(new Car { DrivingStyle = DrivingStyle.Daily });
    }

    // 🧩 Добавяне (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Car car)
    {
        // 🔹 Вземаме текущия потребител
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Challenge(); // ако не е логнат

        // 🔹 Автоматично задаваме UserId преди валидация
        car.UserId = user.Id;

        // 🔹 Изчистваме евентуални грешки по това поле
        ModelState.Remove(nameof(Car.UserId));

        if (!ModelState.IsValid)
            return View(car);

        _context.Add(car);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // ✏️ Редактиране
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var userId = _userManager.GetUserId(User);
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (car == null)
            return NotFound();

        return View(car);
    }

    // ✏️ Редактиране (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Car car)
    {
        if (id != car.Id)
            return NotFound();

        var userId = _userManager.GetUserId(User);
        car.UserId = userId!; // 🔹 отново присвояваме UserId

        // 🔹 премахваме евентуална грешка при валидация на UserId
        ModelState.Remove(nameof(Car.UserId));

        if (!ModelState.IsValid)
            return View(car);

        try
        {
            _context.Update(car);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarExists(car.Id, userId))
                return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // 🗑️ Изтриване
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var userId = _userManager.GetUserId(User);
        var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (car == null)
            return NotFound();

        return View(car);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = _userManager.GetUserId(User);
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        if (car == null)
            return NotFound();

        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool CarExists(int id, string? userId)
    {
        return _context.Cars.Any(e => e.Id == id && e.UserId == userId);
    }
}
