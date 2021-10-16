using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.Data;
using System.Threading.Tasks;
using InfoCadastraisWebApp.DTOs;

namespace InfoCadastraisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniadosController : ControllerBase
    {
        private readonly IInfosCadastraisBroker _broker;
        private readonly InfoCadastraisContext _context;

        public ConveniadosController(IInfosCadastraisBroker broker, InfoCadastraisContext context)
        {
            _broker = broker;
            _context = context;
        }

        // GET: api/Conveniados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conveniado>>> GetConveniados()
        {
            return Ok(await _broker.BuscarConveniados());
        }

        [HttpPost]
        public async Task<ActionResult<Conveniado>> PostConveniado(ConveniadoDTO conveniadoDTO)
        {
            var Conveniado = new Conveniado
            {
                Id = conveniadoDTO.Id,
                Nome = conveniadoDTO.Nome,
                Endereco = conveniadoDTO.Endereco
            };

            _context.Conveniados.Add(Conveniado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConveniado", new { id = Conveniado.Id }, Conveniado);
        }
    }
}
