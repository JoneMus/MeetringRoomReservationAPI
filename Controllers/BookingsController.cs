using Microsoft.AspNetCore.Mvc;
using MeetingRoomReservationAPI.Models;
using MeetingRoomReservationAPI.Services;
using MeetingRoomReservationAPI.Models.DTOs;

namespace MeetingRoomReservationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/bookings/room/1
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetRoomBookings(int roomId)
        {
            if (roomId > 0)
            {
                var getRoom = await _bookingService.GetMeetingRoomName(roomId);
                if (!getRoom.Success)
                {
                    return BadRequest($"Huonetta ID:llä {roomId} ei ole olemassa.");
                }

                var bookings = await _bookingService.GetBookingsByRoomAsync(roomId);
                var response = new CreateBookingResponse
                {
                    MeetingRoomName = getRoom.Name,
                    Bookings = bookings
                };
                return Ok(response);
            }

            return BadRequest("Room id can't be negative or 0");          
        }

        // POST: api/bookings
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
        {
            var booking = new Booking
            {
                MeetingRoomId = request.MeetingRoomId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                ReservedBy = request.ReservedBy
            };
            
            var result = await _bookingService.CreateBookingAsync(booking);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetRoomBookings), new { roomId = booking.MeetingRoomId }, result.Booking);
        }

        // DELETE: api/bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var success = await _bookingService.DeleteBookingAsync(id);

            if (!success)
            {
                return NotFound($"Varausta ID:llä {id} ei löytynyt.");
            }

            return NoContent();
        }
    }
}