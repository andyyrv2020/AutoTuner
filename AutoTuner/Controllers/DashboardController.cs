using AutoTuner.Data;
using AutoTuner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var cars = await _context.Cars.Where(c => c.UserId == userId).ToListAsync();
        var carIds = cars.Select(c => c.Id).ToList();

        var recommendations = await _context.Recommendations
            .Include(r => r.TuningPart)
            .Where(r => carIds.Contains(r.CarId))
            .ToListAsync();

        var history = await _context.PerformanceHistories
            .Where(h => carIds.Contains(h.CarId))
            .OrderBy(h => h.DateApplied)
            .ToListAsync();

        var costByCategory = recommendations
            .Where(r => r.TuningPart != null)
            .GroupBy(r => r.TuningPart!.Category)
            .ToDictionary(g => g.Key, g => g.Sum(r => r.TuningPart!.Cost));

        var efficiencyByCategory = recommendations
            .Where(r => r.TuningPart != null)
            .GroupBy(r => r.TuningPart!.Category)
            .ToDictionary(g => g.Key, g => Math.Round(g.Average(r => r.TuningPart!.EfficiencyImpact), 2));

        var totalBudget = recommendations
            .GroupBy(r => r.CarId)
            .Sum(g => g.First().TotalCost);

        var averageEfficiency = recommendations
            .Where(r => r.TuningPart != null)
            .Select(r => r.TuningPart!.EfficiencyImpact)
            .DefaultIfEmpty(0)
            .Average();

        var model = new DashboardViewModel
        {
            Cars = cars,
            Recommendations = recommendations,
            PerformanceHistory = history,
            CostByCategory = costByCategory,
            EfficiencyByCategory = efficiencyByCategory,
            TotalBudget = totalBudget,
            AverageEfficiency = Math.Round(averageEfficiency, 2)
        };

        return View(model);
    }
}
