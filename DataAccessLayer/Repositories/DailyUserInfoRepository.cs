using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DailyUserInfoRepository : BaseRepository<DailyUserInfo>, IDailyUserInfoRepository
{
    public DailyUserInfoRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }

    public async Task<ICollection<DailyUserInfo>> GetAllUserInfoWithDishesAsync(int userId)
    {
        return await _context.Set<DailyUserInfo>()
            .Select(x => x)
            .Where(x => x.UserId == userId)
            .Include(x => x.EatenDishes)
            .ToListAsync();

    }
}
