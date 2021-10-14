using System;
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
    public class PrestadoresController : ControllerBase
    {
        private readonly InfosCadastraisContext _context;
        private readonly IInfosCadastraisBroker _broker;

        public PrestadoresController(InfosCadastraisContext context, IInfosCadastraisBroker broker)
        {
            _context = context;
            _broker = broker;
        }

        // GET: api/Prestador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadores()
        {
            var prestadores = _context.Prestadores;

            return await prestadores.Select(p => PrestadorParaDTO(p))
                                    .ToListAsync();
        }

        // GET: api/Prestador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrestadorDTO>> GetPrestador(int id)
        {
            var queryPrestador = _context.Prestadores
                                     .Where(p => p.Id == id)
                                     .Include(p => p.Especialidades);

            var prestador = await queryPrestador.FirstOrDefaultAsync();
            
            if (prestador == null)
                return NotFound();
            
            return PrestadorParaDTO(prestador);
        }

        [HttpGet("{contexto}/{nomeEspecialidade}")]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadoresPorEspecialidade([FromRoute]ContextoBusca contexto, [FromRoute]string nomeEspecialidade)
        {
            IEnumerable<Prestador> prestadores;

            if (contexto == ContextoBusca.InfosCadastrais)
            {
                var especialidade = await _context.Especialidades.Where(e => e.Nome == nomeEspecialidade).FirstOrDefaultAsync();
                prestadores = _context.Prestadores.Where(p => p.Especialidades.Contains(especialidade));
            }
            else
                prestadores = await _broker.BuscarPrestadoresPorEspecialidade(nomeEspecialidade);

            if (prestadores == null)
                return NotFound();

            return Ok(prestadores.Select(p => PrestadorParaDTO(p)));
        }

        // PUT: api/Prestador/5
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
        [HttpPost]
        public async Task<ActionResult<Prestador>> PostPrestador(PrestadorDTO prestadorDTO)
        {
            Prestador prestador = new Prestador
            {
                Nome = prestadorDTO.Nome,
                Especialidades = new List<Especialidade>()
            };

            foreach(EspecialidadeDTO esp in prestadorDTO.Especialidades)
                prestador.Especialidades.Add(_context.Especialidades.Find(esp.Id));

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

        private static PrestadorDTO PrestadorParaDTO(Prestador p) =>
            new PrestadorDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Especialidades = p.Especialidades?.Select(e => new EspecialidadeDTO
                {
                    Id = e.Id,
                    Nome = e.Nome
                }).ToList()
            };
    }
}
