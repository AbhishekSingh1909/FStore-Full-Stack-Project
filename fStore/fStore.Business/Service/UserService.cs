using AutoMapper;
using fStore.Core;

namespace fStore.Business;

public class UserService : IUserService
{

    private IUserRepo _userRepo;
    private IMapper _mapper;

    public UserService(IUserRepo userRepo, IMapper mapper)
    {
        _userRepo = userRepo;
        _mapper = mapper;
    }

    public UserReadDTO CreateUser(UserCreateDTO userCreateDto)
    {
        var result = _userRepo.CreateUser(_mapper.Map<UserCreateDTO, User>(userCreateDto));
        return _mapper.Map<User, UserReadDTO>(result);
    }

    public IEnumerable<UserReadDTO> GetAllUsers(GetAllParams options)
    {
        var users = _userRepo.GetAllUsers(options);
        return _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users);
    }

    public UserReadDTO GetUserById(Guid id)
    {
        var user = _userRepo.GetUserById(id);
        return _mapper.Map<User, UserReadDTO>(user);
    }
}
