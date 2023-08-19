namespace Application.Model.Request.RequestLand;

public class RequestFilter
{
    public string? location { get; set; }
    public int size { get; set; }
    public int rate { get; set; }
    public float min { get; set; }
    public float max { get; set; }
}