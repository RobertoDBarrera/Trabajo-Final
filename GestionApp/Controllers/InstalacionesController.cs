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
    public class InstalacionesController : ControllerBase
    {
        private readonly GestionAppContext _context;

        public InstalacionesController(GestionAppContext context)
        {
            _context = context;
        }

        // GET: api/Instalaciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instalacion>>> GetInstalacion()
        {
            return await _context.Instalacion.ToListAsync();
        }

        // GET: api/Instalaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instalacion>> GetInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);

            if (instalacion == null)
            {
                return NotFound();
            }

            return instalacion;
        }

        //punto 4
        [HttpGet("filtroinst")]
        public dynamic Filtroinst(Boolean exitosa)
        {
            if (exitosa == true)
            {
                return _context.Instalacion
                .Where(item => item.Exitosa == true)
                .Select(item => new
                {
                    item.Exitosa,
                    Aplicacion = item.App.Nombre,
                    Operario = item.Operario.Nombre,
                    item.Operario.Apellido

                })
                .ToList();
            }
            else
            {
                return _context.Instalacion
                                .Where(item => item.Exitosa == false)
                                .Select(item => new
                                {
                                    item.Exitosa,
                                    Aplicacion = item.App.Nombre,
                                    Operario = item.Operario.Nombre,
                                    item.Operario.Apellido

                                })
                                .ToList();
            }
        }

        // GET: api/instalaciones/instxdia
        //punto opcional
        [HttpGet("instxdia")]
        public dynamic Instxdia(DateTime fecha)
        {
            fecha = fecha.Date;
            return _context.Instalacion
                .Where(item => item.Fecha.Date == fecha)
                .Select(item => new
                {
                    Fecha = item.Fecha.Date,
                    Nombre_Operario = item.Operario.Nombre,
                    Apellido_Operario = item.Operario.Apellido,
                    Apps_instaladas = item.Operario.Instalaciones.Where(ok=>item.Exitosa==true).Select(inst=> new {Estado=inst.Exitosa,Nombre=inst.App.Nombre }).ToList()
                }).ToList();


        }



        // PUT: api/Instalaciones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstalacion(int id, Instalacion instalacion)
        {
            if (id != instalacion.InstalacionId)
            {
                return BadRequest();
            }

            _context.Entry(instalacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstalacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInstalacion", new { id = instalacion.InstalacionId }, instalacion);
            //return NoContent();
        }

        // POST: api/Instalaciones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Instalacion>> PostInstalacion(Instalacion instalacion)
        {
            DateTime fecha = instalacion.Fecha.Date;
            instalacion.Fecha = fecha;
            _context.Instalacion.Add(instalacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstalacion", new { id = instalacion.InstalacionId }, instalacion);
        }

        // DELETE: api/Instalaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstalacion(int id)
        {
            var instalacion = await _context.Instalacion.FindAsync(id);
            if (instalacion == null)
            {
                return NotFound();
            }

            _context.Instalacion.Remove(instalacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InstalacionExists(int id)
        {
            return _context.Instalacion.Any(e => e.InstalacionId == id);
        }
    }
}
