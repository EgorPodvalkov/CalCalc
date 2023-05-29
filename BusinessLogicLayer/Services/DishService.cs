using AutoMapper;
using BusinessLogicLayer.DeserializeModels;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Filters;
using DataAccessLayer.Interfaces;
using Flurl;
using Flurl.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Text.Json;

namespace BusinessLogicLayer.Services;

public class DishService : BaseService<DishModel, ExampleDish>, IDishService
{
    private readonly IConfiguration _configuration;
    private readonly IDishRepository _dishRepository;

    public DishService(
        IDishRepository dishRepository, 
        IConfiguration configuration,
        IMapper mapper)
        : base(dishRepository, mapper)
    {
        _configuration = configuration;
        _dishRepository = dishRepository;
    }

    public async Task AddDishesAsync(string query)
    {
        // Getting JSON
        var json = await "https://api.calorieninjas.com/v1"
            .AppendPathSegment("nutrition")
            .SetQueryParams(new {query = query})
            .WithHeader("X-Api-Key", _configuration["CalorieNinjasApiKey"])
            .GetStringAsync();

        // Deserialize JSON to Obj
        var dishes = JsonSerializer.Deserialize<DishesDeserialized>(json)
            ?.Items ?? Array.Empty<DishDeserialized>();


        // Getting Culture for Making nice viewing Names of Dishes
        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        foreach (var dish in dishes)
        {
            // Making nice viewing Names of Dishes
            dish.Name = textInfo.ToTitleCase(dish.Name);
            
            // Adding Dishes
            await CreateAsync(_mapper.Map<DishModel>(dish));
        }
    }

    public async Task<ICollection<DishModel>> GetAllAsync(DishFilterModel filter)
    {
        var dishes = await _dishRepository.GetDishesAsync(_mapper.Map<DishFilter>(filter));

        return _mapper.Map<ICollection<DishModel>>(dishes);
    }
}
