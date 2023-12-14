using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller.src.Controller
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(Name ="GetAllUsers")]
        public ActionResult<IEnumerable<UserReadDTO>> GetAllUsers([FromQuery] GetAllParams options)
        {
            return Ok(_userService.GetAllUsers(options));
        }
        
        [HttpPost(Name ="CreateUser")]
        public ActionResult<UserReadDTO> CreateUser(UserCreateDTO userCreateDTO)
        {
            return CreatedAtAction(nameof(CreateUser),_userService.CreateUser(userCreateDTO));

        }
    }
}