using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task AddDish(DishModel dish)
    {
        await _dishRepository.CreateAsync(dish.ToEntity());
    }

    public async Task<ICollection<DishModel>> GetDishes()
    {
        return (await _dishRepository.GetAllAsync()).ToModelCollection();
    }
}
