namespace BusinessLogicLayer.Models;

public class ChartInfoModel
{
    public ICollection<string> Dates { get; set; } = new List<string>();
    public ICollection<int?> CalorieGoals { get; set; } = new List<int?>();
    public ICollection<decimal?> RealCalories { get; set; } = new List<decimal?>();
}
