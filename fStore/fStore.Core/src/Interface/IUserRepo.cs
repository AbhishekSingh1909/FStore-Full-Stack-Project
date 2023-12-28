namespace fStore.Core;
public interface IUserRepo : IBaseRepo<User>
{
    Task<User?> FindByEmailAsync(string email);
    Task<bool> IsEmailAvailableAsync(string email);
    Task<int> GetCountAsync();

}
