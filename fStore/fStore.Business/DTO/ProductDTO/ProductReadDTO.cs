namespace fStore.Business;

public class ProductReadDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public required Guid CategoryId { get; set; }
    public IEnumerable<string>? Images { get; set; }
    public CategoryReadDTO Category { get; set; }
}
