using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendExpertos.Contexts;
using BackendExpertos.Models;

namespace ExpertosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class serviciosController : ControllerBase
    {
        private readonly ExpertoContext _context;

        public serviciosController(ExpertoContext context)
        {
            _context = context;
        }

        // GET: api/servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<servicio>>> Getservicios()
        {
            return await _context.servicios.ToListAsync();
        }

        // GET: api/servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<servicio>> Getservicio(int id)
        {
            var servicio = await _context.servicios.FindAsync(id);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        // PUT: api/servicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putservicio(int id, servicio servicio)
        {
            if (id != servicio.id)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!servicioExists(id))
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

        // POST: api/servicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<servicio>> Postservicio(servicio servicio)
        {
            _context.servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getservicio", new { id = servicio.id }, servicio);
        }

        // DELETE: api/servicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteservicio(int id)
        {
            var servicio = await _context.servicios.FindAsync(id);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool servicioExists(int id)
        {
            return _context.servicios.Any(e => e.id == id);
        }
    }
}
