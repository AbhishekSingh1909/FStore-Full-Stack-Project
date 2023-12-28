namespace fStore.Business;

public class OrderProductCreateDTO
{
    public Guid ProductId { get; set; }
    public int Quntity { get; set; }
}

public class OrderProductReadDTO
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public ProductReadDTO Product { get; set; }
    public int Quntity { get; set; }
    public double Price { get; set; }
}
