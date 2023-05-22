using AutoMapper;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class UserService : BaseService<UserModel, User>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository, 
        IMapper mapper) 
        : base(userRepository, mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task CreateAsync(UserModel userModel)
    {
        if (userModel.RegistrationDate == new DateTime())
            userModel.RegistrationDate = DateTime.Today;

        var user = _mapper.Map<User>(userModel);
        await _userRepository.CreateAsync(user);
    }

    public async Task<UserModel> GetOrCreateUserByIpAsync(string ip)
    {
        // Getting User
        var user = await _userRepository.FindFirstOrDefaultAsync(x => x.Ip == ip);

        // Creating and Getting if not Exist
        if (user == null)
        {
            await CreateAsync(new UserModel() { Ip = ip });
            user = await _userRepository.FindFirstOrDefaultAsync(x => x.Ip == ip);
            if (user == null)
                throw new Exception($"Can`t create user with ip: {ip}");
        }

        return _mapper.Map<UserModel>(user);
    }
}
