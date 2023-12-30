using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class AddressService : BaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>, IAddressService
{
    IAddressRepo _addressRepo;
    IUserRepo _userRepo;

    public AddressService(IAddressRepo repo, IUserRepo userRepo, IMapper mapper) : base(repo, mapper)
    {
        _addressRepo = repo;
        _userRepo = userRepo;
    }

    public override async Task<AddressReadDTO> CreateOneAsync(Guid id, AddressCreateDTO createObject)
    {
        var record = _mapper.Map<AddressCreateDTO, Address>(createObject);
        record.UserId = id;
        var recordCreated = await _repo.CreateOneAsync(record);
        return _mapper.Map<Address, AddressReadDTO>(recordCreated);
    }

    public override async Task<AddressReadDTO> UpdateOneAsync(Guid id, AddressUpdateDTO updateObject)
    {
        var user = await _userRepo.GetByIdAsync(id);

        if (user is null)
        {
            throw CustomException.NotFoundException("user is not found");
        }
        if (user.Address is null)
        {
            throw CustomException.NotFoundException("User does not have any address");
        }
        var address = await _repo.GetByIdAsync(user.Address.Id);

        if (address is null)
        {
            throw CustomException.NotFoundException("No Address found");
        }
        Address record = _mapper.Map<AddressUpdateDTO, Address>(updateObject, address);
        var updatedAddress = await _repo.UpdateOneAsync(id, record);
        return _mapper.Map<Address, AddressReadDTO>(updatedAddress);
    }

    public override async Task<bool> DeleteByIdAsync(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);

        if (user is null)
        {
            throw CustomException.NotFoundException("user is not found");
        }
        if (user.Address is null)
        {
            throw CustomException.NotFoundException("User does not have any address");
        }
        var address = await _repo.GetByIdAsync(user.Address.Id);

        if (address is null)
        {
            throw CustomException.NotFoundException("No Address found");
        }
        return await _repo.DeleteByIdAsync(address);
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
