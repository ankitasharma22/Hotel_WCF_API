using System.Collections.Generic;

namespace HotelService
{ 
    public class HotelFunctionalities : IHotelFunctionality
    {
        CassandraRepo cassandra = new CassandraRepo();
        public List<Hotel> GetAllHotels()
        {
            return cassandra.GetAllHotels();
        }

        public List<Room> GetRoomsOfHotelId(string hotelidInString)
        {
            return cassandra.GetRoomsOfHotelId(hotelidInString);
        }

        public string BookRoom(BookRoom bookRoom)
        {
           return cassandra.BookRoom(bookRoom);
        }
    }
}
