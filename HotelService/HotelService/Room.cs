using System.Runtime.Serialization;

namespace HotelService
{
    [DataContract]
    public class Room
    {
        /// <summary>
        /// Fields/properties of Cassandra keyspace 'hoteldb', table 'rooms' for maintaining count of rooms as per its type and hotelId
        /// Hence, compound primary key: hotelId & roomType
        /// </summary>
        [DataMember]
        public int RoomTypeId { get; set; }
        [DataMember]
        public int HotelID { get; set; }
        [DataMember]
        public string RoomType { get; set; }
        [DataMember]
        public int RoomPrice { get; set; }
        [DataMember]
        public int RoomsAvailable { get; set; }
    }
}