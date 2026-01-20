using MeetingRoomReservationAPI.Data;
using MeetingRoomReservationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomReservationAPI.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;

        public BookingService(BookingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetBookingsByRoomAsync(int roomId)
        {
            return await _context.Bookings
                .Where(b => b.MeetingRoomId == roomId)
                .OrderBy(b => b.StartTime)
                .ToListAsync();
        }

        public async Task<(bool Success, string Name)> GetMeetingRoomName(int roomId)
        {
            // Tarkistetaan onko huone olemassa
            if (!await RoomExists(roomId))
            {
                return (false, string.Empty);
            }

            var name = await _context.MeetingRooms
            .Where(r => r.Id == roomId)
            .Select(r => r.Name)
            .FirstOrDefaultAsync();

            return (true ,name ?? "Huone");
        }

        public async Task<(bool Success, string Message, Booking? Booking)> CreateBookingAsync(Booking booking)
        {
            // Tarkistetaan onko huone olemassa
            if (!await RoomExists(booking.MeetingRoomId))
            {
                return (false, $"Huonetta ID:llä {booking.MeetingRoomId} ei ole olemassa.", null);
            }

            // 1. Sääntö: Aloitusaika on ennen lopetusaikaa
            if (booking.StartTime >= booking.EndTime)
                return (false, "Aloitusajan on oltava ennen lopetusaikaa.", null);

            // 2. Sääntö: Varaukset eivät saa olla menneisyydessä
            if (booking.StartTime < DateTime.Now)
                return (false, "Varausta ei voi tehdä menneisyyteen.", null);

            // 3. Sääntö: Varattavan huoneen numero ei voi olla negatiivinen tai 0
            if (booking.MeetingRoomId <= 0)
                return (false, "Huoneen numero ei voi olla negatiivinen tai 0", null);

            // 4. Sääntö: Päällekkäisyyksien tarkistus
            bool isOverlapping = await _context.Bookings.AnyAsync(b =>
                b.MeetingRoomId == booking.MeetingRoomId &&
                booking.StartTime < b.EndTime &&
                booking.EndTime > b.StartTime);

            if (isOverlapping)
                return (false, "Huone on jo varattu valitulle aikavälille.", null);

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return (true, "Varaus luotu onnistuneesti.", booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> RoomExists(int roomId)
        {
            return await _context.MeetingRooms.AnyAsync(r => r.Id == roomId);
        }
    }
}