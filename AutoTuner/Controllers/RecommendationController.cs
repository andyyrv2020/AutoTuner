using AutoTuner.Data;
using AutoTuner.Services;
using AutoTuner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoTuner.Controllers;

[Authorize]
public class RecommendationController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IRecommendationService _recommendationService;
    private readonly UserManager<IdentityUser> _userManager;

    public RecommendationController(ApplicationDbContext context, IRecommendationService recommendationService, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _recommendationService = recommendationService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index(int? carId)
    {
        var userId = _userManager.GetUserId(User);
        var cars = await _context.Cars.Where(c => c.UserId == userId).ToListAsync();
        RecommendationResultViewModel? result = null;

        if (carId.HasValue)
        {
            result = await _recommendationService.GetRecommendationsAsync(carId.Value)
                     ?? await _recommendationService.GenerateRecommendationsAsync(carId.Value);
        }

        var model = new RecommendationPageViewModel
        {
            Cars = cars,
            SelectedCarId = carId,
            Result = result
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate(int carId)
    {
        var userId = _userManager.GetUserId(User);
        var carExists = await _context.Cars.AnyAsync(c => c.Id == carId && c.UserId == userId);
        if (!carExists)
        {
            return NotFound();
        }

        await _recommendationService.GenerateRecommendationsAsync(carId);
        return RedirectToAction(nameof(Index), new { carId });
    }
}
