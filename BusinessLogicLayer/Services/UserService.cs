using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.DbStartUp;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(CalCalcContext context)
    {
        _userRepository = new UserRepository(context);
    }

    public async Task AddUser(UserModel user)
    {
        await _userRepository.CreateAsync(user.ToEntity());
    }

    public async Task<ICollection<UserModel>> GetUsers()
    {
        return (await _userRepository.GetAllAsync()).ToModelCollection();
    }

    public async Task<UserModel> GetUserById(int id)
    {
        return (await _userRepository.GetAllAsync()).First(x => x.Id == id).ToModel();
    }
}
