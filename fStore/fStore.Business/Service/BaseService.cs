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

    public virtual async Task<TReadDTO> CreateOneAsync(Guid id, TCreateDTO createObject)
    {
        var record = _mapper.Map<TCreateDTO, T>(createObject);
        var recordCreated = await _repo.CreateOneAsync(record);
        return _mapper.Map<T, TReadDTO>(recordCreated);
    }

    public virtual async Task<bool> DeleteByIdAsync(Guid id)
    {
        var record = await _repo.GetByIdAsync(id);
        if (record is null)
        {
            throw CustomException.NotFoundException("records not found");
        }
        return await _repo.DeleteByIdAsync(record);
    }

    public virtual async Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllParams options)
    {
        var records = await _repo.GetAllAsync(options);
        if (records is null)
        {
            throw CustomException.NotFoundException(string.Format($"records not found {nameof(records)}"));
        }
        var results = _mapper.Map<IEnumerable<T>, IEnumerable<TReadDTO>>(records);
        if (results is null)
        {
            throw CustomException.UnableToMap(string.Format($"Unable to Map {nameof(results)}"));
        }
        return results;
    }

    public virtual async Task<TReadDTO> GetByIdAsync(Guid Id)
    {
        var record = await _repo.GetByIdAsync(Id);
        if (record is null)
        {
            throw CustomException.NotFoundException(string.Format($"records not found"));
        }
        var result = _mapper.Map<T, TReadDTO>(record);
        if (result is null)
        {
            throw CustomException.UnableToMap(string.Format($"Unable to Map {nameof(result)}"));
        }
        return result;
    }

    public virtual async Task<TReadDTO> UpdateOneAsync(Guid id, TUpdateDTO updateObject)
    {
        T entity = await _repo.GetByIdAsync(id);
        if (entity is null)
        {
            throw CustomException.NotFoundException("entity not found");
        }
        T record = _mapper.Map<TUpdateDTO, T>(updateObject, entity);

        var updatedRecord = await _repo.UpdateOneAsync(id, record);
        return _mapper.Map<T, TReadDTO>(updatedRecord);
    }
}
