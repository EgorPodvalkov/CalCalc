using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDishService
{
    Task AddDish(DishModel dish);
    Task<ICollection<DishModel>> GetDishes();
}
