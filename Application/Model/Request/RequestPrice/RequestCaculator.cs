using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestPrice;

public class RequestCaculator
{
    [Required] public Guid LandId { get; set; }
    
    [Required] public int Size { get; set; }
    
    [Required] public DateTime StarTime { get; set; }

    [Required] public DateTime EndTime { get; set; }
}