using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.ViewModels;
using InfoCadastraisWebApp.Repositories;

namespace InfoCadastraisWebApp.Controllers
{
    public class PesquisarController : Controller
    {
        private readonly IPrestadorRepository _repository;
        public PesquisarController(IPrestadorRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public IActionResult Prestador()
        {
            return View("PesquisarPrestador");
        }

        [Authorize]
        public async Task<IActionResult> PesquisarPrestadores([Bind("Especialidade,BuscaExterna")]PesquisarPrestadorViewModel busca)
        {
            ContextoBusca contexto = busca.BuscaExterna ? ContextoBusca.Externo : ContextoBusca.InfosCadastrais;

            var prestadores = await _repository.ListarPrestadoresPorEspecialidade(contexto, busca.Especialidade);

            var model = new PesquisarPrestadorViewModel {
                PrestadoresEncontrados = prestadores.ToList()
            };

            return View("PesquisarPrestador", model);
        }
    }
}
