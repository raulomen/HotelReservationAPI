namespace HotelReservationAPI.Core.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string DestinationCity { get; set; }
        public List<Guest> Guests { get; set; }
        public EmergencyContact EmergencyContact { get; set; } 
        public bool IsConfirmed { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
