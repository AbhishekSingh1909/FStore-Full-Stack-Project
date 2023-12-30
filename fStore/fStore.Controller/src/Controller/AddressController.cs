using fStore.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]es")]
public class AddressController : ControllerBase
{
    IAddressService _addressService;
    public AddressController(IAddressService service)
    {
        _addressService = service;
    }

    [Authorize]
    [HttpGet()]
    public async Task<ActionResult<AddressReadDTO>> GetUserAddress()
    {
        var id = GetUserId();
        var address = await _addressService.GetAddreess(id);
        return Ok(address);
    }

    [Authorize]
    [HttpPost()]
    public async Task<ActionResult<AddressReadDTO>> CreateUserAddress([FromBody] AddressCreateDTO addressCreateDTO)
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(value);
        var address = await _addressService.CreateOneAsync(userId, addressCreateDTO);
        return CreatedAtAction(nameof(CreateUserAddress), address);

    }

    [Authorize]
    [HttpDelete()] // users/:userid
    public virtual async Task<ActionResult<bool>> DeleteById()
    {
        var id = GetUserId();
        return Ok(await _addressService.DeleteByIdAsync(id));
    }

    [Authorize]
    [HttpPatch()]
    public virtual async Task<ActionResult<AddressReadDTO>> UpdateOne([FromBody] AddressUpdateDTO updateObject)
    {
        var id = GetUserId();
        return Ok(await _addressService.UpdateOneAsync(id, updateObject));
    }

    private Guid GetUserId()
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var id = Guid.Parse(value);
        return id;
    }
}
