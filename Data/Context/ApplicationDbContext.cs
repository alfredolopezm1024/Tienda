using System;
using System.Collections.Generic;
using System.Text;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteArticulo> ClienteArticulos { get; set; }
        public DbSet<Tienda> Tiendas { get; set; } 
        public DbSet<TiendaArticulo> TiendasArticulos {  get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Declara los modelos asociandolos a las tablas
            modelBuilder.Entity<Articulo>().ToTable("articulo");
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<ClienteArticulo>().ToTable("cliente_articulo");
            modelBuilder.Entity<Tienda>().ToTable("tienda");
            modelBuilder.Entity<TiendaArticulo>(entity => 
            { 
                entity.ToTable("tienda_articulo");
                entity.HasKey(ca => new { ca.TiendaId, ca.ArticuloId });

                entity.Property(e => e.TiendaId).HasColumnName("tienda_id");
                entity.Property(e => e.ArticuloId).HasColumnName("articulo_id");

            });

                


            //Relaciones Articulo
            modelBuilder.Entity<ClienteArticulo>()
                .HasOne(ca => ca.Articulo)
                .WithMany()
                .HasForeignKey(ca => ca.ArticuloId);
            modelBuilder.Entity<ClienteArticulo>()
                .HasOne(ca => ca.Cliente)
                .WithMany()
                .HasForeignKey(ca =>ca.ClienteId);


            //Relaciones Tienda
            modelBuilder.Entity<TiendaArticulo>()
                .HasOne(ta => ta.Articulo)
                .WithMany()
                .HasForeignKey(ta =>  ta.ArticuloId);
            modelBuilder.Entity<TiendaArticulo>()
                .HasOne(ta => ta.Tienda)
                .WithMany()
                .HasForeignKey(ta => ta.TiendaId);


        }

    }
}
