using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDishService : IBaseService<DishModel>
{
    Task AddDishesAsync(string query);
    Task<ICollection<DishModel>> GetAllAsync(DishFilterModel filter);
}
