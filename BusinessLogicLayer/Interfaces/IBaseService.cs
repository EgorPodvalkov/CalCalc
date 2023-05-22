namespace BusinessLogicLayer.Interfaces;

public interface IBaseService<TModel> where TModel : class
{
    Task<ICollection<TModel>> GetAllAsync();
    Task<TModel> GetByIdAsync(int id);
    Task CreateAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(TModel model);
}
