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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EspecialidadePrestador>()
                        .HasKey(ep => new { ep.IdEspecialidade, ep.IdPrestador });

            modelBuilder.Entity<EspecialidadePrestador>()
                        .HasOne<Prestador>(ep => ep.Prestador)
                        .WithMany(p => p.EspecialidadesPrestador)
                        .HasForeignKey(ep => ep.IdPrestador);


            modelBuilder.Entity<EspecialidadePrestador>()
                        .HasOne<Especialidade>(ep => ep.Especialidade)
                        .WithMany(e => e.EspecialidadePrestador)
                        .HasForeignKey(ep => ep.IdEspecialidade);
        }

        public DbSet<Prestador> Prestadores { get; set; }
        public DbSet<EspecialidadePrestador> EspecialidadesPrestador { get; set; }
        public DbSet<Especialidade> Especialidades { get; set; }
    }
