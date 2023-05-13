using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task AddUser(UserModel userModel)
    {
        if (userModel.RegistrationDate == new DateTime())
            userModel.RegistrationDate = DateTime.Today;

        var user = _mapper.Map<User>(userModel);
        await _userRepository.CreateAsync(user);
    }

    public async Task<ICollection<UserModel>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<ICollection<UserModel>>(users);
    }

    public async Task<UserModel> GetUserById(int id)
    {
        var user = (await _userRepository.GetAllAsync()).First(x => x.Id == id);
        return _mapper.Map<UserModel>(user);
    }
}
