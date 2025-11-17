using AutoTuner.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoTuner.Models;
using AutoTuner.Services;
using AutoTuner.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
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

    public async Task<IActionResult> Index(int? carId, [FromQuery] RecommendationRequestOptions? options)
    {
        options ??= new RecommendationRequestOptions();
        var userId = _userManager.GetUserId(User);
        var cars = await _context.Cars.Where(c => c.UserId == userId).ToListAsync();
        RecommendationResultViewModel? result = null;

        if (carId.HasValue)
        {
            result = await _recommendationService.GetRecommendationsAsync(carId.Value, options)
                     ?? await _recommendationService.GenerateRecommendationsAsync(carId.Value, options);
        }

        var model = new RecommendationPageViewModel
        {
            Cars = cars,
            SelectedCarId = carId,
            Result = result,
            Options = options,
            Goals = BuildGoalSelectList(options.Goal)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate(int carId, [FromForm] RecommendationRequestOptions options)
    {
        options ??= new RecommendationRequestOptions();
        var userId = _userManager.GetUserId(User);
        var carExists = await _context.Cars.AnyAsync(c => c.Id == carId && c.UserId == userId);
        if (!carExists)
        {
            return NotFound();
        }

        await _recommendationService.GenerateRecommendationsAsync(carId, options);

        var routeValues = new RouteValueDictionary
        {
            ["carId"] = carId,
            ["Options.Goal"] = options.Goal,
            ["Options.Budget"] = options.Budget,
            ["Options.IncludeSafetyParts"] = options.IncludeSafetyParts
        };

        return RedirectToAction(nameof(Index), routeValues);
    }

    private static IEnumerable<SelectListItem> BuildGoalSelectList(OptimizationGoal selected)
    {
        return Enum.GetValues<OptimizationGoal>()
            .Select(goal => new SelectListItem
            {
                Value = goal.ToString(),
                Text = goal switch
                {
                    OptimizationGoal.MaxPower => "Maximum power",
                    OptimizationGoal.MaxEfficiency => "Maximum efficiency",
                    OptimizationGoal.BudgetFocused => "Budget focused",
                    _ => "Balanced build"
                },
                Selected = goal == selected
            });
    }
}
