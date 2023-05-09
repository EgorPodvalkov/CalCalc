namespace DataAccessLayer.Entities;

public class DailyUserInfo : BaseEntity
{
    public DateTime Date { get; set; }

    public decimal KCalorieReal { get; set; }
    public int? KCalorieGoal { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Dish>? Dishes { get; set; }
}
