namespace fStore.Core;

public class GetAllParams
{
    public int Limit { get; set; } = 0;
    public int Offset { get; set; } = 0;
    public string Search { get; set; } = string.Empty;
}
