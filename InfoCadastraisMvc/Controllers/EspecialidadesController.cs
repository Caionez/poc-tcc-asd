using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisMvc.Models;
using InfoCadastraisMvc.DTOs;
using InfoCadastraisMvc.Data;

namespace InfoCadastraisMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private readonly InfosCadastraisContext _context;

        public EspecialidadesController(InfosCadastraisContext context)
        {
            _context = context;
        }

        // GET: api/Especialidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadeDTO>>> GetEspecialidades()
        {
            var especialidades = _context.Especialidades;

            return await especialidades.Select(e => EspecialidadeParaDTO(e))
            .ToListAsync();
        }

        // GET: api/Especialidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadeDTO>> GetEspecialidade(int id)
        {
            var especialidade = await _context.Especialidades.FindAsync(id);

            if (especialidade == null)
                return NotFound();

            return EspecialidadeParaDTO(especialidade);
        }

        // PUT: api/Especialidades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidade(int id, EspecialidadeDTO especialidadeDTO)
        {
            if (id != especialidadeDTO.Id)
                return BadRequest();
            
            var especialidade = await _context.Especialidades.FindAsync(id);
            
            if (especialidade == null)
                return NotFound();

            especialidade.Nome = especialidadeDTO.Nome;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EspecialidadeExists(id))
            {   
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Especialidades
        [HttpPost]
        public async Task<ActionResult<Especialidade>> PostEspecialidade(EspecialidadeDTO especialidadeDTO)
        {
            var especialidade = new Especialidade
            { 
                Nome = especialidadeDTO.Nome
            };

            _context.Especialidades.Add(especialidade);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecialidade", new { id = especialidade.Id }, especialidade);
        }

        // DELETE: api/Especialidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidade(int id)
        {
            var especialidade = await _context.Especialidades.FindAsync(id);
            if (especialidade == null)
            {
                return NotFound();
            }

            _context.Especialidades.Remove(especialidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EspecialidadeExists(int id)
        {
            return _context.Especialidades.Any(e => e.Id == id);
        }

        private static EspecialidadeDTO EspecialidadeParaDTO(Especialidade especialidade) =>
            new EspecialidadeDTO 
            {
                Id = especialidade.Id,
                Nome = especialidade.Nome
            };
    }
}
