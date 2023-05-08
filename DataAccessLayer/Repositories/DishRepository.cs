using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class DishRepository : IRepository<Dish>
{
    private CalCalcContext _context;

    public DishRepository(CalCalcContext calCalcContext)
        => _context = calCalcContext;

    public async Task<ICollection<Dish>> GetAllAsync()
        => await _context.Set<Dish>().Select(x => x).ToListAsync();

    public async Task<Dish> GetAsync(int id)
        => await _context.Set<Dish>().FirstAsync(x => x.Id == id);

    public async Task<ICollection<Dish>> FindAsync(Func<Dish, bool> predicate)
    {
        var entities = await GetAllAsync();
        return entities.Where(predicate).ToList();
    }

    public async Task CreateAsync(Dish entity)
    {
        await _context.Set<Dish>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Dish entity)
    {
        _context.Set<Dish>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        await DeleteAsync(entity);
    }

    public async Task DeleteAsync(Dish entity)
    {
        _context.Set<Dish>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
