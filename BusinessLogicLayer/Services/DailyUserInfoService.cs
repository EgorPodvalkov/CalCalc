using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class DailyUserInfoService : BaseService<DailyUserInfoModel, DailyUserInfo>, IDailyUserInfoService
{
    private readonly IDailyUserInfoRepository _dailyUserInfoRepository;
    private readonly IDishRepository _dishRepository;

    public DailyUserInfoService(
        IDailyUserInfoRepository dailyUserInfoRepository,
        IDishRepository dishRepository,
        IMapper mapper)
        : base(dailyUserInfoRepository, mapper)
    {
        _dailyUserInfoRepository = dailyUserInfoRepository;
        _dishRepository = dishRepository;
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

            foreach (var dish in dailyInfo.EatenDishes)
            {
                dish.ExampleDish = await _dishRepository.GetAsync(dish.ExampleDishId);
            }

            fullInfo.Add(_mapper.Map<DailyUserInfoModel>(dailyInfo));
        }
        return fullInfo;
    }

    public async Task ChangeTodayGoalAsync(int userId, int? kCaloryGoal)
    {
        if (kCaloryGoal <= 0)
            kCaloryGoal = null;

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

        var additionDish = todayInfo.EatenDishes.FirstOrDefault(x => x.ExampleDishId == dishId);

        if (additionDish == null)
        {
            todayInfo.EatenDishes.Add(new EatenDish()
            {
                DailyUserInfoId = todayInfo.Id,
                ExampleDishId = dishId,
            });
        }
        else
            additionDish.Quantity++;

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
        var dish = todayInfo.EatenDishes.ElementAt(dishIndex);

        // Decrementing Quantity
        if (todayInfo.EatenDishes.ElementAt(dishIndex).Quantity > 1)
            todayInfo.EatenDishes.ElementAt(dishIndex).Quantity--;
        // Deleting Dish
        else
            todayInfo.EatenDishes.Remove(dish);

        todayInfo.KCalorieReal -= dish.ExampleDish.KCalorie;

        // Updating
        await _dailyUserInfoRepository.UpdateAsync(todayInfo);
    }
}
