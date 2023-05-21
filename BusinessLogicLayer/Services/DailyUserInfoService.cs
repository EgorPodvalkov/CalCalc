using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class DailyUserInfoService : IDailyUserInfoService
{
    private readonly IDailyUserInfoRepository _dailyUserInfoRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IMapper _mapper;

    public DailyUserInfoService(
        IDailyUserInfoRepository dailyUserInfoRepository,
        IDishRepository dishRepository,
        IMapper mapper)
    {
        _dailyUserInfoRepository = dailyUserInfoRepository;
        _dishRepository = dishRepository;
        _mapper = mapper;
    }


    private async Task<DailyUserInfo> CreateUserInfoAsync(int userId, DateTime date, int? kCaloryGoal = null)
    {
        var dailyUserInfo = new DailyUserInfo()
        {
            Date = date,
            UserId = userId,
            KCalorieReal = 0,
            KCalorieGoal = kCaloryGoal
        };
        await _dailyUserInfoRepository.CreateAsync(dailyUserInfo);
        return dailyUserInfo;
    }

    public async Task<ICollection<DailyUserInfoModel>> GetUserInfoAsync(UserModel user) 
    {
        var info = await _dailyUserInfoRepository.GetAllUserInfoWithDishesAsync(user.Id);
        
        var fullInfo = new List<DailyUserInfoModel>();

        int? goal = null;
        for (var day = user.RegistrationDate; day <= DateTime.Today; day = day.AddDays(1))
        {
            var dailyInfo = info.FirstOrDefault(x => x.Date ==  day)
                ?? await CreateUserInfoAsync(user.Id, day, goal);

            goal = dailyInfo.KCalorieGoal;

            fullInfo.Add(_mapper.Map<DailyUserInfoModel>(dailyInfo));
        }
        return fullInfo;
    }

    public async Task ChangeTodayGoalAsync(int userId, int kCaloryGoal)
    {
        // Getting Today Info
        var todayInfo = (await _dailyUserInfoRepository.GetAllAsync())
            .Where(x => x.UserId == userId)
            .FirstOrDefault(x => x.Date == DateTime.Today);

        // Creating Today Info with right Goal if not Exist
        if (todayInfo == null)
        {
            await CreateUserInfoAsync(userId, DateTime.Today, kCaloryGoal);
        }
        // Updateting Existing Info
        else
        {
            todayInfo.KCalorieGoal = kCaloryGoal;
            await _dailyUserInfoRepository.UpdateAsync(todayInfo);
        }
    }

    public async Task AddDishAsync(int userId, int dishId)
    {
        // Getting Today Info
        var todayInfo = (await _dailyUserInfoRepository.GetAllUserInfoWithDishesAsync(userId))
            .FirstOrDefault(x => x.Date == DateTime.Today);

        // Creating Today Info if not Exist
        if (todayInfo == null)
        {
            await CreateUserInfoAsync(userId, DateTime.Today); 

            todayInfo = (await _dailyUserInfoRepository.GetAllUserInfoWithDishesAsync(userId))
            .First(x => x.Date == DateTime.Today);
        }

        // Adding Dish
        var dish = await _dishRepository.GetAsync(dishId);
        todayInfo.Dishes.Add(dish);
        todayInfo.KCalorieReal += dish.KCalorie;

        // Updating
        await _dailyUserInfoRepository.UpdateAsync(todayInfo);
    }
    public async Task RemoveDishAsync(int userId, int dishIndex)
    {
        // Getting Today Info
        var todayInfo = (await _dailyUserInfoRepository.GetAllUserInfoWithDishesAsync(userId))
            .FirstOrDefault(x => x.Date == DateTime.Today);

        // Creating Today Info if not Exist
        if (todayInfo == null)
        {
            await CreateUserInfoAsync(userId, DateTime.Today);

            todayInfo = (await _dailyUserInfoRepository.GetAllUserInfoWithDishesAsync(userId))
            .First(x => x.Date == DateTime.Today);
        }

        // Adding Dish
        var dish = todayInfo.Dishes.ElementAt(dishIndex);
        todayInfo.Dishes.Remove(dish);
        todayInfo.KCalorieReal -= dish.KCalorie;

        // Updating
        await _dailyUserInfoRepository.UpdateAsync(todayInfo);
    }
}
