using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task AddUser(UserModel user)
    {
        if (user.RegistrationDate == new DateTime())
            user.RegistrationDate = DateTime.Today;

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
