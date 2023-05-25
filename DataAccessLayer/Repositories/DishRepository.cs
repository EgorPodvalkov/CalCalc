using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class DishRepository : BaseRepository<ExampleDish>, IDishRepository
{
    public DishRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }

    public async Task<IQueryable<ExampleDish>> GetQueryable()
    {
        return _context.Set<ExampleDish>().Select(x => x);
    }
}
