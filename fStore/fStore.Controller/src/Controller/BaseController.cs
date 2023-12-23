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

    [Authorize(Roles = "Admin")]
    [HttpGet()]
    public virtual async Task<ActionResult<IEnumerable<TReadDTO>>> GetAll([FromQuery] GetAllParams options)
    {
        var records = await _baseServie.GetAllAsync(options);
        return Ok(records);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDTO>> GetById([FromRoute] Guid id)
    {
        return Ok(await _baseServie.GetByIdAsync(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost()]
    public virtual async Task<ActionResult<TReadDTO>> CreateOne([FromBody] TCreateDTO createObject)
    {
        try
        {
            var result = await _baseServie.CreateOneAsync(new Guid(), createObject);
            return CreatedAtAction(nameof(CreateOne), result);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:Guid}")]
    public virtual async Task<ActionResult<bool>> DeleteById([FromRoute] Guid id)
    {
        return Ok(await _baseServie.DeleteByIdAsync(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:Guid}")]
    public virtual async Task<ActionResult<TReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] TUpdateDTO updateObject)
    {
        return Ok(await _baseServie.UpdateOneAsync(id, updateObject));
    }

}
