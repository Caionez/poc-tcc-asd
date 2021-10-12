using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisApi.Models;
using InfoCadastraisApi.Data;

namespace InfoCadastraisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConveniadosController : ControllerBase
    {
        private readonly BrokerInfosCadastrais _broker;
        public ConveniadosController(BrokerInfosCadastrais broker)
        {
            _broker = broker;
        }

        // GET: api/Conveniados
        [HttpGet]
        public ActionResult<IEnumerable<Conveniado>> GetConveniados()
        {
            return Ok(_broker.BuscarConveniados());
        }
    }
}
