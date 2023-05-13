using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public DishService(IDishRepository dishRepository, IMapper mapper)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
    }

    public async Task AddDish(DishModel dishModel)
    {
        var dish = _mapper.Map<Dish>(dishModel);
        await _dishRepository.CreateAsync(dish);
    }

    public async Task<ICollection<DishModel>> GetDishes()
    {
        var dishes = await _dishRepository.GetAllAsync();
        return _mapper.Map<ICollection<DishModel>>(dishes);
    }
}
