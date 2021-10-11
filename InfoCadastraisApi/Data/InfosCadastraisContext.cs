using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InfoCadastraisApi.Models;

    public class InfosCadastraisContext : DbContext
    {
        public InfosCadastraisContext (DbContextOptions<InfosCadastraisContext> options)
            : base(options)
        {
        }

        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<EspecialidadePrestador> EspecialidadesPrestador { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
    }
