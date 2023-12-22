namespace fStore.Business;

public class ProductCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public IEnumerable<ImageCreateDTO>? Images { get; set; }
    public Guid CategoryId { get; set; }

}
