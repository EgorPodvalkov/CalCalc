using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    Task AddUserAsync(UserModel user);
    Task<ICollection<UserModel>> GetUsersAsync();
    Task<UserModel> GetUserByIdAsync(int id);
    Task<UserModel> GetOrCreateUserByIpAsync(string ip);
}
