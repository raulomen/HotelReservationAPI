using HotelReservationAPI.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelReservationAPI.Core.Interfaces
{
    public interface IHotelRepository
    {
        Task CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelByIdAsync(int id);
        Task UpdateHotelAsync(Hotel hotel);
        Task<List<Hotel>> GetAllHotelsAsync();
        Task DeleteHotelAsync(int id);
    }
}
