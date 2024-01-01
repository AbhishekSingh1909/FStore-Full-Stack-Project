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
        var query =  _data.AsNoTracking().Where(a => a.Country.ToLower().Contains(options.Search.ToLower()) || a.City.ToLower().Contains(options.Search.ToLower()) ||
        a.PostCode.ToLower().Contains(options.Search.ToLower()) || a.Street.ToLower().Contains(options.Search.ToLower())).AsQueryable();
        if (options.Limit > 0 && options.Offset >= 0)
        { 
            return await query.Skip(options.Offset).Take(options.Limit).ToListAsync();
        }
           return await query.ToListAsync();
    }

    public override async Task<Address> UpdateOneAsync(Guid id, Address updateObject)
    {

        _data.Update(updateObject);
        await _dbContext.SaveChangesAsync();
        return updateObject;
    }
}
