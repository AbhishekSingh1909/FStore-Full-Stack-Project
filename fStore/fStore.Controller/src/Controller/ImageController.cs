using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]s")]
public class ImageController : BaseController<Image, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    IImageService _service;
    public ImageController(IImageService service) : base(service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpGet()]
    public override async Task<ActionResult<IEnumerable<ImageReadDTO>>> GetAll([FromQuery] GetAllParams options)
    {
        return await base.GetAll(options);
    }

    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<ImageReadDTO>> GetById([FromRoute] Guid id)
    {
        return await base.GetById(id);
    }

    [AllowAnonymous]
    [HttpGet("product/{productId:Guid}")]
    public async Task<ActionResult<ImageReadDTO>> GetProductImages([FromRoute] Guid productId)
    {
        return Ok(await _service.GetImagesByProductId(productId));
    }

    public override async Task<ActionResult<ImageReadDTO>> CreateOne([FromBody] ImageCreateDTO createObject)
    {
        return await base.CreateOne(createObject);
    }


}
