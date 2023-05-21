namespace BusinessLogicLayer.Models;

public class DailyUserInfoModel
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public decimal KCalorieReal { get; set; }
    public int? KCalorieGoal { get; set; }
    public int UserId { get; set; }

    public UserModel User { get; set; }
    public ICollection<DishModel> EatenDishes { get; set; } = new List<DishModel>();
}
