using System.ComponentModel.DataAnnotations;

namespace AutoTuner.Models
{
    public enum DrivingStyle
    {
        [Display(Name = "Икономичен")]
        Eco = 1,

        [Display(Name = "Ежедневен")]
        Daily = 2,

        [Display(Name = "Спортен")]
        Sport = 3
    }
}
