using HotelReservationAPI.Core.Entities;
using HotelReservationAPI.Core.Interfaces;
using HotelReservationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservationAPI.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly EmailService _emailService;

        public ReservationController(IReservationRepository reservationRepository, EmailService emailService)
        {
            _reservationRepository = reservationRepository;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(int hotelId)
        {
            var reservations = await _reservationRepository.GetAllReservationsAsync(hotelId);
            if (reservations == null || reservations.Count == 0)
            {
                return NotFound("No reservations found for this hotel.");
            }

            return Ok(reservations);
        }

        [HttpGet("{reservationId}")]
        public async Task<ActionResult<Reservation>> GetReservation(int hotelId, int reservationId)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
            if (reservation == null || reservation.HotelId != hotelId)
            {
                return NotFound("Reservation not found.");
            }

            return Ok(reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservation>> CreateReservation(int hotelId, Reservation reservation)
        {
            if (reservation.HotelId != hotelId)
            {
                return BadRequest("Hotel ID in the request does not match the URL hotel ID.");
            }

            // Lógica para la reserva
            await _reservationRepository.CreateReservationAsync(reservation);

            // Enviar correo electrónico de confirmación

            // descomentar para que funcione, primero configurar las variables del servicio de Email

            //var subject = "Reservation Confirmation";
            //var body = $"Your reservation at hotel {reservation.HotelId} is confirmed from {reservation.CheckInDate.ToShortDateString()} to {reservation.CheckOutDate.ToShortDateString()}.";
            //await _emailService.SendEmailAsync(reservation.Guests[0].Email, subject, body);

            return CreatedAtAction(nameof(GetReservation), new { hotelId = hotelId, reservationId = reservation.Id }, reservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationRepository.DeleteReservationAsync(id);
            return NoContent();
        }
    }
}
