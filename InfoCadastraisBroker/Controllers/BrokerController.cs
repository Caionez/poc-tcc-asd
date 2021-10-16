using InfoCadastraisBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace InfoCadastraisBroker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrokerController : ControllerBase
    {
        private readonly List<Conveniado> ListaConveniados = new()
        {
            new Conveniado
            {
                Id = 1,
                Nome = "Clinica Sul",
                Endereco = "Rua Sul, N° 23, Centro Sul"
            },
            new Conveniado
            {
                Id = 2,
                Nome = "Centro Médico Lagoa",
                Endereco = "Rua Maria Goretti, N° 215, Lagoa"
            },
            new Conveniado
            {
                Id = 3,
                Nome = "Hospital Santo Américo",
                Endereco = "Avenida Principal, N° 152, Centro"
            }
        };

        private readonly List<Consulta> ListaConsultas = new()
        {
            new Consulta
            {
                Id = 254,
                DataConsulta = DateTime.Now.AddDays(-30).Date,
                Especialidade = new() { Nome = "Dermatologia" },
                Prestador = new Prestador
                {
                    Id = 1,
                    Nome = "Alexandre Silva",
                    IdConveniado = 1
                },
                Conveniado = new Conveniado
                {
                    Id = 1,
                    Nome = "Clinica Sul",
                    Endereco = "Rua Sul, N° 23, Centro Sul"
                },
                IdAssociado = 1
            },
            new Consulta
            {
                Id = 451,
                DataConsulta = DateTime.Now.AddDays(-10).Date,
                Especialidade = new() { Nome = "Clinica Geral" },
                Prestador = new Prestador
                {
                    Id = 2,
                    Nome = "Maria Goretti",
                    IdConveniado = 1
                },
                Conveniado = new Conveniado
                {
                    Id = 1,
                    Nome = "Clinica Sul",
                    Endereco = "Rua Sul, N° 23, Centro Sul"
                },
                IdAssociado = 1
            },
            new Consulta
            {
                Id = 451,
                DataConsulta = DateTime.Now.AddDays(-40).Date,
                Especialidade = new() { Nome = "Clinica Geral" },
                Prestador = new Prestador
                {
                    Id = 2,
                    Nome = "Maria Goretti",
                    IdConveniado = 1
                },
                Conveniado = new Conveniado
                {
                    Id = 1,
                    Nome = "Clinica Sul",
                    Endereco = "Rua Sul, N° 23, Centro Sul"
                },
                IdAssociado = 2
            }
        };

        private readonly List<Prestador> ListaPrestadores = new()
        {
            new Prestador
            {
                Id = 1,
                Nome = "Alexandre Silva",
                Especialidades = new List<Especialidade> { new() { Nome = "Dermatologia" } },
                IdConveniado = 1
            },
            new Prestador
            {
                Id = 2,
                Nome = "Maria Goretti",
                Especialidades = new List<Especialidade> { new() { Nome = "Clinica Geral" } },
                IdConveniado = 1
            }
        };

        [HttpGet("conveniados")]
        public IEnumerable<Conveniado> BuscarConveniados()
        {
            return ListaConveniados;
        }

        [HttpGet("consultas/{idConveniado}/{idAssociado}")]
        public IEnumerable<Consulta> BuscarConsultasAssociadoPorConveniado([FromRoute] int idConveniado, [FromRoute] int idAssociado)
        {
            return ListaConsultas.Where(c => c.Conveniado.Id == idConveniado && c.IdAssociado == idAssociado);
        }

        [HttpGet("prestadores/{nomeEspecialidade}")]
        public IEnumerable<Prestador> BuscarPrestadoresPorEspecialidade([FromRoute] string nomeEspecialidade)
        {
            return ListaPrestadores.Where(p => p.Especialidades.Contains(new Especialidade { Nome = nomeEspecialidade }));
        }
    }
}
