using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.ViewModels;
using InfoCadastraisWebApp.Repositories;

namespace InfoCadastraisWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPrestadorRepository _repository;
        public HomeController(IPrestadorRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ConsultaPrestador()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> ConsultarPrestador([Bind("Especialidade,BuscaExterna")]ConsultaPrestadorViewModel busca)
        {
            ContextoBusca contexto = busca.BuscaExterna ? ContextoBusca.Externo : ContextoBusca.InfosCadastrais;

            var prestadores = await _repository.ListarPrestadoresPorEspecialidade(contexto, busca.Especialidade);

            var model = new ConsultaPrestadorViewModel {
                PrestadoresEncontrados = prestadores.ToList()
            };

            return View("ConsultaPrestador", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
