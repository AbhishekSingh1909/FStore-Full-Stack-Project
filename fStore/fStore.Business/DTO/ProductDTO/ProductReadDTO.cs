namespace fStore.Business;

public class ProductReadDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public IEnumerable<ImageReadDTO>? Images { get; set; }
    public CategoryReadDTO Category { get; set; }
}
