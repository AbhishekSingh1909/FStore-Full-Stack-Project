using System.Security.Claims;
using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HttpGet("profile")]
    public async Task<ActionResult<AddressReadDTO>> GetUserAddress()
    {
        var id = GetUserId();
        return Ok(await _addressService.GetByIdAsync(id));
    }

    [Authorize]
    [HttpPost()]
    public async Task<ActionResult<AddressReadDTO>> CreateUserAddress([FromBody] AddressCreateDTO addressCreateDTO)
    {
        var authenticatedClaims = HttpContext.User;
        var value = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        addressCreateDTO.UserId = Guid.Parse(value);
        var address = await _addressService.CreateOneAsync(addressCreateDTO);
        return CreatedAtAction(nameof(CreateUserAddress), address);

    }

    [Authorize]
    [HttpDelete("{id:Guid}")] // users/:userid
    public virtual async Task<ActionResult<bool>> DeleteById([FromRoute] Guid id)
    {
        return Ok(await _addressService.DeleteByIdAsync(id));
    }

    [Authorize]
    [HttpPatch("{id:Guid}")]
    public virtual async Task<ActionResult<AddressReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
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
