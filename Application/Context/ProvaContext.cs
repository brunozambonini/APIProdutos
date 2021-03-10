using Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Context
{
    public class ProvaContext : DbContext
    {
        // Comentar esse método para rodar os testes
        
        public ProvaContext(DbContextOptions<ProvaContext> options) : base(options)
        {

        }
        
        public virtual DbSet<Produtos> Produtos { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Torna o nome um tipo único
            builder.Entity<Categorias>()
                .HasIndex(x => x.Nome)
                .IsUnique();
            builder.Entity<Produtos>()
                .HasIndex(x => x.Nome)
                .IsUnique();
        }
    }
}
