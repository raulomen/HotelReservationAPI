namespace HotelReservationAPI.Core.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Room> Rooms { get; set; }

    }
}
