using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace HotelService
{  
    [ServiceContract]
    public interface IHotelFunctionality 
    {
        [OperationContract]
        [WebGet( UriTemplate = "/Hotel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Hotel> GetAllHotels();

        [OperationContract]
        [WebGet( UriTemplate = "/Room/{hotelid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<Room> GetRoomsOfHotelId(string hotelid);

        [OperationContract]
        [WebInvoke(UriTemplate = "/BookRoom", Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string BookRoom(BookRoom bookRoom);
    }
}
