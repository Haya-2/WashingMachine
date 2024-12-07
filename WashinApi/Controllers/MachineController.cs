using Microsoft.AspNetCore.Mvc;
using WashinApi.Data;
using WashinApi.Models;

namespace WashinApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachineController : ControllerBase
    {
        private readonly WashinContext _context;

        public MachineController(WashinContext context)
        {
            _context = context;
        }

        // GET: api/machine/5
        [HttpGet("{machineId}")]
        public ActionResult<Machine> GetMachine(int machineId)
        {
            var machine = _context.Machines.FirstOrDefault(m => m.Id == machineId);
            if (machine == null)
            {
                return NotFound();
            }

            return machine;
        }

        // PUT: api/machine/5/updateKeys
        [HttpPut("{machineId}/updateKeys")]
        public IActionResult UpdateMachineKey(int machineId, [FromBody] int? userId)
        {
            var machine = _context.Machines.FirstOrDefault(m => m.Id == machineId);
            if (machine == null)
            {
                return NotFound();
            }

            machine.UserId = userId;
            if (userId!=null)
            {
                var building = _context.Buildings.FirstOrDefault(m => m.Id == machine.Id_Building);
                building.Queue.RemoveAll(u => u.Id == userId);
            }
            _context.SaveData();

            return NoContent();
        }

        // PUT: api/machine/5/updateStatus
        [HttpPut("{machineId}/updateStatus")]
        public IActionResult UpdateMachineStatus(int machineId, [FromBody] bool status)
        {
            var machine = _context.Machines.FirstOrDefault(m => m.Id == machineId);
            if (machine == null)
            {
                return NotFound();
            }

            // Update the machine's status
            machine.IsWorking = status;

            // Save changes back to the JSON file
            _context.SaveData();

            return NoContent();
        }
    }
}
