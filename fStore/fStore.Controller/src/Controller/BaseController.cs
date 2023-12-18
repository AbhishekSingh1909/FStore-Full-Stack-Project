using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]s")]
public class BaseController<T, TReadDTO, TCreateDTO, TUpdateDTO> : ControllerBase where T : BaseEntity
{
    protected IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> _baseServie;
    public BaseController(IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO> service)
    {
        _baseServie = service;
    }
    [Authorize(Policy = "Admin")]
    [HttpGet()] // users? limit =20&offset=0
    public async Task<ActionResult<IEnumerable<TReadDTO>>> GetAll([FromQuery] GetAllParams options)
    {
        return Ok(await _baseServie.GetAllAsync(options));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDTO>> GetById([FromRoute] Guid id)
    {
        return await _baseServie.GetByIdAsync(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost(Name = "Crerate")]
    public virtual async Task<ActionResult<TReadDTO>> CreateOne([FromBody] TCreateDTO userCreateDTO)
    {
        try
        {
            var user = await _baseServie.CreateOneAsync(userCreateDTO);
            return CreatedAtAction(nameof(CreateOne), user);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:Guid}")] // users/:userid
    public virtual async Task<ActionResult<bool>> DeleteById([FromRoute] Guid id)
    {
        return await _baseServie.DeleteByIdAsync(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] TUpdateDTO updateObject)
    {
        return await _baseServie.UpdateOneAsync(id, updateObject);
    }

}
