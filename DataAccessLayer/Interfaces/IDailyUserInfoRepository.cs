using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IDailyUserInfoRepository : IRepository<DailyUserInfo>
{
    Task<ICollection<DailyUserInfo>> GetAllUserInfoWithDishesAsync(int userId);
}
