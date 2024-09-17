using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TiendaLibros.API.Models;

namespace TiendaLibros.API.Data;

public partial class TiendaLibrosContext : DbContext
{
    public TiendaLibrosContext()
    {
    }

    public TiendaLibrosContext(DbContextOptions<TiendaLibrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autor> Autors { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=.;Database=TiendaLibros;User Id=sa;Password=prac!4;MultipleActiveResultSets=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_autor");

            entity.ToTable("Autor");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .HasColumnName("apellidos");
            entity.Property(e => e.Biografia)
                .HasMaxLength(300)
                .HasColumnName("biografia");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_libro");

            entity.ToTable("libro");

            entity.HasIndex(e => e.Isbn, "unq_libro_ISBN").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AutorGuid).HasColumnName("AutorGUID");
            entity.Property(e => e.Año).HasColumnName("año");
            entity.Property(e => e.CostoVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("costoVenta");
            entity.Property(e => e.Imagen)
                .HasMaxLength(100)
                .HasColumnName("imagen");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Resumen)
                .HasMaxLength(300)
                .HasColumnName("resumen");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .HasColumnName("titulo");

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorGuid)
                .HasConstraintName("fk_autor_libro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
