namespace HotelSearchConsumer.Models
{
    /// <summary>
    /// properties as per hotel data from WCF(3rd party)
    /// </summary>
    public class HotelFromWCF
    {
        public int HotelID { get; set; } 
        public string HotelName { get; set; } 
        public string HotelAddress { get; set; } 
        public string HotelCity { get; set; } 
        public string HotelState { get; set; } 
        public int PinCode { get; set; }
    }
}