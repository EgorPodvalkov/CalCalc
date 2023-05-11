using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.DbStartUp;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(CalCalcContext context)
    {
        _dishRepository = new DishRepository(context);
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
