using System;
using System.Collections.Generic;
using BackendExpertos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendExpertos.Contexts;

public partial class ExpertoContext : DbContext
{
    public ExpertoContext()
    {
    }

    public ExpertoContext(DbContextOptions<ExpertoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<categoria> categorias { get; set; }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<profesionale> profesionales { get; set; }

    public virtual DbSet<resena> resenas { get; set; }

    public virtual DbSet<servicio> servicios { get; set; }

    public virtual DbSet<solicitude> solicitudes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-LEFET2G;Database=oficiosLocales;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True;");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<categoria>(entity =>
        {
            entity.HasKey(e => e.nombre).HasName("PK__categori__72AFBCC729C9C2A8");

            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__clientes__3213E83FFA25E466");

            entity.Property(e => e.direccion).HasMaxLength(255);
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.notificacionesFormateada).HasMaxLength(50);
            entity.Property(e => e.telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<profesionale>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__profesio__3213E83FA499EBCD");

            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.especialidad).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.telefono).HasMaxLength(20);
            entity.Property(e => e.ubicacion).HasMaxLength(100);
        });

        modelBuilder.Entity<resena>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__resenas__3213E83F9EF4FB76");

            entity.HasOne(d => d.cliente).WithMany(p => p.resenas)
                .HasForeignKey(d => d.cliente_id)
                .HasConstraintName("FK_Resenas_Clientes");

            entity.HasOne(d => d.servicio).WithMany(p => p.resenas)
                .HasForeignKey(d => d.servicio_id)
                .HasConstraintName("FK_Resenas_Servicios");
        });

        modelBuilder.Entity<servicio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__servicio__3213E83F716147EB");

            entity.Property(e => e.categoria).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.precioBase).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.categoriaNavigation).WithMany(p => p.servicios)
                .HasForeignKey(d => d.categoria)
                .HasConstraintName("FK_Servicios_Categorias");

            entity.HasOne(d => d.profesional).WithMany(p => p.servicios)
                .HasForeignKey(d => d.profesional_id)
                .HasConstraintName("FK_Servicios_Profesionales");
        });

        modelBuilder.Entity<solicitude>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__solicitu__3213E83F78986589");

            entity.Property(e => e.estado).HasMaxLength(50);
            entity.Property(e => e.fecha).HasColumnType("datetime");
            entity.Property(e => e.ubicacion).HasMaxLength(255);

            entity.HasOne(d => d.cliente).WithMany(p => p.solicitudes)
                .HasForeignKey(d => d.cliente_id)
                .HasConstraintName("FK_Solicitudes_Clientes");

            entity.HasOne(d => d.profesional).WithMany(p => p.solicitudes)
                .HasForeignKey(d => d.profesional_id)
                .HasConstraintName("FK_Solicitudes_Profesionales");

            entity.HasOne(d => d.servicio).WithMany(p => p.solicitudes)
                .HasForeignKey(d => d.servicio_id)
                .HasConstraintName("FK_Solicitudes_Servicios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
