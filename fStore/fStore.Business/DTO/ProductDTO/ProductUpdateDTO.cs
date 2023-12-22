namespace fStore.Business;

public class ProductUpdateDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public int? Inventory { get; set; }
    public Guid? CategoryId { get; set; }
}
