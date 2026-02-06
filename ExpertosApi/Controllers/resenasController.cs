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
    public class resenasController : ControllerBase
    {
        private readonly ExpertoContext _context;

        public resenasController(ExpertoContext context)
        {
            _context = context;
        }

        // GET: api/resenas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<resena>>> Getresenas()
        {
            return await _context.resenas.ToListAsync();
        }

        // GET: api/resenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<resena>> Getresena(int id)
        {
            var resena = await _context.resenas.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            return resena;
        }

        // PUT: api/resenas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putresena(int id, resena resena)
        {
            if (id != resena.id)
            {
                return BadRequest();
            }

            _context.Entry(resena).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!resenaExists(id))
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

        // POST: api/resenas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<resena>> Postresena(resena resena)
        {
            _context.resenas.Add(resena);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getresena", new { id = resena.id }, resena);
        }

        // DELETE: api/resenas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteresena(int id)
        {
            var resena = await _context.resenas.FindAsync(id);
            if (resena == null)
            {
                return NotFound();
            }

            _context.resenas.Remove(resena);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool resenaExists(int id)
        {
            return _context.resenas.Any(e => e.id == id);
        }
    }
}
