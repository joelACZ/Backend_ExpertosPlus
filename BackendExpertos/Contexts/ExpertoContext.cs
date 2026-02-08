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

    public virtual DbSet<User> Users { get; set; }

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
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FF9E5A80");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E46070932E").IsUnique();

            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<categoria>(entity =>
        {
            entity.HasKey(e => e.nombre).HasName("PK__categori__72AFBCC78A99D258");

            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__clientes__3213E83F85F57932");

            entity.Property(e => e.direccion).HasMaxLength(255);
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<profesionale>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__profesio__3213E83FFE1FBFAE");

            entity.Property(e => e.direccion).HasMaxLength(255);
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.especialidad).HasMaxLength(100);
            entity.Property(e => e.nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<resena>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__resenas__3213E83F3838CA01");

            entity.Property(e => e.fecha).HasColumnType("datetime");
            entity.Property(e => e.fechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.fechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.cliente).WithMany(p => p.resenas)
                .HasForeignKey(d => d.cliente_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resenas_Clientes");

            entity.HasOne(d => d.servicio).WithMany(p => p.resenas)
                .HasForeignKey(d => d.servicio_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resenas_Servicios");
        });

        modelBuilder.Entity<servicio>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__servicio__3213E83F9D6A374D");

            entity.Property(e => e.categoria).HasMaxLength(100);
            entity.Property(e => e.estado).HasMaxLength(50);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.precioBase).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.categoriaNavigation).WithMany(p => p.servicios)
                .HasForeignKey(d => d.categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Servicios_Categorias");

            entity.HasOne(d => d.profesional).WithMany(p => p.servicios)
                .HasForeignKey(d => d.profesional_id)
                .HasConstraintName("FK_Servicios_Profesionales");
        });

        modelBuilder.Entity<solicitude>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__solicitu__3213E83F5C9F05D7");

            entity.Property(e => e.estado).HasMaxLength(50);
            entity.Property(e => e.fecha).HasColumnType("datetime");
            entity.Property(e => e.fechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.fechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.nivelUrgencia).HasMaxLength(20);
            entity.Property(e => e.ubicacion).HasMaxLength(255);

            entity.HasOne(d => d.cliente).WithMany(p => p.solicitudes)
                .HasForeignKey(d => d.cliente_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitudes_Clientes");

            entity.HasOne(d => d.servicio).WithMany(p => p.solicitudes)
                .HasForeignKey(d => d.servicio_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Solicitudes_Servicios");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
