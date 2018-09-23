using System.Collections.Generic;

namespace HotelService
{
    interface IRepository
    {
        List<Hotel> GetAllHotels();
        List<Room> GetRoomsOfHotelId(string hotelid);
        string BookRoom(BookRoom bookRoom);
    }
}
