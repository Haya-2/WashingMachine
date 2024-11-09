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
            var residents = _context.Users
                .Where(u => u.Id_Building == Id_Building && !u.IsManager)
                .ToList();
            return residents.Any() ? residents : NotFound(); return residents.Any() ? residents : NotFound();
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
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var building = _context.Buildings.FirstOrDefault(b => b.Id == user.Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            if (!building.Queue.Any(u => u.Id == userId))
            {
                building.Queue.Add(user);
                _context.SaveData();
            }

            return Ok();
        }

        // DELETE: api/user/removeFromQueue/5
        [HttpDelete("removeFromQueue/{userId}")]
        public IActionResult RemoveFromQueue(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound();
            }

            var building = _context.Buildings.FirstOrDefault(b => b.Id == user.Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            building.Queue.RemoveAll(u => u.Id == userId);
            _context.SaveData();

            return Ok();
        }
    }
}
