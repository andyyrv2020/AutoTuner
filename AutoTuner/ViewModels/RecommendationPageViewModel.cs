using System.Collections.Generic;
using System.Linq;
using AutoTuner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoTuner.ViewModels;

public class RecommendationPageViewModel
{
    public List<Car> Cars { get; set; } = new();
    public int? SelectedCarId { get; set; }
    public RecommendationResultViewModel? Result { get; set; }
    public RecommendationRequestOptions Options { get; set; } = new();
    public IEnumerable<SelectListItem> Goals { get; set; } = Enumerable.Empty<SelectListItem>();
}
