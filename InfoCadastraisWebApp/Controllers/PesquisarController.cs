using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.ViewModels;
using InfoCadastraisWebApp.Repositories;
using InfoCadastraisWebApp.Data;
using InfoCadastraisWebApp.DTOs;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InfoCadastraisWebApp.Controllers
{
    public class PesquisarController : Controller
    {
        private readonly IPrestadorRepository _prestadorRepository;
        private readonly IInfosCadastraisBroker _broker;
        public PesquisarController(IPrestadorRepository prestadorRepository, IInfosCadastraisBroker broker)
        {
            _prestadorRepository = prestadorRepository;
            _broker = broker;
        }

        [Authorize]
        public IActionResult Prestador()
        {
            return View("PesquisarPrestador");
        }

        [Authorize]
        public async Task<IActionResult> ConsultasAssociados()
        {
            PesquisarConsultasAssociadosViewModel model = await CarregarFiltrosConsultasAssociados();

            return View("PesquisarConsultasAssociados", model);
        }

        private async Task<PesquisarConsultasAssociadosViewModel> CarregarFiltrosConsultasAssociados()
        {
            var conveniadosDto = (await _broker.BuscarConveniados()).Select(c => ConveniadoParaDTO(c)).ToList();

            return new()
            {
                Associados = new SelectList(MockAssociados, "Id", "Nome"),
                Conveniados = new SelectList(conveniadosDto, "Id", "Nome")
            };
        }

        private readonly List<AssociadoDTO> MockAssociados = new()
        {
            new AssociadoDTO { Id = 1, Nome = "Rayane Sousa" },
            new AssociadoDTO { Id = 2, Nome = "José Maria" },
            new AssociadoDTO { Id = 2, Nome = "Rubens Sousa" }
        };

        [Authorize]
        public async Task<IActionResult> PesquisarPrestadores([Bind("Especialidade,BuscaExterna")] PesquisarPrestadorViewModel busca)
        {
            ContextoBusca contexto = busca.BuscaExterna ? ContextoBusca.Externo : ContextoBusca.InfosCadastrais;

            var prestadores = await _prestadorRepository.ListarPrestadoresPorEspecialidade(contexto, busca.Especialidade);

            var model = new PesquisarPrestadorViewModel
            {
                PrestadoresEncontrados = prestadores.ToList()
            };

            return View("PesquisarPrestador", model);
        }

        [Authorize]
        public async Task<IActionResult> PesquisarConsultasAssociados([Bind("IdAssociado,IdConveniado")] PesquisarConsultasAssociadosViewModel busca)
        {
            var consultas = await _broker.BuscarConsultasAssociadoPorConveniado(busca.IdConveniado, busca.IdAssociado);
            var model = await CarregarFiltrosConsultasAssociados();
            model.ConsultasEncontradas = consultas?.Select(c => ConsultaParaDTO(c)).ToList();

            return View("PesquisarConsultasAssociados", model);
        }

        private static ConveniadoDTO ConveniadoParaDTO(Conveniado c) =>
        new()
        {
            Id = c.Id,
            Nome = c.Nome,
            Endereco = c.Endereco
        };
        private static ConsultaDTO ConsultaParaDTO(Consulta c) =>
            new()
            {
                Id = c.Id,
                DataConsulta = c.DataConsulta,
                NomePrestador = c.Prestador?.Nome,
                NomeEspecialidade = c.Especialidade?.Nome,
                NomeConveniado = c.Conveniado?.Nome
            };
    }
}
