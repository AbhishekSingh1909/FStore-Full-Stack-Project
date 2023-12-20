using fStore.Business;
using fStore.Business.DTO;
using fStore.Core;
using Microsoft.AspNetCore.Mvc;

namespace fStore.Controller;
[ApiController]
[Route("api/v1/categories")]
public class CategoryController : BaseController<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{
    public CategoryController(ICategoryService service) : base(service)
    {
    }
}
