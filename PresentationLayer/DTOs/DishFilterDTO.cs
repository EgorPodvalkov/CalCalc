namespace PresentationLayer.DTOs;

public class DishFilterDTO
{
    public string Search { get; set; } = "";
    public int? MinCalorie { get; set; } 
    public int? MaxCalorie { get; set; }
}
