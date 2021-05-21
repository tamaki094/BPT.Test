using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BPT.Test.EdriAAA.API.Models
{
    public partial class BdEstudiantesContext : DbContext
    {
        public BdEstudiantesContext()
        {
        }

        public BdEstudiantesContext(DbContextOptions<BdEstudiantesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignaciones> Asignaciones { get; set; }
        public virtual DbSet<AsignacionesEstudiante> AsignacionesEstudiante { get; set; }
        public virtual DbSet<Estudiantes> Estudiantes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=BdEstudiantes;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asignaciones>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AsignacionesEstudiante>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.IdAsignacionNavigation)
                    .WithMany(p => p.AsignacionesEstudiante)
                    .HasForeignKey(d => d.IdAsignacion)
                    .HasConstraintName("FK__Asignacio__IdAsi__2B3F6F97");

                entity.HasOne(d => d.IdEstudianteNavigation)
                    .WithMany(p => p.AsignacionesEstudiante)
                    .HasForeignKey(d => d.IdEstudiante)
                    .HasConstraintName("FK__Asignacio__IdEst__2A4B4B5E");
            });

            modelBuilder.Entity<Estudiantes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });
        }
    }
}
