using fStore.Core;

namespace fStore.Business;

public interface IAddressService : IBaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>
{
    Task<AddressReadDTO> GetAddreess(Guid id);
}

