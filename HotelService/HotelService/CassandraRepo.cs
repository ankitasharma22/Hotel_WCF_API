using Cassandra;
using System;
using System.Collections.Generic;

namespace HotelService
{
    /// <summary>
    /// Implementing IRepository pattern
    /// </summary>
    public class CassandraRepo : IRepository
    {
        public List<Hotel> GetAllHotels()
        {
            var cluster = Cluster.Builder()
                                   .AddContactPoints("127.0.0.1")
                                   .Build();
            // Connect to the nodes using a keyspace
            var session = cluster.Connect("hoteldb");
            var rs = session.Execute("SELECT * FROM hotel");
            List<Hotel> hotelList = new List<Hotel>();
            foreach (var row in rs)
            {
                int id = row.GetValue<int>("hotelid");
                string hotelname = row.GetValue<string>("hotelname");
                string hoteladdress = row.GetValue<string>("hoteladdress");
                string hotelcity = row.GetValue<string>("hotelcity");
                string hotelstate = row.GetValue<string>("hotelstate");
                int pincode = row.GetValue<int>("pincode");
                hotelList.Add(new Hotel { HotelID = id, HotelAddress = hoteladdress, HotelName = hotelname, PinCode = pincode });
            }
            return hotelList;
        }

        public List<Room> GetRoomsOfHotelId(string hotelidInString)
        {
            int hotelid = int.Parse(hotelidInString);
            var cluster = Cluster.Builder()
                                   .AddContactPoints("127.0.0.1")
                                   .Build();
            var session = cluster.Connect("hoteldb");
            List<Room> roomsList = new List<Room>();
            string query = "SELECT * FROM hoteldb.Rooms where hotelid = " + hotelid + " ALLOW FILTERING";
            var res = session.Execute(query);
            foreach (var row in res)
            {
                int HotelId = row.GetValue<int>("hotelid");
                string RoomType = row.GetValue<string>("roomtype");
                int NoOfRooms = row.GetValue<int>("noofrooms");
                int Price = row.GetValue<int>("price");
                roomsList.Add(new Room { HotelID = HotelId, RoomType = RoomType, RoomsAvailable = NoOfRooms, RoomPrice = Price });
            }
            return roomsList;
        }

        public string BookRoom(BookRoom bookRoom)
        {
            var cluster = Cluster.Builder()
                                   .AddContactPoints("127.0.0.1")
                                   .Build();
            var session = cluster.Connect("hoteldb");
            int hotelid = bookRoom.HotelId;
            List<Room> roomsList = GetRoomsOfHotelId(hotelid.ToString());
            string roomtype = bookRoom.RoomType;
            int roomsAvailable = roomsList.Find(x => x.HotelID == hotelid && x.RoomType == roomtype).RoomsAvailable;
            int roomsLeft = roomsAvailable - bookRoom.NumberOfRoomsToBeBooked;
            if (roomsLeft < 0)
                return "Number of rooms required are not available! Only " + roomsAvailable + " rooms available. Please try again!";
            try
            {
                string query = "UPDATE rooms SET noofrooms = " + roomsLeft + " WHERE hotelid = " + hotelid + " and roomtype = '" + roomtype + "';";
                var res = session.Execute(query);
                return "Room booked successfully!";
            }
            catch (Exception e)
            {
                return "Room could not be booked! Please try again with valid details! "+ e.StackTrace;
            }
        }
    }
}