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

    public IEnumerable<User> GetAll()
        => _context.Users;

    public User Get(int id)
        => _context.Users.First(x => x.Id == id);

    public IEnumerable<User> Find(Func<User, bool> predicate)
        => _context.Users.Where(predicate);

    public void Create(User entity)
        => _context.Users.Add(entity);

    public void Update(User entity)
        => _context.Entry(entity).State = EntityState.Modified;
    public void Delete(int id)
    {
        var user = Get(id);
        if (user != null)
            _context.Users.Remove(user);
    }
}
