using System.Security.Claims;
using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]s")]
public class UserController : BaseController<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    private IUserService _userService;

    public UserController(IUserService service) : base(service)
    {
        _userService = service;
    }

    [Authorize]
    [HttpGet("Profile")]
    public async Task<ActionResult<UserReadDTO>> GetUserProfile()
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var id = Guid.Parse(value);
        return Ok(await _baseServie.GetByIdAsync(id));
    }

    [AllowAnonymous]
    [HttpPost("Profile")]
    public async Task<ActionResult<UserReadDTO>> CreateUserProfile([FromBody] UserCreateDTO userCreateDTO)
    {
        var user = await _baseServie.CreateOneAsync(userCreateDTO);
        return CreatedAtAction(nameof(CreateUserProfile), user);
    }

    [Authorize]
    [HttpDelete("Profile")]
    public async Task<ActionResult<bool>> DeleteUserProfile()
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Guid.Parse(value);
        return Ok(await _baseServie.DeleteByIdAsync(id));
    }

    [Authorize]
    [HttpPatch("Profile")]
    public async Task<ActionResult<UserReadDTO>> UpdateUserProfile([FromBody] UserUpdateDTO updateObject)
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Guid.Parse(value);
        return Ok(await _baseServie.UpdateOneAsync(id, updateObject));
    }

    [Authorize]
    [HttpPatch("ChangePassword")]
    public async Task<ActionResult<bool>> ChangePassword([FromBody] UpdateUserPasswordDTO user)
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Guid.Parse(value);
        return Ok(await _userService.UpdatePasswordAsync(user.Password, id));
    }
}
