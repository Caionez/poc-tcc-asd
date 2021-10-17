using InfoCadastraisWebApp.Models;
using InfoCadastraisWebApp.DTOs;
using InfoCadastraisWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoCadastraisWebApp.Repositories
{
    public interface IAssociadoRepository
    {
        Task<IEnumerable<AssociadoDTO>> ListarAssociados();
    }

    public class AssociadoRepository : IAssociadoRepository
    {
        private readonly InfoCadastraisContext _context;

        public AssociadoRepository(InfoCadastraisContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<IEnumerable<AssociadoDTO>> ListarAssociados()
        {
            var associados = _context.Associados;

            return await associados.Select(a => AssociadoParaDTO(a))
                                    .ToListAsync();
        }

        private static AssociadoDTO AssociadoParaDTO(Associado a) =>
            new()
            {
                Id = a.Id,
                Nome = a.Nome,
                DataNascimento = a.DataNascimento,
                Ativo = a.Ativo
            };
    }
}