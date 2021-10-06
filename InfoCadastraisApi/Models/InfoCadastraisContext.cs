namespace InfoCadastraisApi.Models
{
    public class InfoCadastraisContext : DbContext
    {
        public InfoCadastraisContext(DbContextOptions<Prestador> options)
            : base(options)
        {
        }

        public DbSet<Prestador> Prestadores { get; set; }
    }
}