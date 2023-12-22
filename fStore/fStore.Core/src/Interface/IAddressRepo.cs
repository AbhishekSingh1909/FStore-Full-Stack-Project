namespace fStore.Core;

public interface IAddressRepo : IBaseRepo<Address>
{
    Task<Address> GetAddreess(Guid id);
}
