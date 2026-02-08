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
    public class profesionalesController : ControllerBase
    {
        private readonly ExpertoContext _context;

        public profesionalesController(ExpertoContext context)
        {
            _context = context;
        }

        // GET: api/profesionales - Todos autenticados ven lista
        [Authorize(Roles = "Administrador,Cliente,Profesional")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<profesionale>>> Getprofesionales()
        {
            return await _context.profesionales.ToListAsync();
        }


        // GET: api/profesionales/5 - Público puede ver perfil
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<profesionale>> Getprofesionale(int id)
        {
            var profesionale = await _context.profesionales.FindAsync(id);

            if (profesionale == null)
            {
                return NotFound();
            }

            return profesionale;
        }

        // PUT: api/profesionales/5 - Solo el propio profesional o Admin 
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Administrador,Profesional")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Putprofesionale(int id, profesionale profesionale)
        {
            if (id != profesionale.id)
            {
                return BadRequest();
            }

            _context.Entry(profesionale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!profesionaleExists(id))
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

        // POST: api/profesionales - Profesionales se registran
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<profesionale>> Postprofesionale(profesionale profesionale)
        {
            _context.profesionales.Add(profesionale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getprofesionale", new { id = profesionale.id }, profesionale);
        }

        // DELETE: api/profesionales/5 - Solo Administrador
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteprofesionale(int id)
        {
            var profesionale = await _context.profesionales.FindAsync(id);
            if (profesionale == null)
            {
                return NotFound();
            }

            _context.profesionales.Remove(profesionale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool profesionaleExists(int id)
        {
            return _context.profesionales.Any(e => e.id == id);
        }
    }
}
