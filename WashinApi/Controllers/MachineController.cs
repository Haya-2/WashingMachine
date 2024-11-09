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
            var machine = _context.Machines.Find(machineId);
            if (machine == null)
            {
                return NotFound();
            }

            return machine;
        }

        // PUT: api/machine/5/updateStatus
        [HttpPut("{machineId}/updateStatus")]
        public IActionResult UpdateMachineStatus(int machineId, [FromBody] int? userId)
        {
            var machine = _context.Machines.Find(machineId);
            if (machine == null)
            {
                return NotFound();
            }

            machine.userId = userId;
            _context.SaveChanges();

            return NoContent();
        }
    }
}
