using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisMvc.Models;
using InfoCadastraisMvc.Data;
using System.Threading.Tasks;

namespace InfoCadastraisMvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniadosController : ControllerBase
    {
        private readonly InfosCadastraisBroker _broker;
        public ConveniadosController(InfosCadastraisBroker broker)
        {
            _broker = broker;
        }

        // GET: api/Conveniados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conveniado>>> GetConveniados()
        {
            return Ok(await _broker.BuscarConveniados());
        }
    }
}
