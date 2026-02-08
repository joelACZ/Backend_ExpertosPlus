using BackendExpertos.Contexts;
using BackendExpertos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertosApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]

    [ApiController]
    public class solicitudesController : ControllerBase
    {
        private readonly ExpertoContext _context;

        public solicitudesController(ExpertoContext context)
        {
            _context = context;
        }


        // GET: api/solicitudes - Admin ve todas, Profesional/Cliente ven las suyas
        [Authorize(Roles = "Administrador,Profesional,Cliente")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<solicitude>>> Getsolicitudes()
        {
            return await _context.solicitudes.ToListAsync();
        }


        // GET: api/solicitudes/5 - Admin o los involucrados
        [Authorize(Roles = "Administrador,Profesional,Cliente")]
        [HttpGet("{id}")]
        public async Task<ActionResult<solicitude>> Getsolicitude(int id)
        {
            var solicitude = await _context.solicitudes.FindAsync(id);

            if (solicitude == null)
            {
                return NotFound();
            }

            return solicitude;
        }

        // PUT: api/solicitudes/5 - Solo Admin o Profesional (para cambiar estado)
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador,Profesional")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Putsolicitude(int id, solicitude solicitude)
        {
            if (id != solicitude.id)
            {
                return BadRequest();
            }

            _context.Entry(solicitude).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!solicitudeExists(id))
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


        // POST: api/solicitudes - Solo Clientes crean solicitudes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Cliente")]
        [HttpPost]
        public async Task<ActionResult<solicitude>> Postsolicitude(solicitude solicitude)
        {
            _context.solicitudes.Add(solicitude);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getsolicitude", new { id = solicitude.id }, solicitude);
        }

        // DELETE: api/solicitudes/5 - Solo Admin
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesolicitude(int id)
        {
            var solicitude = await _context.solicitudes.FindAsync(id);
            if (solicitude == null)
            {
                return NotFound();
            }

            _context.solicitudes.Remove(solicitude);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool solicitudeExists(int id)
        {
            return _context.solicitudes.Any(e => e.id == id);
        }
    }
}
