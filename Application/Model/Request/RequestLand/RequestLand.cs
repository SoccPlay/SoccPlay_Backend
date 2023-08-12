using System.ComponentModel.DataAnnotations;

namespace Application.Model.Request.RequestLand;

public class RequestLand
{
    [Required]
    public string NameLand { get; set; } 
    [Required]
    public string Title { get; set; }
    [Required]
    public string Policy { get; set; }
    [Required]
    public string Location { get; set; } 
    [Required]
    public string Description { get; set; }
    [Required]
    public Guid OwnerId { get; set; }
}