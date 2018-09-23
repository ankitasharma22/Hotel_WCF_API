using Cassandra;

/// <summary>
/// Singleton class LogManager for the purpose of logging
/// Database: Cassandra, Keyspace: hoteldb, table: LogDetails
/// </summary> 
namespace HotelSearchConsumer.Controllers
{
    public class LogManager
    {
        public static LogManager _instance;

        public static LogManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LogManager();

                return _instance;
            }
        }

        public void LogEntryForRequest(string Request, string Response, string Comment)
        {
            var cluster = Cluster.Builder().AddContactPoints("127.0.0.1") .Build();
            var session = cluster.Connect("hoteldb");
            string query = "Insert into  LogDetails  (LogId, Request, Response, Comments, Time) values (uuid(),'" + Request + "', '" + Response + "', '" + Comment + "', dateof(now()))";
            var res = session.Execute(query);
        }
    }
}