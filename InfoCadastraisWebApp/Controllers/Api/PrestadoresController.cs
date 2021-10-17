using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.DTOs;
using InfoCadastraisWebApp.Repositories;
using Microsoft.AspNetCore.Http;

namespace InfoCadastraisWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestadoresController : ControllerBase
    {
        private readonly IPrestadorRepository _repository;

        public PrestadoresController(IPrestadorRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Busca um Prestador pelo Id.
        /// </summary>
        /// <param name="id">Id do Prestador</param>
        /// <returns>Objeto contendo informações do Prestador solicitado</returns>
        /// <response code="200">Retorna um objeto contendo informações do Prestador solicitado</response>
        /// <response code="404">Se o Prestador não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PrestadorDTO>> GetPrestador(int id)
        {
            var prestador = await _repository.ObterPrestadorPorId(id);

            return prestador ?? (ActionResult<PrestadorDTO>)NotFound();
        }

        /// <summary>
        /// Editar os dados de um Prestador.
        /// </summary>
        /// <param name="id">Id do Prestador</param>
        /// <param name="prestadorDTO">Objeto contendo as informações do Prestador a ser editado</param>
        /// <response code="200">Sucesso na alteração do Prestador</response>
        /// <response code="400">Se o Id informado for diferente do Id do Objeto passado no corpo da requisição</response>
        /// <response code="404">Se o Prestador não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutPrestador(int id, PrestadorDTO prestadorDTO)
        {
            if (id != prestadorDTO.Id)
                return BadRequest();

            return (await _repository.AtualizarPrestador(id, prestadorDTO)) ? NoContent() : NotFound();
        }

        /// <summary>
        /// Cadastrar um novo Prestador.
        /// </summary>
        /// <param name="prestadorDTO">Objeto contendo as informações do Prestador a ser cadastrado</param>
        /// <response code="201">Sucesso na criação do Prestador</response>
        /// <response code="400">Se o Objeto passado no corpo da requisição tiver algum dado inválido ou dados requeridos em branco</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Prestador>> PostPrestador(PrestadorDTO prestadorDTO)
        {
            await _repository.InserirPrestador(prestadorDTO);
            return CreatedAtAction(nameof(GetPrestador), new { id = prestadorDTO.Id }, prestadorDTO);
        }

        /// <summary>
        /// Apagar o cadastro de um Prestador.
        /// </summary>
        /// <param name="id">Id do prestador</param>
        /// <response code="204">Sucesso na exclusão do Prestador</response>
        /// <response code="404">Se o prestador não for encontrado para exclusão</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePrestador(int id)
        {
            return (await _repository.ExcluirPrestador(id)) ? NoContent() : NotFound();
        }
    }
}
