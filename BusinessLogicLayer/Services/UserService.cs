using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly DailyUserInfoRepository _dailyUserInfoRepository;

    public UserService(CalCalcContext context)
    {
        _userRepository = new UserRepository(context);
        _dailyUserInfoRepository = new DailyUserInfoRepository(context);
    }

    public async Task AddUser(User user)
    {
        await _userRepository.CreateAsync(user);
    }

    public async Task<ICollection<User>> GetUsers()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<ICollection<DailyUserInfo>> GetUserInfo(User user)
    {
        return await _dailyUserInfoRepository.FindAsync(x => x.User == user);
    }

    public async Task<DailyUserInfo?> GetUserInfo(User user, DateTime date)
    {
        var userInfo = await GetUserInfo(user);
        var dailyUserInfo = userInfo.FirstOrDefault(x => x.Date == date);
        if (dailyUserInfo == null)
        {
            dailyUserInfo = new DailyUserInfo()
            {
                Date = date,
                KCalorieReal = 0,
                User = user,
                Dishes = new List<Dish>()
            };
            await _dailyUserInfoRepository.CreateAsync(dailyUserInfo);
        }
        return dailyUserInfo;
    }

    public async Task<DailyUserInfo> GetTodayUserInfo(User user)
    {
        return await GetUserInfo(user, DateTime.Today);
    }

    public async Task AddDishToUser(User user, Dish dish)
    {
        var todayUserInfo = await GetTodayUserInfo(user);
        todayUserInfo.Dishes.Add(dish);
    }
}
