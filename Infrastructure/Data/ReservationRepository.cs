using HotelReservationAPI.Core.Entities;
using HotelReservationAPI.Core.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationAPI.Infrastructure.Data
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationRepository(IMongoDatabase database)
        {
            _reservations = database.GetCollection<Reservation>("Reservations");
        }
        public async Task<List<Reservation>> GetAllReservationsAsync(int hotelId)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.HotelId, hotelId);
            return await _reservations.Find(filter).ToListAsync();
        }

        public async Task<Reservation> GetReservationByIdAsync(int reservationId)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, reservationId);
            return await _reservations.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _reservations.InsertOneAsync(reservation);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservations.DeleteOneAsync(h => h.Id == id);
        }
    }
}
