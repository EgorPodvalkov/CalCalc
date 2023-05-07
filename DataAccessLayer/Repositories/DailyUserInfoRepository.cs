using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DailyUserInfoRepository : IRepository<DailyUserInfo>
{
    private CalCalcContext _context;

    public DailyUserInfoRepository(CalCalcContext calCalcContext)
        => _context = calCalcContext;

    public IEnumerable<DailyUserInfo> GetAll()
        => _context.DailyUsersInfo;

    public DailyUserInfo Get(int id)
        => _context.DailyUsersInfo.First(x => x.Id == id);

    public IEnumerable<DailyUserInfo> Find(Func<DailyUserInfo, bool> predicate)
        => _context.DailyUsersInfo.Where(predicate);

    public void Create(DailyUserInfo entity)
        => _context.DailyUsersInfo.Add(entity);

    public void Update(DailyUserInfo entity)
        => _context.Entry(entity).State = EntityState.Modified;
    public void Delete(int id)
    {
        var dailyUserInfo = Get(id);
        if (dailyUserInfo != null)
            _context.DailyUsersInfo.Remove(dailyUserInfo);
    }
}
