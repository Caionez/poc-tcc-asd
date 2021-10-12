using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisApi.Models;
using InfoCadastraisApi.DTOs;

namespace InfoCadastraisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadoresController : ControllerBase
    {
        private readonly InfosCadastraisContext _context;

        public PrestadoresController(InfosCadastraisContext context)
        {
            _context = context;
        }

        // GET: api/Prestador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadores()
        {
            var prestadores = _context.Prestadores;

            return await prestadores.Select(p => new PrestadorDTO 
            {
                Nome = p.Nome,
                Especialidades = p.Especialidades.Select(e => new EspecialidadeDTO
                    {
                        Id = e.Id,
                        Nome = e.Nome
                    })
                    .ToList()
            })
            .ToListAsync();
        }

        // GET: api/Prestador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestador>> GetPrestador(int id)
        {
            var queryPrestador = _context.Prestadores
                                     .Where(p => p.Id == id)
                                     .Include(p => p.Especialidades);

            var prestador = await queryPrestador.FirstOrDefaultAsync();
            
            if (prestador == null)
                return NotFound();
            
            return prestador;
        }

        [HttpGet("{contexto}/{nomeEspecialidade}")]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadoresPorEspecialidade([FromRoute]ContextoBusca contexto, [FromRoute]string nomeEspecialidade)
        {
            IEnumerable<Prestador> prestadores;

            if (contexto == ContextoBusca.InfosCadastrais)
            {
                prestadores = await (from p in _context.Prestadores
                                  from e in _context.Especialidades
                                  where e.Nome == nomeEspecialidade
                                  select p).ToListAsync();
            }
            else
                prestadores = null; //_broker.BuscarPrestadoresPorEspecialidade(especialidade);

            if (prestadores == null)
            {
                return NotFound();
            }

            return Ok(prestadores);
        }

        // PUT: api/Prestador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, PrestadorDTO prestadorDTO)
        {
            if (id != prestadorDTO.Id)
                return BadRequest();
            
            var prestador = await _context.Prestadores
                                          .Where(p => p.Id == id)
                                          .Include(p => p.Especialidades)
                                          .FirstOrDefaultAsync();
            
            if (prestador == null)
                return NotFound();

            prestador.Nome = prestadorDTO.Nome;
            prestador.Especialidades.Clear();

            foreach(EspecialidadeDTO esp in prestadorDTO.Especialidades)
                prestador.Especialidades.Add(_context.Especialidades.Find(esp.Id));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PrestadorExists(id))
            {   
                return NotFound();
            }

            return NoContent();

        }

        // POST: api/Prestador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Prestador>> PostPrestador(Prestador prestador)
        {
            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrestador), new { id = prestador.Id }, prestador);
        }

        // DELETE: api/Prestador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);
            if (prestador == null)
            {
                return NotFound();
            }

            _context.Prestadores.Remove(prestador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrestadorExists(int id)
        {
            return _context.Prestadores.Any(e => e.Id == id);
        }
    }
}
