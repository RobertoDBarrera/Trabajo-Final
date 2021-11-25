using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionApp.Models;
using System.Collections.ObjectModel;

namespace GestionApp.Controllers
{
    [Route("api/1.0/[controller]")]
    [ApiController]
    public class TelefonosController : ControllerBase
    {
        private readonly GestionAppContext _context;

        public TelefonosController(GestionAppContext context)
        {
            _context = context;
        }

        // GET: api/Telefonos

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetTelefono()
        {
            return await _context.Telefono.ToListAsync();
        }

        //GET: api/Telefonos/buscar
        
        [HttpGet("buscar")]
        public dynamic Buscar()
        {
            return  _context.Telefono
                .Select(item=> new 
                { 
                    item.Marca,
                    item.Modelo,
                    item.Sensores

                }).ToList();

        }

        
        [HttpGet("telxid")]
        public dynamic Telxid(int id)
        {
            var Tel = _context.Telefono.Find(id);
            
            foreach (var obj in Tel.Instalaciones  )
            {

            }
            return _context.Telefono
                .Where(item => item.TelefonoId == id)
                .Select(item => new
                {   
                    Marca = item.Marca,
                    Modelo = item.Modelo,
                                        
                    //Instalacion Instalaciones = item.Instalaciones
                    
                })
                .ToList(); 

        }
        


        // GET: api/Telefonos/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Telefono>> GetTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);

            if (telefono == null)
            {
                return NotFound();
            }
            
                return telefono;

        }

        // PUT: api/Telefonos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(int id, Telefono telefono)
        {
            if (id != telefono.TelefonoId)
            {
                return BadRequest();
            }

            var tel = await _context.Telefono.FindAsync(id);

            if (tel.Sensores != null)
            {
                tel.Sensores.Clear();
            }

            _context.ChangeTracker.Clear();

            if (telefono.SensoresList != null)
            {
                foreach (var senId in telefono.SensoresList)
                {
                    var sensor = await _context.Sensor.FindAsync(senId);
                    if (sensor != null)
                    {
                        telefono.Sensores.Add(sensor);
                    }

                }
            }


            _context.Entry(telefono).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelefonoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Telefonos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono tel)
        {
            
            foreach (var item in tel.SensoresList)
            {
                Sensor s = await _context.Sensor.FindAsync(item);
                tel.Sensores.Add(s);
                              
                         
            }
            
            _context.Telefono.Add(tel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelefono", new { id = tel.TelefonoId }, tel);


        }

        // DELETE: api/Telefonos/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(int id)
        {
            var telefono = await _context.Telefono.FindAsync(id);
            if (telefono == null)
            {
                return NotFound();
            }

            _context.Telefono.Remove(telefono);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelefonoExists(int id)
        {
            return _context.Telefono.Any(e => e.TelefonoId == id);
        }
    }
}
