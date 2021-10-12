using Microsoft.EntityFrameworkCore;
using InfoCadastraisApi.Models;

namespace InfoCadastraisApi.Data
{
    public class InfosCadastraisContext : DbContext
    {
        public InfosCadastraisContext (DbContextOptions<InfosCadastraisContext> options)
            : base(options)
        {
        }

        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Associado> Associados { get; set; }
    }
}