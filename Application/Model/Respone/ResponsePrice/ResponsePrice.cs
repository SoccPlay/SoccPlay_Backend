namespace Application.Model.Respone.ResponsePrice;

public class ResponsePrice
{
    public Guid PriceId { get; set; }
    public int StarTime { get; set; }
    public int EndTime { get; set; }
    public float Price1 { get; set; }
    public int Size { get; set; }
    public Guid LandLandId { get; set; }
}