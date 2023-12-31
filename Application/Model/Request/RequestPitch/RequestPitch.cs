using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestPitch;

public class RequestPitch
{
    [Required] public string Name { get; set; } = null!;

    [Required] public int Size { get; set; }

    [Required] public Guid LandId { get; set; }

    [Required] public Guid OwnerId { get; set; }
}