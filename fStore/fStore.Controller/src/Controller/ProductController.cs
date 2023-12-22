using fStore.Business;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;

[ApiController]
[Route("api/v1/[controller]s")]
public class ProductController : BaseController<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
{
    public ProductController(IProductService service) : base(service)
    {
    }

    [AllowAnonymous]
    [HttpGet("{id:Guid}")]
    public override async Task<ActionResult<ProductReadDTO>> GetById([FromRoute] Guid id)
    {
        return await base.GetById(id);
    }

    [AllowAnonymous]
    [HttpGet()]
    public override async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAll([FromQuery] GetAllParams options)
    {
        return await base.GetAll(options);
    }
}
