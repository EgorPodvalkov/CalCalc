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

    public async Task<ICollection<DailyUserInfo>> GetAllAsync()
        => await _context.Set<DailyUserInfo>().Select(x => x).ToListAsync();

    public async Task<DailyUserInfo> GetAsync(int id)
        => await _context.Set<DailyUserInfo>().FirstAsync(x => x.Id == id);

    public async Task<ICollection<DailyUserInfo>> FindAsync(Func<DailyUserInfo, bool> predicate)
    {
        var entities = await GetAllAsync();
        return entities.Where(predicate).ToList();
    }

    public async Task CreateAsync(DailyUserInfo entity)
    {
        await _context.Set<DailyUserInfo>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DailyUserInfo entity)
    {
        _context.Set<DailyUserInfo>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        await DeleteAsync(entity);
    }

    public async Task DeleteAsync(DailyUserInfo entity)
    {
        _context.Set<DailyUserInfo>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
