using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ExamenAnahuacApdContext : DbContext
{
    public ExamenAnahuacApdContext()
    {
    }

    public ExamenAnahuacApdContext(DbContextOptions<ExamenAnahuacApdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuento> Cuentos { get; set; }

    public virtual DbSet<Escritor> Escritors { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=ExamenAnahuacAPD; Trusted_Connection=True; User ID=sa; Password=pass@word1; TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuento>(entity =>
        {
            entity.HasKey(e => e.IdCuento).HasName("PK__Cuento__D41FD7F498839106");

            entity.ToTable("Cuento");

            entity.Property(e => e.FechaRegistro).HasColumnType("date");
            entity.Property(e => e.NombreCuento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Portada).IsUnicode(false);
            entity.Property(e => e.TextoCuento)
                .HasMaxLength(1500)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEscritorNavigation).WithMany(p => p.Cuentos)
                .HasForeignKey(d => d.IdEscritor)
                .HasConstraintName("FK__Cuento__IdEscrit__1FCDBCEB");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Cuentos)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__Cuento__IdGenero__20C1E124");
        });

        modelBuilder.Entity<Escritor>(entity =>
        {
            entity.HasKey(e => e.IdEscritor).HasName("PK__Escritor__BFAEBFC10B7A0A41");

            entity.ToTable("Escritor");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__Genero__0F8349883865C55E");

            entity.ToTable("Genero");

            entity.Property(e => e.IdGenero).ValueGeneratedOnAdd();
            entity.Property(e => e.NombreGenero)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C001137A4");

            entity.ToTable("Rol");

            entity.Property(e => e.TipoRol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97BC8180F4");

            entity.ToTable("Usuario");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__25869641");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
