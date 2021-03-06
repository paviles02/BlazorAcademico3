using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlazorAcademico.Server.Models
{
    public partial class RegistroAcaContext : DbContext
    {
        public RegistroAcaContext()
        {
        }

        public RegistroAcaContext(DbContextOptions<RegistroAcaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CuposCurso> CuposCurso { get; set; }
        public virtual DbSet<Cursos> Cursos { get; set; }
        public virtual DbSet<DetRegistroAcademico> DetRegistroAcademico { get; set; }
        public virtual DbSet<DireccionEstudiantes> DireccionEstudiantes { get; set; }
        public virtual DbSet<EncRegistroAcademcico> EncRegistroAcademcico { get; set; }
        public virtual DbSet<Estudiante> Estudiante { get; set; }
        public virtual DbSet<Facultad> Facultad { get; set; }
        public virtual DbSet<Grado> Grado { get; set; }
        public virtual DbSet<Maestros> Maestros { get; set; }
        public virtual DbSet<Seccion> Seccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DEVELOPER-HP; DataBase=RegistroAca; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CuposCurso>(entity =>
            {
                entity.HasKey(e => e.IdCuposCurso);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.Cursos)
                    .WithMany(p => p.CuposCurso)
                    .HasForeignKey(d => d.CursosId);
            });

            modelBuilder.Entity<Cursos>(entity =>
            {
                entity.HasKey(e => e.CursoId);

                entity.Property(e => e.OnLineMaestrosId).HasColumnName("onLineMaestrosId");

                entity.Property(e => e.PresencialMaestrosId).HasColumnName("presencialMaestrosID");

                //entity.HasOne(d => d.DetRegistroAcademicoDetRegistroIdAcadNavigation)
                //    .WithMany(p => p.Cursos)
                //    .HasForeignKey(d => d.DetRegistroAcademicoDetRegistroIdAcad);

                entity.HasOne(d => d.OnLineMaestros)
                    .WithMany(p => p.CursosOnLineMaestros)
                    .HasForeignKey(d => d.OnLineMaestrosId);

                entity.HasOne(d => d.PresencialMaestros)
                    .WithMany(p => p.CursosPresencialMaestros)
                    .HasForeignKey(d => d.PresencialMaestrosId);
            });

            modelBuilder.Entity<DetRegistroAcademico>(entity =>
            {
                entity.HasKey(e => e.DetRegistroIdAcad);

                entity.HasOne(d => d.EncRegistroAcademico)
                    .WithMany(p => p.DetRegistroAcademico)
                    .HasForeignKey(d => d.EncRegistroAcademicoId);
            });

            modelBuilder.Entity<DireccionEstudiantes>(entity =>
            {
                entity.HasKey(e => e.IdDireccionEstudiante);

                entity.HasOne(d => d.EstudianteIdEstudianteNavigation)
                    .WithMany(p => p.DireccionEstudiantes)
                    .HasForeignKey(d => d.EstudianteIdEstudiante);
            });

            modelBuilder.Entity<EncRegistroAcademcico>(entity =>
            {
                entity.HasKey(e => e.IdEncRegistroAcad);

                entity.HasOne(d => d.Estudiante)
                    .WithMany(p => p.EncRegistroAcademcico)
                    .HasForeignKey(d => d.EstudianteId);
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.IdEstudiante);

                entity.Property(e => e.Altura).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.CursosCurso)
                    .WithMany(p => p.Estudiante)
                    .HasForeignKey(d => d.CursosCursoId);

                entity.HasOne(d => d.FacultadRef)
                    .WithMany(p => p.Estudiante)
                    .HasForeignKey(d => d.FacultadRefId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Grado)
                    .WithMany(p => p.Estudiante)
                    .HasForeignKey(d => d.GradoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Maestros>(entity =>
            {
                entity.HasKey(e => e.MaestroId);
            });

            modelBuilder.Entity<Seccion>(entity =>
            {
                entity.HasKey(e => e.IdSeccion);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
