namespace PresentationLayer.DTOs;

public class DailyUserInfoDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public decimal KCalorieReal { get; set; }
    public int? KCalorieGoal { get; set; }
    public int UserId { get; set; }

    public UserDTO User { get; set; }
    public ICollection<DishDTO> EatenDishes { get; set; } = new List<DishDTO>();
}
