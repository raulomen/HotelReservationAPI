using HotelReservationAPI.Core.Entities;
using MongoDB.Bson;

namespace HotelReservationAPI.Core.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync(int hotelId);
        Task<Reservation> GetReservationByIdAsync(int reservationId);
        Task CreateReservationAsync(Reservation reservation); 
        Task DeleteReservationAsync(int id);
    }
}
