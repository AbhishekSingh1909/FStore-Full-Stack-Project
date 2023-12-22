using fStore.Core;
using Microsoft.EntityFrameworkCore;

namespace fStore.WEBAPI;

public class AddressRepo : BaseRepo<Address>, IAddressRepo
{
    public AddressRepo(DataBaseContext dataBaseContext) : base(dataBaseContext)
    {
    }

    public async Task<Address> GetAddreess(Guid id)
    {
        var address = await _data.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
        return address;
    }

    public override async Task<IEnumerable<Address>> GetAllAsync(GetAllParams options)
    {
        var address = await _data.AsNoTracking().Where(a => a.Country.Contains(options.Search) || a.City.Contains(options.Search) ||
        a.PostCode.Contains(options.Search) || a.Street.Contains(options.Search))
        .Skip(options.Offset).Take(options.Limit).ToListAsync();
        return address;
    }

    public override async Task<Address> UpdateOneAsync(Guid id, Address updateObject)
    {
        var userAddress = await _data.FindAsync(id);
        if (userAddress is null)
        {
            return null;
        }

        if (updateObject.Street is not null)
        {
            userAddress.Street = updateObject.Street;
        }
        if (updateObject.PostCode is not null)
        {
            userAddress.PostCode = updateObject.PostCode;
        }
        if (updateObject.City is not null)
        {
            userAddress.City = updateObject.City;
        }
        if (updateObject.Country is not null)
        {
            userAddress.Country = updateObject.Country;
        }
        _data.Update(userAddress);
        await _dbContext.SaveChangesAsync();
        return userAddress;
    }
}
