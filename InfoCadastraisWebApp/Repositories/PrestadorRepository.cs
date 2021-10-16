using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.DTOs;
using InfoCadastraisWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCadastraisWebApp.Repositories
{
    public interface IPrestadorRepository
    {
        Task<IEnumerable<PrestadorDTO>> ListarPrestadores();
        Task<PrestadorDTO> ObterPrestadorPorId(int id);
        Task<IEnumerable<PrestadorDTO>> ListarPrestadoresPorEspecialidade(ContextoBusca contexto, string nomeEspecialidade);
        Task<bool> AtualizarPrestador(int id, PrestadorDTO prestadorDTO);
        Task InserirPrestador(PrestadorDTO prestadorDTO);
        Task<bool> ExcluirPrestador(int id);
    }

    public class PrestadorRepository : IPrestadorRepository
    {
        private readonly InfoCadastraisContext _context;
        private readonly IInfosCadastraisBroker _broker;

        public PrestadorRepository(InfoCadastraisContext context, IInfosCadastraisBroker broker)
        {
            _context = context;
            _broker = broker;

            _context.Database.EnsureCreated();
        }

        public async Task<IEnumerable<PrestadorDTO>> ListarPrestadores()
        {
            var prestadores = _context.Prestadores;

            return await prestadores.Select(p => PrestadorParaDTO(p))
                                    .ToListAsync();
        }

        public async Task<PrestadorDTO> ObterPrestadorPorId(int id)
        {
            var prestador = await ObterPrestadorPorIdInterno(id).ConfigureAwait(false);
            if (prestador == null) return null;

            return PrestadorParaDTO(prestador);
        }

        private async Task<Prestador> ObterPrestadorPorIdInterno(int id)
        {
            var queryPrestador = _context.Prestadores
                                         .Where(p => p.Id == id)
                                         .Include(p => p.Especialidades);

            return await queryPrestador.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PrestadorDTO>> ListarPrestadoresPorEspecialidade(ContextoBusca contexto, string nomeEspecialidade)
        {
            IEnumerable<Prestador> prestadores;

            if (contexto == ContextoBusca.InfosCadastrais)
            {
                var especialidade = await _context.Especialidades.Where(e => e.Nome == nomeEspecialidade).FirstOrDefaultAsync();
                prestadores = _context.Prestadores.Where(p => p.Especialidades.Contains(especialidade));
            }
            else
                prestadores = await _broker.BuscarPrestadoresPorEspecialidade(nomeEspecialidade);

            if (prestadores == null) return null;

            return prestadores.Select(p => PrestadorParaDTO(p));
        }

        public async Task<bool> AtualizarPrestador(int id, PrestadorDTO prestadorDTO)
        {
            Prestador prestador = await ObterPrestadorPorIdInterno(id);

            if (prestador == null) return false;

            prestador.Nome = prestadorDTO.Nome;
            prestador.Especialidades.Clear();

            foreach(EspecialidadeDTO esp in prestadorDTO.Especialidades)
                prestador.Especialidades.Add(_context.Especialidades.Find(esp.Id));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!PrestadorExists(id))
            {   
                return false;
            }

            return true;
        }

        public async Task InserirPrestador(PrestadorDTO prestadorDTO)
        {
            Prestador prestador = new()
            {
                Nome = prestadorDTO.Nome,
                Especialidades = new List<Especialidade>()
            };

            foreach(EspecialidadeDTO esp in prestadorDTO.Especialidades)
                prestador.Especialidades.Add(_context.Especialidades.Find(esp.Id));

            _context.Prestadores.Add(prestador);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExcluirPrestador(int id)
        {
            var prestador = await ObterPrestadorPorIdInterno(id);

            if (prestador == null)
                return false;

             _context.Prestadores.Remove(prestador);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool PrestadorExists(int id)
        {
            return _context.Prestadores.Any(e => e.Id == id);
        }

        private static PrestadorDTO PrestadorParaDTO(Prestador p) =>
            new PrestadorDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Especialidades = p.Especialidades?.Select(e => new EspecialidadeDTO
                {
                    Id = e.Id,
                    Nome = e.Nome
                }).ToList()
            };
    }
}