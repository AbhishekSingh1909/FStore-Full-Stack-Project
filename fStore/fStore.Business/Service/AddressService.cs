using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class AddressService : BaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>, IAddressService
{
    IAddressRepo _addressRepo;

    public AddressService(IAddressRepo repo, IMapper mapper) : base(repo, mapper)
    {
        _addressRepo = repo;
    }

    public override async Task<AddressReadDTO> CreateOneAsync(Guid id, AddressCreateDTO createObject)
    {
        var record = _mapper.Map<AddressCreateDTO, Address>(createObject);
        record.UserId = id;
        var recordCreated = await _repo.CreateOneAsync(record);
        return _mapper.Map<Address, AddressReadDTO>(recordCreated);
    }

    public async Task<AddressReadDTO> GetAddreess(Guid id)
    {
        var address = await _addressRepo.GetAddreess(id);
        if (address is null)
        {
            throw CustomException.NotFoundException("User does not have any address");
        }
        return _mapper.Map<Address, AddressReadDTO>(address);
    }

}
