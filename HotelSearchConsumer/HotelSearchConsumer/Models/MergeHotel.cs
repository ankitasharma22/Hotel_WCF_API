namespace HotelSearchConsumer.Models
{
    /// <summary>
    /// All the properties of hotel including Json and Wcf
    /// </summary>
    public class MergeHotel
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelCity { get; set; }
        public string HotelState { get; set; }
        public int PinCode { get; set; }
        public string HotelDescription { get; set; }
        public int HotelContactNumber { get; set; }
        public string HotelAmenities { get; set; }
        public string HotelPolicy { get; set; }
        public string HotelImageURL { get; set; }
    }
}