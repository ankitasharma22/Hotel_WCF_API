namespace HotelService
{
    /// <summary>
    /// This class contains fields/properties of 'booking' table, to maintain record of booked rooms
    /// Assumption: Only one user is accessing application, hence 'userId' is not being maintained.
    /// </summary>
    public class BookRoom
    {
        public int BookingId { get; set; }
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string RoomType { get; set; }
        public int NumberOfRoomsToBeBooked { get; set; }
        public int TotalPrice { get; set; }
    }
}