using HotelSearchConsumer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http; 
using System.Threading.Tasks;
using System.Web.Http;

namespace HotelSearchConsumer.Controllers
{
    public class HotelController : ApiController
    {
        List<MergeHotel> HotelDetails = new List<MergeHotel>();

        //Fetching Data of Hotels from JSON
        public List<HotelFromJson> GetHotelListFromJSON()
        {
            try
            {
                List<HotelFromJson> itemsFromJson = new List<HotelFromJson>();
                using (StreamReader r = new StreamReader(@"C:\HotelStaticDetails"))
                {
                    string json = r.ReadToEnd();
                    itemsFromJson = JsonConvert.DeserializeObject<List<HotelFromJson>>(json);
                }
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromJson", "Success", "List of hotels from json fetched");
                return itemsFromJson;
            }
            catch (Exception e)
            {
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromJson", "Failure", e.StackTrace);
                return null;
            }
        }

        //Fetching Data of Hotels from WCF(3rd party), which is getting data from cassandra database.
        public async Task<List<HotelFromWCF>> GetHotelListFromWCF()
        {
            try
            {
                List<HotelFromWCF> itemsFromWCF = new List<HotelFromWCF>();
                var client = new HttpClient();
                var response = await client.GetAsync("http://localhost:61367/HotelFunctionalities.svc/Hotel");
                if (response.StatusCode == HttpStatusCode.OK)
                    itemsFromWCF = await response.Content.ReadAsAsync<List<HotelFromWCF>>();
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromWCF", "Success", "List of hotels from wcf fetched");
                return itemsFromWCF;
            }
            catch (Exception e)
            {
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromWCF", "Failure", e.StackTrace);
                return null;
            }
        }

        //Merging Data of Hotel(Json and Wcf) together
        [HttpGet]
        [Route("Hotel/MergeHotels")]
        public async Task<List<MergeHotel>> MergeData()
        {
            try
            {
                List<HotelFromJson> itemsFromJson = GetHotelListFromJSON();
                List<HotelFromWCF> itemsFromWCF = await GetHotelListFromWCF();
                MergeHotel dummyHotel = new MergeHotel();
                for (int i = 0; i < itemsFromJson.Count; i++)
                {
                    dummyHotel.HotelId = itemsFromJson[i].HotelId;
                    dummyHotel.HotelDescription = itemsFromJson[i].HotelDescription;
                    dummyHotel.HotelContactNumber = itemsFromJson[i].HotelContactNumber;
                    dummyHotel.HotelAmenities = itemsFromJson[i].HotelAmenities;
                    dummyHotel.HotelPolicy = itemsFromJson[i].HotelPolicy;
                    dummyHotel.HotelImageURL = itemsFromJson[i].HotelImageURL;
                    for (int j = 0; j < itemsFromWCF.Count; j++)
                    {
                        if (itemsFromWCF[j].HotelID == itemsFromJson[i].HotelId)
                        {
                            dummyHotel.HotelName = itemsFromWCF[i].HotelName;
                            dummyHotel.HotelAddress = itemsFromWCF[i].HotelAddress;
                            dummyHotel.HotelCity = itemsFromWCF[i].HotelCity;
                            dummyHotel.HotelState = itemsFromWCF[i].HotelState;
                            dummyHotel.PinCode = itemsFromWCF[i].PinCode;
                            break;
                        }
                    }
                    HotelDetails.Add(dummyHotel);
                }
                LogManager.Instance.LogEntryForRequest("HotelController / MergeData", "Success", "List of hotels merged");
                return HotelDetails;
            }
            catch (Exception e)
            {
                LogManager.Instance.LogEntryForRequest("HotelController / MergeData", "Failure", e.StackTrace);
                return null;
            }
        }

        //Getting Rooms of a hotel by HotelId
        [HttpGet]
        [Route("Hotel/GetRooms/{hotelId}")]
        public async Task<List<Room>> GetRoomsByHotelId([FromUri]int hotelId)
        {
            try
            {
                string hotelIdInString = hotelId.ToString();
                List<Room> items = new List<Room>();
                var client = new HttpClient();
                var response = await client.GetAsync("http://localhost:61367/HotelFunctionalities.svc/Room/" + hotelIdInString);
                if (response.StatusCode == HttpStatusCode.OK)
                    items = await response.Content.ReadAsAsync<List<Room>>();
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromWCF", "Success", "List of hotels from wcf fetched");
                return items;
            }
            catch (Exception e)
            {
                LogManager.Instance.LogEntryForRequest("HotelController / GetHotelListFromWCF", "Failure", e.StackTrace);
                return null;
            }
        }

        //Booking room into SQL database
        [HttpPost]
        [Route("BookHotel")]
        public void BookRoom([FromBody]JObject jsonFormatInput)
        {
            try
            {
                SqlConnectionEstablishment SqlConnection = new SqlConnectionEstablishment();
                SqlConnection connection = SqlConnection.EstablishConnection();
                BookRoom bookRoom = new BookRoom();
                List<HotelFromJson> dummyHotelFromJson = GetHotelListFromJSON();
                string HotelName = jsonFormatInput.GetValue("HotelName").ToString();
                bookRoom.HotelName = HotelName;
                int HotelId = dummyHotelFromJson.Find(x => x.HotelName == HotelName).HotelId;
                bookRoom.HotelId = HotelId;
                string RoomType = jsonFormatInput.GetValue("RoomType").ToString();
                bookRoom.RoomType = RoomType;
                int NumberOfRoomsToBeBooked = Convert.ToInt32(jsonFormatInput.GetValue("NumberOfRoomsToBeBooked"));
                bookRoom.NumberOfRoomsToBeBooked = NumberOfRoomsToBeBooked;
                int TotalPrice = Convert.ToInt32(jsonFormatInput.GetValue("Price"));
                bookRoom.TotalPrice = TotalPrice;
                jsonFormatInput["HotelId"] = HotelId;
                string query = "Insert into  HotelBooking (HotelId, HotelName, RoomType, NumberOfRoomsToBeBooked, TotalPrice) values (" + HotelId + ", '" + HotelName + "', '" + RoomType + "', " + NumberOfRoomsToBeBooked + ", " + TotalPrice + ")";
                SqlCommand myCommand = new SqlCommand(query, connection);
                myCommand.ExecuteNonQuery();
                LogManager.Instance.LogEntryForRequest("HotelController / BookROom", "Success", "Booking Details added to SQL");
                Put(bookRoom);
            }
            catch (Exception e)
            {
                LogManager.Instance.LogEntryForRequest("HotelController / BookROom", "Failure", e.StackTrace); 
            }
        }

        //calling WCF application
        [HttpPut]
        public async Task Put(BookRoom bookRoom)
        {
            LogManager.Instance.LogEntryForRequest("HotelController / Put", "Success", "Called WCF from API");
            string url = "http://localhost:61367/HotelFunctionalities.svc/BookRoom";
            HttpClient httpClient = new HttpClient();
            await httpClient.PutAsJsonAsync(url, bookRoom);
        }
    }
}
