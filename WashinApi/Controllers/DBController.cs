using Microsoft.AspNetCore.Mvc;
using WashinApi;
//using MySql.Data.MySqlClient;

namespace WashinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DBController : ControllerBase
    {
        private readonly DB _databaseConnection;

        public DBController(DB databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        //[HttpGet("test-connection")]
        //public IActionResult TestConnection()
        //{
        //    try
        //    {
        //        using MySqlConnection connection = _databaseConnection.ConnectToDatabase();
        //        // Effectuez des opérations avec la connexion
        //        return Ok("Connexion réussie !");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erreur de connexion : {ex.Message}");
        //    }
        //}
    }
}