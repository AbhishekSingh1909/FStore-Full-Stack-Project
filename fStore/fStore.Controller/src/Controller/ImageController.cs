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
        var images = await base.GetAll(options);

        return Ok(images.Result);
    }

    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<ImageReadDTO>> GetById([FromRoute] Guid id)
    {
        return Ok(await base.GetById(id));
    }

    [AllowAnonymous]
    [HttpGet("product/{productId:Guid}")]
    public async Task<ActionResult<ImageReadDTO>> GetProductImages([FromRoute] Guid productId)
    {
        return Ok(await _service.GetImagesByProductId(productId));
    }

    public override Task<ActionResult<ImageReadDTO>> CreateOne([FromBody] ImageCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }


}
