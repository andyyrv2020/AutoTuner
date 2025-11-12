using AutoTuner.Models;

namespace AutoTuner.ViewModels;

public class RecommendationPageViewModel
{
    public List<Car> Cars { get; set; } = new();
    public int? SelectedCarId { get; set; }
    public RecommendationResultViewModel? Result { get; set; }
}
