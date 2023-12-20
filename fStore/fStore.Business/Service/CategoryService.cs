using AutoMapper;
using fStore.Business.DTO;
using fStore.Core;

namespace fStore.Business;

public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
{
    public CategoryService(ICategoryRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }
}
