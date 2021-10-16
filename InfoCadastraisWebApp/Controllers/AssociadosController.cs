using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.Data;

namespace InfoCadastraisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociadosController : ControllerBase
    {
        private readonly InfoCadastraisContext _context;

        public AssociadosController(InfoCadastraisContext context)
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

            return associado ?? (ActionResult<Associado>)NotFound();
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
    }
}
