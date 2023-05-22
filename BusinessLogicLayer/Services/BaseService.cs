using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BusinessLogicLayer.Services;

public class BaseService<TModel, TEntity> : IBaseService<TModel> 
    where TModel : class 
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    internal readonly IMapper _mapper;

    public BaseService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return _mapper.Map<ICollection<TModel>>(await _repository.GetAllAsync());
    }

    public async Task<TModel> GetByIdAsync(int id)
    {
        return _mapper.Map<TModel>(await _repository.GetAsync(id));
    }

    public virtual async Task CreateAsync(TModel model)
    {
        await _repository.CreateAsync(_mapper.Map<TEntity>(model));
    }

    public async Task UpdateAsync(TModel model)
    {
        await _repository.UpdateAsync(_mapper.Map<TEntity>(model));
    }

    public async Task DeleteAsync(TModel model)
    {
        await _repository.DeleteAsync(_mapper.Map<TEntity>(model));
    }
}
