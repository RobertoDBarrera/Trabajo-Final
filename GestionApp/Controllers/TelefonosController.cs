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

        [HttpGet("filtroappsen")]
        public dynamic Filtroappsen(int idapp, int idsen)
        {
            if (idsen !=0 && idapp !=0 )
            {
                return BadRequest();
            }
            if (idsen != 0)
            {
                return _context.Sensor
                 .Where(item => item.SensorId == idsen)
                 .Select(item => new
                 {
                     Nombre_Sensor = item.Nombre,
                     listatelefonos = item.Telefonos.Select(tel => new
                     {
                         marca = tel.Marca,
                         modelo = tel.Modelo,
                         precio = tel.Precio
                     }).ToList()
                 }).ToList();
            }
            return _context.App
                .Where(item => item.AppId == idapp)
                .Select(item => new
                {
                    Nombre_App = item.Nombre,
                    listatelefonos = item.Instalaciones.Select(inst => new
                    {
                        Telefono_Marca = inst.Telefono.Marca,
                        Telefono_Modelo = inst.Telefono.Modelo,
                        Telefono_Precio = inst.Telefono.Precio
                    }).ToList()
                }).ToList();

        }

        //punto 5
        //filtro x sensor
        [HttpGet("filtroxsensor")]
        public dynamic Filtroxsensor(int id)
        {
           return _context.Sensor
                .Where(item => item.SensorId == id)
                .Select(item => new
                {
                    Nombre_Sensor = item.Nombre,
                    listatelefonos = item.Telefonos.Select(tel => new
                    {
                        marca = tel.Marca,
                        modelo = tel.Modelo,
                        precio = tel.Precio
                    }).ToList()
                }).ToList();

        }

        //filtro x app instalada
        [HttpGet("filtroxapp")]
        public dynamic Filtroxapp(int id)
        {
             return _context.App
                .Where(item => item.AppId == id)
                .Select(item => new
                {
                    Nombre_App = item.Nombre,
                    listatelefonos = item.Instalaciones.Select(inst => new
                    {
                        Telefono_Marca = inst.Telefono.Marca,
                        Telefono_Modelo = inst.Telefono.Modelo,
                        Telefono_Precio = inst.Telefono.Precio
                    }).ToList()
                }).ToList();

        }
        //fin punto 5

        //GET: api/Telefonos/buscar
        //punto 2
        [HttpGet("telysensores")]
        public dynamic Telysensores()
        {
            return _context.Telefono
                .Select(item => new
                {
                    item.Marca,
                    item.Modelo,
                    item.Precio,
                    ListaSensores=item.Sensores.Select(sen => new
                    {
                        sen.Nombre
                    }).ToList()
                }).ToList();

        }

        
        //punto 3
        [HttpGet("telxid")]
        public dynamic Telxid(int id)
        {
            var Tel = _context.Telefono.Find(id);
            if (Tel == null)
            {
                return NotFound();
            }
            return _context.Telefono
                .Where(item => item.TelefonoId == id)
                .Select(item => new
                {
                    marca = item.Marca,
                    modelo = item.Modelo,
                    listaappsinstaladas = item.Instalaciones.Select(app => new
                    {
                        Aplicacion=app.App.Nombre,
                        Nombre_Operario=app.Operario.Nombre,
                        Apellido_Operario=app.Operario.Apellido
                    }).ToList()
                }).ToList(); 

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

            return CreatedAtAction("GetTelefono", new { id = telefono.TelefonoId }, telefono);
            //return NoContent();
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
