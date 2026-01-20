using System.ComponentModel.DataAnnotations;
namespace MeetingRoomReservationAPI.Models.DTOs;
public class CreateBookingRequest
{
    [Required(ErrorMessage = "Huone-ID on pakollinen.")]
    [Range(1, int.MaxValue, ErrorMessage = "Valitse voimassa oleva huone.")]
    public int MeetingRoomId { get; set; }
    
    [Required(ErrorMessage = "Aloitusaika on pakollinen.")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "Lopetusaika on pakollinen.")]
    public DateTime EndTime { get; set; }

    [Required(ErrorMessage = "Varaajan nimi on pakollinen.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Varaajan nimen on oltava 2-100 merkkiä pitkä.")]
    public string ReservedBy { get; set; } = string.Empty;
}
