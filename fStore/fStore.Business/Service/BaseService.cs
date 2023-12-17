using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class BaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> : IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : BaseEntity
{
    protected IBaseRepo<T> _repo;
    protected IMapper _mapper;
    public BaseService(IBaseRepo<T> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public virtual async Task<TReadDTO> CreateOneAsync(TCreateDTO createObject)
    {
        var record = _mapper.Map<TCreateDTO, T>(createObject);
        var recordCreated = await _repo.CreateOneAsync(record);
        return _mapper.Map<T, TReadDTO>(recordCreated);
    }

    public virtual async Task<bool> DeleteByIdAsync(Guid Id)
    {
        var foundUser = await _repo.GetByIdAsync(Id);
        if (foundUser is null)
        {
            throw new Exception("User is not found");
        }
        return await _repo.DeleteByIdAsync(foundUser);
    }

    public virtual async Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllParams options)
    {
        var records = await _repo.GetAllAsync(options);
        Console.WriteLine("Records :{0}", records);
        return _mapper.Map<IEnumerable<T>, IEnumerable<TReadDTO>>(records);
    }

    public virtual async Task<TReadDTO> GetByIdAsync(Guid Id)
    {
        var user = await _repo.GetByIdAsync(Id);
        return _mapper.Map<T, TReadDTO>(user);
    }

    public virtual async Task<TReadDTO> UpdateOneAsync(Guid id, TUpdateDTO updateObject)
    {
        var user = _mapper.Map<TUpdateDTO, T>(updateObject);
        
        var updatedUser = await _repo.UpdateOneAsync(id, user);
        return _mapper.Map<T, TReadDTO>(updatedUser);
    }
}
