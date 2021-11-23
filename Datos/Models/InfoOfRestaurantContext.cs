using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Datos.Models
{
    public partial class InfoOfRestaurantContext : DbContext
    {
        public InfoOfRestaurantContext()
        {
        }

        public InfoOfRestaurantContext(DbContextOptions<InfoOfRestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consumible> Consumible { get; set; }
        public virtual DbSet<ConsumibleMenu> ConsumibleMenu { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Factura> Factura { get; set; }
        public virtual DbSet<FuncionarioRestaurante> FuncionarioRestaurante { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Mesa> Mesa { get; set; }
        public virtual DbSet<Paremetros> Paremetros { get; set; }
        public virtual DbSet<Restaurante> Restaurante { get; set; }
        public virtual DbSet<Servicio> Servicio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=InfoOfRestaurant;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Consumible>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Consumible)
                    .HasForeignKey(d => d.RestauranteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Consumible_Restaurante");
            });

            modelBuilder.Entity<ConsumibleMenu>(entity =>
            {
                entity.HasOne(d => d.Consumible)
                    .WithMany(p => p.ConsumibleMenu)
                    .HasForeignKey(d => d.ConsumibleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConsumibleMenu_Consumible");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.ConsumibleMenu)
                    .HasForeignKey(d => d.MenuID)
                    .HasConstraintName("FK_ConsumibleMenu_Menu");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.Property(e => e.CodigoDeAprobacion).IsUnicode(false);

                entity.Property(e => e.MetodoDePago).IsUnicode(false);

                entity.HasOne(d => d.Mesa)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.MesaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orden_Mesa");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.RestauranteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cuenta_Restaurante");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.Property(e => e.NombreCliente).IsUnicode(false);

                entity.Property(e => e.NombreFuncionario).IsUnicode(false);

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Factura)
                    .HasForeignKey(d => d.CuentaID)
                    .HasConstraintName("FK_Factura_Cuenta");
            });

            modelBuilder.Entity<FuncionarioRestaurante>(entity =>
            {
                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.FuncionarioRestaurante)
                    .HasForeignKey(d => d.RestauranteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Funcionario_Restaurante");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.RestauranteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menu_Restaurante");
            });

            modelBuilder.Entity<Mesa>(entity =>
            {
                entity.Property(e => e.Identificador).IsUnicode(false);

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Mesa)
                    .HasForeignKey(d => d.RestauranteID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mesa_Restaurante");
            });

            modelBuilder.Entity<Paremetros>(entity =>
            {
                entity.HasKey(e => e.ParametroID)
                    .HasName("PK__Paremetr__2B3CE6723971683F");

                entity.HasOne(d => d.Restaurante)
                    .WithMany(p => p.Paremetros)
                    .HasForeignKey(d => d.RestauranteID)
                    .HasConstraintName("FK_Parametros_Restaurante");
            });

            modelBuilder.Entity<Restaurante>(entity =>
            {
                entity.Property(e => e.Barrio).IsUnicode(false);

                entity.Property(e => e.Direccion).IsUnicode(false);

                entity.Property(e => e.Nit).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Telefono).IsUnicode(false);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.Peticiones).IsUnicode(false);

                entity.HasOne(d => d.Consumible)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.ConsumibleID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Consumible");

                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Servicio)
                    .HasForeignKey(d => d.CuentaID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Servicio_Cuenta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
