using Microsoft.EntityFrameworkCore;
using InfoCadastraisWebApp.Models;
using System.Reflection;

namespace InfoCadastraisWebApp.Data
{
    public class InfoCadastraisContext : DbContext
    {
        public InfoCadastraisContext (DbContextOptions<InfoCadastraisContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=app.db", options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
        public DbSet<Associado> Associados { get; set; }
        public DbSet<Consulta> Consulta { get; set; }
    }
}