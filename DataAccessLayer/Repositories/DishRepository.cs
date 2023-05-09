using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class DishRepository : BaseRepository<Dish>, IDishRepository
{
    public DishRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }
}
