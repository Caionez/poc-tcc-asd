using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfoCadastraisWebApp.Data
{
    public class InfoCadastraisIdentityDbContext : IdentityDbContext
    {
        public InfoCadastraisIdentityDbContext(DbContextOptions<InfoCadastraisIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=app.db", options => options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
