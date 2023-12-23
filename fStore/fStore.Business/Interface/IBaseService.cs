using fStore.Core;

namespace fStore.Business;

public interface IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> where T : BaseEntity
{
    Task<IEnumerable<TReadDTO>> GetAllAsync(GetAllParams options);
    Task<TReadDTO> GetByIdAsync(Guid Id);
    Task<TReadDTO> CreateOneAsync(Guid Id, TCreateDTO createObject);
    Task<TReadDTO> UpdateOneAsync(Guid id, TUpdateDTO updateObject);
    Task<bool> DeleteByIdAsync(Guid Id);
}
