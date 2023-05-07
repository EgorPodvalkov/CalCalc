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

    public IEnumerable<Dish> GetAll()
        => _context.Dishes;

    public Dish Get(int id)
        => _context.Dishes.First(x => x.Id == id);

    public IEnumerable<Dish> Find(Func<Dish, bool> predicate)
        => _context.Dishes.Where(predicate);

    public void Create(Dish entity)
        => _context.Dishes.Add(entity);

    public void Update(Dish entity)
        => _context.Entry(entity).State = EntityState.Modified;
    public void Delete(int id)
    {
        var dish = Get(id);
        if (dish != null)
            _context.Dishes.Remove(dish);
    }
}
