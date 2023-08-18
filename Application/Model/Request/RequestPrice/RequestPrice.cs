using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestPrice;

public class RequestPrice
{
    [Required] public int StarTime { get; set; }

    [Required] public int EndTime { get; set; }

    [Required] public float Price1 { get; set; }

    [Required] public int Size { get; set; }

    [Required] public Guid LandLandId { get; set; }
}