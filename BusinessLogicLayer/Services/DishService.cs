using AutoMapper;
using BusinessLogicLayer.DeserializeModels;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Text.Json;

namespace BusinessLogicLayer.Services;

public class DishService : IDishService
{
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public DishService(IDishRepository dishRepository, IMapper mapper, IConfiguration configuration)
    {
        _dishRepository = dishRepository;
        _mapper = mapper;
        _configuration = configuration;
    }

    public async Task AddDish(DishModel dishModel)
    {
        var dish = _mapper.Map<Dish>(dishModel);
        await _dishRepository.CreateAsync(dish);
    }

    public async Task AddDish(string query)
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
            await AddDish(_mapper.Map<DishModel>(dish));
        }
    }

    public async Task<ICollection<DishModel>> GetDishes()
    {
        var dishes = await _dishRepository.GetAllAsync();
        return _mapper.Map<ICollection<DishModel>>(dishes);
    }
}
