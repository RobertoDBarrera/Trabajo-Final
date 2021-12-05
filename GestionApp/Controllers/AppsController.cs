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
    public class AppsController : ControllerBase
    {
        private readonly GestionAppContext _context;

        public AppsController(GestionAppContext context)
        {
            _context = context;
        }

        // GET: api/Apps
        [HttpGet]
        public dynamic GetApp()
        {
            return _context.App.Select(item=>new {
                                                    item.AppId,
                                                    item.Nombre,
                                                    
                                                 });
        }

        // GET: api/Apps/5
        [HttpGet("{id}")]
        public dynamic GetApp(int id)
        {
            var app = _context.App.Find(id);

            if (app == null)
            {
                return NotFound();
            }

            return _context.App.Where(a=> a.AppId==id)
                .Select(item=>new { 
                                            item.AppId,
                                            item.Nombre
                                                  });
        }
        
        // PUT: api/Apps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApp(int id, App app)
        {
            if (id != app.AppId)
            {
                return BadRequest();
            }

            _context.Entry(app).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetApp", new { id = app.AppId }, app);
            //return NoContent();
        }

        // POST: api/Apps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App>> PostApp(App app)
        {
            _context.App.Add(app);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApp", new { id = app.AppId }, app);
        }

        // DELETE: api/Apps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(int id)
        {
            var app = await _context.App.FindAsync(id);
            if (app == null)
            {
                return NotFound();
            }

            _context.App.Remove(app);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppExists(int id)
        {
            return _context.App.Any(e => e.AppId == id);
        }
    }
}
