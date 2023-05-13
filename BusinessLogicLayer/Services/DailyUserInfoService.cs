using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class DailyUserInfoService : IDailyUserInfoService
{
    private readonly IDailyUserInfoRepository _dailyUserInfoRepository;
    private readonly IMapper _mapper;

    public DailyUserInfoService(IDailyUserInfoRepository dailyUserInfoRepository, IMapper mapper)
    {
        _dailyUserInfoRepository = dailyUserInfoRepository;
        _mapper = mapper;
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
        return _mapper.Map<DailyUserInfoModel>(dailyUserInfo);
    }

    public async Task<ICollection<DailyUserInfoModel>> GetUserInfo(int userId)
    {
        var userInfo = await _dailyUserInfoRepository.FindAsync(x => x.UserId == userId);
        return _mapper.Map<ICollection<DailyUserInfoModel>>(userInfo);
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

    public async Task AddDishToUser(int userId, DishModel dishModel)
    {
        // Getting Info
        var todayUserInfoModel = await GetTodayUserInfo(userId);
        
        // Creating List if no Dishes
        if (todayUserInfoModel.Dishes == null)
            todayUserInfoModel.Dishes = new List<DishModel>();

        // Adding Dish
        todayUserInfoModel.Dishes.Add(dishModel);

        // Adding Calories
        todayUserInfoModel.KCalorieReal += dishModel.KCalorie;

        // Updating DB
        var todayUserInfo = _mapper.Map<DailyUserInfo>(todayUserInfoModel);
        await _dailyUserInfoRepository.UpdateAsync(todayUserInfo);
    }

    public async Task<bool> RemoveDishFromUser(int userId, int dishIndex)
    {
        // Getting Info
        var todayUserInfoModel = await GetTodayUserInfo(userId);

        // Creating List if no Dishes
        if (todayUserInfoModel.Dishes == null)
            todayUserInfoModel.Dishes = new List<DishModel>();

        // False if bad index
        if (dishIndex >= todayUserInfoModel.Dishes.Count || dishIndex < 0)
            return false;

        // Removing Dish
        var list = todayUserInfoModel.Dishes.ToList();
        var dishModel = list[dishIndex];
        list.RemoveAt(dishIndex);
        todayUserInfoModel.Dishes = list;

        // Removing Calories
        todayUserInfoModel.KCalorieReal += dishModel.KCalorie;

        // Updating DB
        var todayUserInfo = _mapper.Map<DailyUserInfo>(todayUserInfoModel);
        await _dailyUserInfoRepository.UpdateAsync(todayUserInfo);
        return true;
    }
}
