namespace DataAccessLayer.Entities;

public class Dish : BaseEntity
{
    public string Name { get; set; }
    public decimal KCalorie { get; set; }
    public string ServingSize { get; set; }

    public string TotalFat { get; set; }
    public string SaturatedFat { get; set; }
    public string Carbohydrates { get; set; }
    public string Protein { get; set; }

    public string? Recipe { get; set; }

    public ICollection<DailyUserInfo> DailyUsersInfo { get; set; } = new List<DailyUserInfo>();
}
