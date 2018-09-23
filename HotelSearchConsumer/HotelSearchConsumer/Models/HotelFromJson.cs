namespace HotelSearchConsumer.Models
{
    /// <summary>
    /// Attributes/properties as per json data 
    /// </summary>
    public class HotelFromJson
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelDescription { get; set; }
        public int HotelContactNumber { get; set; }
        public string HotelAmenities { get; set; }
        public string HotelPolicy { get; set; }
        public string HotelImageURL { get; set; }
    }
}