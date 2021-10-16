using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.DTOs;
using InfoCadastraisWebApp.Repositories;

namespace InfoCadastraisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadoresController : ControllerBase
    {
        private readonly IPrestadorRepository _repository;

        public PrestadoresController(IPrestadorRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Prestador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadores()
        {
            return Ok(await _repository.ListarPrestadores());
        }

        // GET: api/Prestador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrestadorDTO>> GetPrestador(int id)
        {   
            var prestador = await _repository.ObterPrestadorPorId(id);            
            if (prestador == null)
                return NotFound();
            
            return prestador;
        }

        [HttpGet("{contexto}/{nomeEspecialidade}")]
        public async Task<ActionResult<IEnumerable<PrestadorDTO>>> GetPrestadoresPorEspecialidade([FromRoute]ContextoBusca contexto, [FromRoute]string nomeEspecialidade)
        {
            var prestadores = await _repository.ListarPrestadoresPorEspecialidade(contexto, nomeEspecialidade);
            
            if (prestadores == null)
                return NotFound();

            return Ok(prestadores);
        }

        // PUT: api/Prestador/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestador(int id, PrestadorDTO prestadorDTO)
        {
            if (id != prestadorDTO.Id)
                return BadRequest();

            return (await _repository.AtualizarPrestador(id, prestadorDTO)) ? NoContent() : NotFound();
        }

        // POST: api/Prestador
        [HttpPost]
        public async Task<ActionResult<Prestador>> PostPrestador(PrestadorDTO prestadorDTO)
        {
            await _repository.InserirPrestador(prestadorDTO);
            return CreatedAtAction(nameof(GetPrestador), new { id = prestadorDTO.Id }, prestadorDTO);
        }

        // DELETE: api/Prestador/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            return (await _repository.ExcluirPrestador(id)) ? NoContent() : NotFound();
        }
    }
}
