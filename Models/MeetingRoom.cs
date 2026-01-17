namespace MeetingRoomReservationAPI.Models
{
    public class MeetingRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigaatio-ominaisuus: huoneen varaukset
        public List<Booking> Bookings { get; set; } = new();
    }
}