using DataAccessLayer.Entities;

namespace BusinessLogicLayer.Models;

public class DailyUserInfoModel
{
    public DateTime Date { get; set; }

    public decimal KCalorieReal { get; set; }
    public int? KCalorieGoal { get; set; }

    public User User { get; set; }
    public ICollection<DishModel>? Dishes { get; set;}
}
