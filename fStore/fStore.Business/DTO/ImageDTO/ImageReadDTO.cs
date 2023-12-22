using fStore.Core;

namespace fStore.Business;

public class ImageReadDTO
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
    public Guid ProductId { get; set; }
}
