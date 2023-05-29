using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Filters;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class DishRepository : BaseRepository<ExampleDish>, IDishRepository
{
    public DishRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }

    public async Task<IQueryable<ExampleDish>> GetDishesAsync(DishFilter filter)
    {
        var dishes = _context.Set<ExampleDish>().Select(x => x);
        dishes = dishes.Where(x => x.Name.Contains(filter.Search));

        if (filter.MinCalorie != null)
            dishes= dishes.Where(x => x.KCalorie >= filter.MinCalorie);

        if (filter.MaxCalorie != null)
            dishes = dishes.Where(x => x.KCalorie <= filter.MaxCalorie);

        return dishes;
    }
}
