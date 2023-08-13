using Domain.Entities;

namespace Application.Model.Respone.ResponseBooking;

public class ResponseBooking
{
    public Guid BookingId { get; set; }
    public float TotalPrice { get; set; }
    public DateTime DateBooking { get; set; }
    public string Note { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public List<ResponseSchedule.ResponseSchedule> Schedules { get; set; } = null!;
}