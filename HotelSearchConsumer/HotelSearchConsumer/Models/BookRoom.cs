namespace HotelSearchConsumer.Models
{
    /// <summary>
    /// Properties of 'HotelBooking' to maintain the record of room booked.
    /// Assumption: only one user is accessing the application, hence userId isn't stored
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