using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisWebApp.Data;
using InfoCadastraisWebApp.Models;

namespace InfoCadastraisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly InfosCadastraisContext _context;
        private readonly IInfosCadastraisBroker _broker;

        public ConsultasController(InfosCadastraisContext context, IInfosCadastraisBroker broker)
        {
            _context = context;
            _broker = broker;
        }

        // GET: api/Consultas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsulta()
        {
            return await _context.Consulta.ToListAsync();
        }

        // GET: api/Consultas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Consulta>> GetConsulta(int id)
        {
            var consulta = await _context.Consulta.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }

            return consulta;
        }

        [HttpGet("{idConveniado}/{idAssociado}")]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultasPorConveniado([FromRoute]int idConveniado, [FromRoute]int idAssociado)
        {
            var consultas = await _broker.BuscarConsultasAssociadoPorConveniado(idConveniado, idAssociado);

            if (consultas == null)
            {
                return NotFound();
            }

            return Ok(consultas);
        }

        // POST: api/Consultas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Consulta>> PostConsulta(Consulta consulta)
        {
            _context.Consulta.Add(consulta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConsulta", new { id = consulta.Id }, consulta);
        }

    }
}
