namespace BusinessLogicLayer.Models;

public class DishModel
{
    public string Name { get; set; }
    public decimal KCalorie { get; set; }
    public string ServingSize { get; set; }

    public string TotalFat { get; set; }
    public string SaturatedFat { get; set; }
    public string Carbohydrates { get; set; }
    public string Protein { get; set; }

    public string? Recipe { get; set; }
}
