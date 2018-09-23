namespace HotelSearchConsumer.Models
{
    /// <summary>
    /// properties of 'Room'
    /// </summary>
    public class Room
    { 
        public int HotelID { get; set; } 
        public string RoomType { get; set; } 
        public int RoomPrice { get; set; } 
        public int RoomsAvailable { get; set; } 
    }
}