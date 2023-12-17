using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller.src.Controller
{
    public class UserController : BaseController<User,UserReadDTO,UserCreateDTO,UserUpdateDTO>
    {
        private IUserService _userService;

        public UserController(IUserService service) : base(service)
        {
            _userService = service;
        }


        //[HttpGet(Name = "GetAllUsers")]
        //public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllUsers([FromQuery] GetAllParams options)
        //{
        //    return Ok( await _userService.GetAllAsync(options));
        //}

        //[HttpPost(Name = "CreateUser")]
        //public async Task<ActionResult<UserReadDTO>> CreateUser([FromBody] UserCreateDTO userCreateDTO)
        //{
        //    return CreatedAtAction(nameof(CreateUser), await _userService.CreateOneAsync(userCreateDTO));

        //}
        //[HttpPost(Name = "CreateUser")]
        //public override async Task<ActionResult<UserReadDTO>> CreateOneAsync([FromBody] UserCreateDTO userCreateDTO)
        //{
        //    var user = await _userService.CreateOneAsync(userCreateDTO);
        //    return CreatedAtAction(nameof(CreateOneAsync),user);
        //}
    }
}