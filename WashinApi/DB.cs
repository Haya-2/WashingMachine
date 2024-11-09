//using MySql.Data.MySqlClient;
    
namespace WashinApi
{
    public class DB
    {
        private string connectionString = "server=127.0.0.1:3306;database=WashinApp;user=root;password=2020Epita!";

        //public MySqlConnection ConnectToDatabase()
        //{
        //    var connection = new MySqlConnection(connectionString);
        //    try
        //    {
        //        connection.Open();
        //        Console.WriteLine("Connection successful!");
        //        return connection;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        throw;
        //    }
        //}
    }
}

