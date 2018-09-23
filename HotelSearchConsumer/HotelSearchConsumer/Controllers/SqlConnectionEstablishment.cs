using System.Data.SqlClient;

namespace HotelSearchConsumer.Controllers
{
    /// <summary>
    /// Establish connection with Sql Database
    /// </summary>
    public class SqlConnectionEstablishment
    {
        public SqlConnection EstablishConnection()
        {
            SqlConnection connection = new SqlConnection();
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "TAVDESK145";
                builder.UserID = "sa";
                builder.Password = "test123!@#";
                builder.InitialCatalog = "HotelBookingDB";
                // Connect to SQL 
                connection = new SqlConnection(builder.ConnectionString);
                connection.Open();
                LogManager.Instance.LogEntryForRequest("SqlConnectionEstablishment / EstablishConnection", "Success","SQl Connection Established Successfully!");
                return connection;
            }
            catch (SqlException e)
            {
                LogManager.Instance.LogEntryForRequest("SqlConnectionEstablishment / EstablishConnection", "Failure", e.StackTrace);
            }
            return connection;
        }
    }
}