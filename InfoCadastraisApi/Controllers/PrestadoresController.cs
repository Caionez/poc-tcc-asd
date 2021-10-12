using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisApi.Models;
using System.ComponentModel.DataAnnotations;

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
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadores()
        {
            return await _context.Prestadores.ToListAsync();
        }

        // GET: api/Prestador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestador>> GetPrestador(int id)
        {
            var prestador = await _context.Prestadores.FindAsync(id);

            if (prestador == null)
            {
                return NotFound();
            }

            return prestador;
        }

        [HttpGet("{contexto}/{nomeEspecialidade}")]
        public async Task<ActionResult<IEnumerable<Prestador>>> GetPrestadoresPorEspecialidade([FromRoute]ContextoBusca contexto, [FromRoute]string nomeEspecialidade)
        {
            IEnumerable<Prestador> prestadores;

            if (contexto == ContextoBusca.InfosCadastrais)
            {
                prestadores = await (from p in _context.Prestadores
                                  from ep in p.Especialidades
                                  from e in _context.Especialidades
                                  where e.Nome == nomeEspecialidade
                                        && e.Id == ep.IdEspecialidade
                                        && ep.IdPrestador == p.Id
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
        public async Task<IActionResult> PutPrestador(int id, Prestador prestador)
        {
            if (id != prestador.Id)
            {
                return BadRequest();
            }

            _context.Entry(prestador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestadorExists(id))
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
