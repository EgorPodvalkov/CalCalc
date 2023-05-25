namespace BusinessLogicLayer.Models;

public class DishFilterModel
{
    public string Search { get; set; } = "";
    public int? MinCalorie { get; set; }
    public int? MaxCalorie { get; set; }
}
