using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionApp.Models;

namespace GestionApp.Controllers
{
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class SensoresController : ControllerBase
    {
        private readonly GestionAppContext _context;

        public SensoresController(GestionAppContext context)
        {
            _context = context;
        }

        // GET: api/Sensores
        [HttpGet]
        public dynamic GetSensor()
        {
            return  _context.Sensor.Select(item=>new { item.SensorId,item.Nombre}).ToList();
        }

        // GET: api/Sensores/5
        [HttpGet("{id}")]
        public dynamic GetSensor(int id)
        {
            var sensor =  _context.Sensor.Find(id);

            if (sensor == null)
            {
                return NotFound();
            }

            return _context.Sensor.Where(s=>s.SensorId==id).Select(item=>new {item.SensorId,item.Nombre});
        }

        // PUT: api/Sensores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensor(int id, Sensor sensor)
        {
            if (id != sensor.SensorId)
            {
                return BadRequest();
            }

            _context.Entry(sensor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSensor", new { id = sensor.SensorId }, sensor);
        }

        // POST: api/Sensores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sensor>> PostSensor(Sensor sensor)
        {
            _context.Sensor.Add(sensor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSensor", new { id = sensor.SensorId }, sensor);
        }

        // DELETE: api/Sensores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensor(int id)
        {
            var sensor = await _context.Sensor.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }

            _context.Sensor.Remove(sensor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorExists(int id)
        {
            return _context.Sensor.Any(e => e.SensorId == id);
        }
    }
}
