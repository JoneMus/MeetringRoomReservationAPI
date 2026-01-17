using System.ComponentModel.DataAnnotations;

namespace MeetingRoomReservationAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int MeetingRoomId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Lisätään varaajan nimi, jotta API on realistisempi
        public string ReservedBy { get; set; } = string.Empty;

        // Navigaatio-ominaisuus takaisin huoneeseen
        public MeetingRoom? MeetingRoom { get; set; }
    }
}