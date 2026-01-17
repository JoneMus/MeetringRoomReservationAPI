using MeetingRoomReservationAPI.Models;

namespace MeetingRoomReservationAPI.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId);
        Task<(bool Success, string Message, Booking? Booking)> CreateBookingAsync(Booking booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}