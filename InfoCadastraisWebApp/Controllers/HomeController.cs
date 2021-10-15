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

namespace InfoCadastraisWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult ConsultarPrestador([Bind("Especialidade,BuscaExterna")] ConsultaPrestadorViewModel busca)
        {
            var model = new ConsultaPrestadorViewModel {
                PrestadoresEncontrados = new List<Prestador>() {
                    new Prestador 
                    {
                        Nome = "José Silva",
                        Conveniado = new Conveniado 
                        {
                            Nome = "Teste", Endereco = "Teste"
                        }
                    }
                }
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
