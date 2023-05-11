using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IDailyUserInfoService
{
    Task<DailyUserInfoModel> CreateDailyUserInfo(int userId, DateTime date);
    Task<ICollection<DailyUserInfoModel>> GetUserInfo(int userId);
    Task<DailyUserInfoModel> GetDailyUserInfo(int userId, DateTime date);
    Task<DailyUserInfoModel> GetTodayUserInfo(int userId);
    Task AddDishToUser(int userId, DishModel dish);
    Task<bool> RemoveDishFromUser(int userId, int dishIndex);
}
