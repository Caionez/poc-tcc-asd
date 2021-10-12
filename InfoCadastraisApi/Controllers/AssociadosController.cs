using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisApi.Models;
using InfoCadastraisApi.Data;

namespace InfoCadastraisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociadosController : ControllerBase
    {
        private readonly InfosCadastraisContext _context;

        public AssociadosController(InfosCadastraisContext context)
        {
            _context = context;
        }

        // GET: api/Associados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Associado>>> GetAssociados()
        {
            return await _context.Associados.ToListAsync();
        }

        // GET: api/Associados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Associado>> GetAssociado(int id)
        {
            var associado = await _context.Associados.FindAsync(id);

            if (associado == null)
            {
                return NotFound();
            }

            return associado;
        }

        // PUT: api/Associados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssociado(int id, Associado associado)
        {
            if (id != associado.Id)
            {
                return BadRequest();
            }

            _context.Entry(associado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!AssociadoExists(id))
            {
                return NotFound();
            }
             
            return NoContent();
        }

        // POST: api/Associados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Associado>> PostAssociado(Associado associado)
        {
            _context.Associados.Add(associado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssociado", new { id = associado.Id }, associado);
        }

        // DELETE: api/Associados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssociado(int id)
        {
            var associado = await _context.Associados.FindAsync(id);
            if (associado == null)
            {
                return NotFound();
            }

            _context.Associados.Remove(associado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssociadoExists(int id)
        {
            return _context.Associados.Any(e => e.Id == id);
        }
    }
}
