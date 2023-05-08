using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class UserRepository : IRepository<User>
{
    private CalCalcContext _context;

    public UserRepository(CalCalcContext calCalcContext)
        => _context = calCalcContext;

    public async Task<ICollection<User>> GetAllAsync()
        => await _context.Set<User>().Select(x => x).ToListAsync();

    public async Task<User> GetAsync(int id)
        => await _context.Set<User>().FirstAsync(x => x.Id == id);

    public async Task<ICollection<User>> FindAsync(Func<User, bool> predicate)
    {
        var entities = await GetAllAsync();
        return entities.Where(predicate).ToList();
    }

    public async Task CreateAsync(User entity)
    {
        await _context.Set<User>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        _context.Set<User>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        await DeleteAsync(entity);
    }

    public async Task DeleteAsync(User entity)
    {
        _context.Set<User>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
