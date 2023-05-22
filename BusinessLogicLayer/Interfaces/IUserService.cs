using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService : IBaseService<UserModel>
{
    Task<UserModel> GetOrCreateUserByIpAsync(string ip);
}
