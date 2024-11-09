using Microsoft.AspNetCore.Mvc;
using WashinApi.Data;
using WashinApi.Models;

namespace WashinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Controller actions will be defined here
        private readonly WashinContext _context;

        public UserController(WashinContext context)
        {
            _context = context;
        }

        // GET: api/user/residents/1
        [HttpGet("residents/{Id_Building}")]
        public ActionResult<IEnumerable<User>> GetResidents(int Id_Building)
        {
            var building = _context.Buildings.Find(Id_Building);

            if (building == null)
            {
                return NotFound();
            }

            var context = _context.Users.Where(u => u.Id_Building == Id_Building && !u.IsManager).ToList();
            return context;
        }

        // GET: api/user/residents/1
        [HttpGet("residents/{login}/{pwd}")]
        public ActionResult<User> GetLogin(string login, string pwd)
        {
            var user = _context.Users.Where(u => u.Login == login && u.Password == pwd).ToList();

            if (user == null)
            {
                return NotFound();
            }
            return user.First();
        }

        // POST: api/user/addToQueue
        [HttpPost("addToQueue")]
        public IActionResult AddToQueue([FromBody] int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            var building = _context.Buildings.Find(user.Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            // Ajout à la queue (on suppose que Building gère la queue)
            building.Queue.Add(user);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/user/removeFromQueue/5
        [HttpDelete("removeFromQueue/{userId}")]
        public IActionResult RemoveFromQueue(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            var building = _context.Buildings.Find(user.Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            building.Queue.Remove(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
