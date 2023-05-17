using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDishService
{
    Task AddDish(DishModel dish);
    Task AddDish(string query);
    Task<ICollection<DishModel>> GetDishes();
}
