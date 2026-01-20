using Microsoft.EntityFrameworkCore;
using MeetingRoomReservationAPI.Models;

namespace MeetingRoomReservationAPI.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
            : base(options)
        {
        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Lisätään muutama testihuone valmiiksi
            modelBuilder.Entity<MeetingRoom>().HasData(
                new MeetingRoom { Id = 1, Name = "Neukkari Havu" },
                new MeetingRoom { Id = 2, Name = "Studio Kivi" },
                new MeetingRoom { Id = 3, Name = "Auditorio Meri" }
            );
        }
    }
}