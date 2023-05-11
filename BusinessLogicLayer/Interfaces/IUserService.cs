using BusinessLogicLayer.Models;

namespace BusinessLogicLayer.Interfaces;

public interface IUserService
{
    Task AddUser(UserModel user);
    Task<ICollection<UserModel>> GetUsers();
    Task<UserModel> GetUserById(int id);
}
