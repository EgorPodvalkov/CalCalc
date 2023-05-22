using AutoMapper;
using BusinessLogicLayer.DeserializeModels;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BusinessLogicLayer.Services;

public class DishService : BaseService<DishModel, ExampleDish>, IDishService
{
    private readonly IConfiguration _configuration;

    public DishService(
        IDishRepository dishRepository, 
        IConfiguration configuration,
        IMapper mapper)
        : base(dishRepository, mapper)
    {
        _configuration = configuration;
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

        // Adding dishes if more then 0
        foreach (var dish in dishes)
        {
            await CreateAsync(_mapper.Map<DishModel>(dish));
        }
    }
}
