using System.ComponentModel.DataAnnotations;
namespace MeetingRoomReservationAPI.Models.DTOs;
public class CreateBookingResponse
{
    public string MeetingRoomName { get; set; } = string.Empty;
    public IEnumerable<Booking> Bookings { get; set; } = [];
}
