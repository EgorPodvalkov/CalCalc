using DataAccessLayer.Entities;
using DataAccessLayer.Filters;

namespace DataAccessLayer.Interfaces;

public interface IDishRepository : IRepository<ExampleDish>
{
    Task<IQueryable<ExampleDish>> GetDishesAsync(DishFilter filter);
}
