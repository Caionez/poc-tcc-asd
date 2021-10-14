using Microsoft.EntityFrameworkCore;
using InfoCadastraisMvc.Models;

namespace InfoCadastraisMvc.Data
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
        public DbSet<Consulta> Consulta { get; set; }
    }
}