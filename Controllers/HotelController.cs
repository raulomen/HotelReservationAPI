using HotelReservationAPI.Core.Entities;
using HotelReservationAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotels()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();
            return Ok(hotels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }

        [HttpPost]
        public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
        {
            await _hotelRepository.CreateHotelAsync(hotel);
            return CreatedAtAction(nameof(GetHotel), new { id = hotel.Id }, hotel);
        }

        [HttpPost("{hotelId}/rooms")]
        public async Task<ActionResult<Room>> AddRoomToHotel(int hotelId, Room room)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return NotFound($"Hotel with id {hotelId} not found.");
            }

            room.HotelId = hotelId;

            hotel.Rooms.Add(room);

            await _hotelRepository.UpdateHotelAsync(hotel);

            return CreatedAtAction(nameof(GetHotel), new { id = hotelId }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            await _hotelRepository.UpdateHotelAsync(hotel);
            return NoContent();
        }

        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateRoom(int hotelId, int roomId, Room updatedRoom)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return NotFound($"Hotel with id {hotelId} not found.");
            }

            var room = hotel.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                return NotFound($"Room with id {roomId} not found in hotel {hotelId}.");
            }

            room.Type = updatedRoom.Type;
            room.BaseCost = updatedRoom.BaseCost;
            room.Taxes = updatedRoom.Taxes;
            room.IsAvailable = updatedRoom.IsAvailable;
            room.Location = updatedRoom.Location;

            // Guardar los cambios en la base de datos
            await _hotelRepository.UpdateHotelAsync(hotel);

            return NoContent();  // Indica que la actualización fue exitosa
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _hotelRepository.DeleteHotelAsync(id);
            return NoContent();
        }

        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> DeleteRoom(int hotelId, int roomId)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(hotelId);
            if (hotel == null)
            {
                return NotFound($"Hotel with id {hotelId} not found.");
            }

            var room = hotel.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                return NotFound($"Room with id {roomId} not found in hotel {hotelId}.");
            }

            hotel.Rooms.Remove(room);

            await _hotelRepository.UpdateHotelAsync(hotel);

            return NoContent();
        }

    }
}
