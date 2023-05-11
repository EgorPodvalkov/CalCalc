using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class DailyUserInfoService : IDailyUserInfoService
{
    private readonly IDailyUserInfoRepository _dailyUserInfoRepository;

    public DailyUserInfoService(CalCalcContext context)
    {
        _dailyUserInfoRepository = new DailyUserInfoRepository(context);
    }

    public async Task<DailyUserInfoModel> CreateDailyUserInfo(int userId, DateTime date)
    {
        var dailyUserInfo = new DailyUserInfo()
        {
            Date = date,
            KCalorieReal = 0,
            UserId = userId,
        };
        await _dailyUserInfoRepository.CreateAsync(dailyUserInfo);
        return dailyUserInfo.ToModel();
    }

    public async Task<ICollection<DailyUserInfoModel>> GetUserInfo(int userId)
    {
        return (await _dailyUserInfoRepository
            .FindAsync(x => x.UserId == userId))
            .ToModelCollection();
    }

    public async Task<DailyUserInfoModel> GetDailyUserInfo(int userId, DateTime date)
    {
        var userInfo = await GetUserInfo(userId);

        var dailyUserInfo = userInfo.FirstOrDefault(x => x.Date == date) 
            ?? await CreateDailyUserInfo(userId, date);

        return dailyUserInfo;
    }

    public async Task<DailyUserInfoModel> GetTodayUserInfo(int userId)
    {
        return await GetDailyUserInfo(userId, DateTime.Today);
    }

    public async Task AddDishToUser(int userId, DishModel dish)
    {
        // Getting Info
        var todayUserInfo = await GetTodayUserInfo(userId);
        
        // Creating List if no Dishes
        if (todayUserInfo.Dishes == null)
            todayUserInfo.Dishes = new List<DishModel>();

        // Adding Dish
        todayUserInfo.Dishes.Add(dish);

        // Updating DB
        await _dailyUserInfoRepository.UpdateAsync(todayUserInfo.ToEntity());
    }

    public async Task<bool> RemoveDishFromUser(int userId, int dishIndex)
    {
        // Getting Info
        var todayUserInfo = await GetTodayUserInfo(userId);

        // Creating List if no Dishes
        if (todayUserInfo.Dishes == null)
            todayUserInfo.Dishes = new List<DishModel>();

        // False if bad index
        if (dishIndex >= todayUserInfo.Dishes.Count && dishIndex < 0)
            return false;

        // Removing Dish
        var list = todayUserInfo.Dishes.ToList();
        list.RemoveAt(dishIndex);
        todayUserInfo.Dishes = list;

        // Updating DB
        await _dailyUserInfoRepository.UpdateAsync(todayUserInfo.ToEntity());
        return true;
    }
}
