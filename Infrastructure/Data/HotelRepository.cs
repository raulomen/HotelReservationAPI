using HotelReservationAPI.Core.Entities;
using HotelReservationAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservationAPI.Infrastructure.Data
{
    public class HotelRepository :IHotelRepository
    {
        private readonly IMongoCollection<Hotel> _hotelCollection;

        public HotelRepository(IMongoClient client)
        {
            var database = client.GetDatabase("HotelManagement");
            _hotelCollection = database.GetCollection<Hotel>("Hotels");
        }

        public async Task CreateHotelAsync(Hotel hotel)
        {
            await _hotelCollection.InsertOneAsync(hotel);
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _hotelCollection.Find(h => h.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            await _hotelCollection.ReplaceOneAsync(h => h.Id == hotel.Id, hotel);
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            return await _hotelCollection.Find(h => true).ToListAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            await _hotelCollection.DeleteOneAsync(h => h.Id == id);
        }
    }
}
