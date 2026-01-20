namespace MeetingRoomReservationAPI.Models
{
    public class MeetingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Booking> Bookings { get; set; } = new();
    }
}