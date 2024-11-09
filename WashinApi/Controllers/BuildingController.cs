using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WashinApi.Data;
using WashinApi.Models;

namespace WashinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly WashinContext _context;

        public BuildingController(WashinContext context)
        {
            _context = context;
        }

        // GET: api/building/1
        [HttpGet("{Id_Building}")]
        public ActionResult<Building> GetBuilding(int Id_Building)
        {
            var building = _context.Buildings.Find(Id_Building);

            return building == null ? NotFound() : building;
        }

        // GET: api/building/1/queue
        [HttpGet("{Id_Building}/queue")]
        public ActionResult<IEnumerable<User>> GetQueue(int Id_Building)
        {
            var building = _context.Buildings.Find(Id_Building);

            return building == null ? NotFound() : building.Queue.ToList();
        }

        // POST: api/building/1/addToQueue
        [HttpPost("{Id_Building}/addToQueue")]
        public IActionResult AddToQueue(int Id_Building, [FromBody] int userId)
        {
            var building = _context.Buildings.Find(Id_Building);
            var user = _context.Users.Find(userId);

            if (building == null || user == null)
            {
                return NotFound();
            }

            // Vérifie si l'utilisateur est déjà dans la file d'attente
            if (!building.Queue.Any(u => u.Id == userId))
            {
                building.Queue.Add(user);
                _context.SaveChanges();
            }

            return Ok();
        }

        // DELETE: api/building/1/removeFromQueue/5
        [HttpDelete("{Id_Building}/removeFromQueue/{userId}")]
        public IActionResult RemoveFromQueue(int Id_Building, int userId)
        {
            var building = _context.Buildings.Find(Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            var userInQueue = building.Queue.FirstOrDefault(u => u.Id == userId);
            if (userInQueue != null)
            {
                building.Queue.Remove(userInQueue);
                _context.SaveChanges();
            }

            return Ok();
        }

        // GET: api/building/1/queuePosition/5
        [HttpGet("{Id_Building}/queuePosition/{userId}")]
        public ActionResult<int> GetPositionInQueue(int Id_Building, int userId)
        {
            var building = _context.Buildings.Find(Id_Building);
            if (building == null)
            {
                return NotFound();
            }

            // Trouve la position de l'utilisateur dans la file d'attente
            var position = building.Queue.FindIndex(u => u.Id == userId);
            return position >= 0 ? position + 1 : NotFound();
        }

        [HttpGet("building/{Id_Building}/managers")]
        public async Task<IActionResult> GetManagers(int Id_Building)
        {
            var managers = await _context.Users
                .Where(u => u.IsManager == true && u.Id_Building == Id_Building)
                .ToListAsync();

            return Ok(managers);
        }

        // GET: api/building/1/residents

        [HttpGet("building/{Id_Building}/residents")]
        public async Task<IActionResult> GetResidents(int Id_Building)
        {
            var residents = await _context.Users
                .Where(u => u.IsManager == false && u.Id_Building == Id_Building)
                .ToListAsync();

            return Ok(residents);
        }

        // GET: api/building/1/residents

        [HttpGet("building/{Id_Building}/machines")]
        public async Task<IActionResult> GetMachines(int Id_Building)
        {
            var machines = await _context.Machines
                .Where(m => m.Id_Building == Id_Building)
                .ToListAsync();

            return Ok(machines);
        }
    }
}
