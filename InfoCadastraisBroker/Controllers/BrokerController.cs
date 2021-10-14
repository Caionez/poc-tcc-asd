using InfoCadastraisBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InfoCadastraisBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrokerController : ControllerBase
    {
        private readonly List<Conveniado> ListaConveniados = new List<Conveniado>()
        {
            new Conveniado
            {
                Id = 1,
                Nome = "Clinica Sul",
                Endereço = "Rua Sul, N° 23, Centro Sul"
            },
            new Conveniado
            {
                Id = 2,
                Nome = "Centro Médico Lagoa",
                Endereço = "Rua Maria Goretti, N° 215, Lagoa"
            },
            new Conveniado
            {
                Id = 3,
                Nome = "Hospital Santo Américo",
                Endereço = "Avenida Principal, N° 152, Centro"
            }
        };

        private readonly List<Consulta> ListaConsultas = new List<Consulta>()
        {
            new Consulta
            {
                Id = 254,
                Especialidade = "Dermatologia",
                DataConsulta = DateTime.Now.AddDays(-30).Date,
                Prestador = new Prestador
                {
                    Id = 1,
                    Nome = "Alexandre Silva",
                    Especialidades = new [] { "Dermatologia" },
                    IdConveniado = 1
                },
                Conveniado = new Conveniado
                {
                    Id = 1,
                    Nome = "Clinica Sul",
                    Endereço = "Rua Sul, N° 23, Centro Sul"
                }
            }
        };
        
        private readonly List<Prestador> ListaPrestadores = new List<Prestador>()
        {
            new Prestador
            {
                Id = 1,
                Nome = "Alexandre Silva",
                Especialidades = new [] { "Dermatologia" },
                IdConveniado = 1
            }
        };       

        [HttpGet("conveniados")]
        public IEnumerable<Conveniado> BuscarConveniados()
        {
            return ListaConveniados; 
        }

        [HttpGet("consultas/{idConveniado}/{idAssociado}")]
        public IEnumerable<Consulta> BuscarConsultasAssociadoPorConveniado([FromRoute]int idConveniado, [FromRoute]int idAssociado)
        {
            return ListaConsultas;
        }

        [HttpGet("prestadores/{nomeEspecialidade}")]
        public IEnumerable<Prestador> BuscarPrestadoresPorEspecialidade([FromRoute]string nomeEspecialidade)
        {
            return ListaPrestadores;
        }
    }
}
