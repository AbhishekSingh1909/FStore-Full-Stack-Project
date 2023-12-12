using fStore.Business;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller.src.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost(Name ="CreateUser")]
        public ActionResult<UserReadDTO> CreateUser(UserCreateDTO userCreateDTO)
        {
            return CreatedAtAction(nameof(CreateUser),_userService.CreateUser(userCreateDTO));

        }
    }
}