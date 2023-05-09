using DataAccessLayer.DbStartUp;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(CalCalcContext calCalcContext) : base(calCalcContext) { }
}
