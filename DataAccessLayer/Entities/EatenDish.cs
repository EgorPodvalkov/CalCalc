namespace DataAccessLayer.Entities;

public class EatenDish : BaseEntity
{
    public int ExampleDishId { get; set; }
    public int DailyUserInfoId { get; set; }

    public int Quantity { get; set; } = 1;

    public virtual ExampleDish ExampleDish { get; set; }
    public virtual DailyUserInfo DailyUserInfo { get; set; }
}
