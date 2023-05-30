using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.DTOs;
using System.Text.Json;

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
            await _dishService.AddDishesAsync(
                "Cheesecake pasta French fries boiled potato salad steak cutlet burger " +
                "mushroom risotto bread Pizza Ramen Paella Moussaka Coleslaw Dashi ravioli " +
                "jiaozi Sashimi Yakiniku Tortilla croissant Tacos  Sushi tonkatsu Yakitori" +
                "gnocchi Ceviche Quesadilla Fajitas Guacamole hummus Tamal Burrito barbecue" +
                "Mac and cheese Biryani Pho carbonara nachos brownies Cheeseburger paella " +
                "wonton Quiche fried chicken gyoza kimchi cupcake doughnut Churrasco " +
                "Mochi onigiri Roti tiramisu Fondue Neapolitan pizza Milkshake Grilled cheese " +
                "Miso soup Dal apple pie sandwich Chicken Kyiv Olivier Potato Salad " +
                "Sauerkraut Soup Potato Pancakes Crepes Buckwheat Porridge " +
                "Garlic Pampushki jam ");
            dishes = _mapper.Map<ICollection<DishDTO>>(await _dishService.GetAllAsync());
        }

        return View(dishes);
    }


    [HttpPost("/GetDishes")]
    public async Task<IActionResult> GetDishList()
    {
        var json = Request.Headers["filter"];
        var filter = JsonSerializer.Deserialize<DishFilterDTO>(json);

        var dishes = await _dishService.GetAllAsync(_mapper.Map<DishFilterModel>(filter));

        return Ok(dishes);
    }
}
