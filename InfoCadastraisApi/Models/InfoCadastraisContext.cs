using Microsoft.EntityFrameworkCore;

namespace InfoCadastraisApi.Models
{
    public class InfoCadastraisContext : DbContext
    {
        public InfoCadastraisContext(DbContextOptions<InfoCadastraisContext> options)
            : base(options)
        {
        }

        public DbSet<Prestador> Prestadores { get; set; }
    }
}