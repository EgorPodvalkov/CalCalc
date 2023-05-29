namespace DataAccessLayer.Filters;

public class DishFilter
{
    public string Search { get; set; } = "";
    public int? MinCalorie { get; set; }
    public int? MaxCalorie { get; set; }
}
