using Application.Model.Response.ResponseBooking;

namespace Application.Model.ResponseLand;

public class ResponseAllLandBooking
{
    public Guid LandId { get; set; }
    public string Location { get; set; } = null!;
    public string Name { get; set; } = null!;

    public List<ResponseBooking_v2>? List { get; set; }
}