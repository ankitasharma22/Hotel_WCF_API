using System.Runtime.Serialization; 

namespace HotelService
{
    /// <summary>
    /// Contains properties/fields of Cassandra database/keyspace 'hoteldb', table 'hotel'
    /// Half of the Hotel details are in cassandra database, while some details(static details) are in json file
    /// </summary>
    [DataContract]
    public class Hotel
    {
        [DataMember]
        public int HotelID { get; set; }
        [DataMember]
        public string HotelName { get; set; }
        [DataMember]
        public string HotelAddress { get; set; }
        [DataMember]
        public string HotelCity { get; set; }
        [DataMember]
        public string HotelState { get; set; }
        [DataMember]
        public int PinCode { get; set; }
    }
}
