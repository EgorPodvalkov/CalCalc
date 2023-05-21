using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDishService
{
    Task AddDishAsync(DishModel dish);
    Task AddDishAsync(string query);
    Task<ICollection<DishModel>> GetDishesAsync();
}
