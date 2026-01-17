using MeetingRoomReservationAPI.Data;
using MeetingRoomReservationAPI.Models;
using MeetingRoomReservationAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MeetingRoomBookingApi.Tests
{
    public class BookingServiceTests
    {
        private BookingContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Uniikki kanta jokaiselle testille
                .Options;
            var databaseContext = new BookingContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldFail_IfTimesOverlap()
        {
            // Arrange
            var context = GetDatabaseContext();
            var service = new BookingService(context);
            var baseDate = new DateTime(2026, 6, 1, 10, 0, 0);

            // Luodaan olemassa oleva varaus klo 10:00 - 11:00
            context.Bookings.Add(new Booking { 
                MeetingRoomId = 1, 
                StartTime = baseDate, 
                EndTime = baseDate.AddHours(1) 
            });
            await context.SaveChangesAsync();

            // Yritetään varata päällekkäinen aika klo 10:30 - 11:30
            var overlappingBooking = new Booking
            {
                MeetingRoomId = 1,
                StartTime = baseDate.AddMinutes(30),
                EndTime = baseDate.AddHours(1.5),
                ReservedBy = "Testi"
            };

            // Act
            var result = await service.CreateBookingAsync(overlappingBooking);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Huone on jo varattu valitulle aikavälille.", result.Message);
        }

        [Fact]
        public async Task CreateBookingAsync_ShouldFail_IfStartTimeInPast()
        {
            // Arrange
            var context = GetDatabaseContext();
            var service = new BookingService(context);
            var pastBooking = new Booking
            {
                MeetingRoomId = 1,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(-1).AddHours(1),
                ReservedBy = "Menneisyyden Mies"
            };

            // Act
            var result = await service.CreateBookingAsync(pastBooking);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("menneisyyteen", result.Message);
        }
    }
}