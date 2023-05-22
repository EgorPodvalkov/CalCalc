using AutoMapper;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;

namespace PresentationLayer.Controllers;

public class DishController : Controller
{

    private readonly IDishService _dishService;
    private readonly IMapper _mapper;

    private readonly ILogger<TodayInfoController> _logger;

    public DishController(
            ILogger<TodayInfoController> logger,
            IDishService dishService,
            IMapper mapper)
    {
        _logger = logger;
        _dishService = dishService;
        _mapper = mapper;
    }

    public async Task<IActionResult> DishList()
    {
        var dishes = _mapper.Map<ICollection<DishDTO>>(await _dishService.GetAllAsync());
        
        if (dishes.Count == 0)
        {
            await _dishService.AddDishesAsync("Cheesecake pasta French fries boiled potato salad steak cutlet burger mushroom risotto bread");
            dishes = _mapper.Map<ICollection<DishDTO>>(await _dishService.GetAllAsync());
        }

        return View(dishes);
    }
}
