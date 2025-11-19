using Microsoft.EntityFrameworkCore;
using SkillBridge.Domain;

namespace SkillBridge.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Trilha> Trilhas { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("USUARIOS");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).HasColumnName("NOME").IsRequired();
                entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired();
                entity.Property(e => e.AreaAtuacao).HasColumnName("AREA_ATUACAO");
                entity.Property(e => e.NivelCarreira).HasColumnName("NIVEL_CARREIRA");
                entity.Property(e => e.DataCadastro).HasColumnName("DATA_CADASTRO");
            });

            modelBuilder.Entity<Trilha>(entity =>
            {
                entity.ToTable("TRILHAS"); 

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).HasColumnName("NOME").IsRequired();
                entity.Property(e => e.Descricao).HasColumnName("DESCRICAO");
                entity.Property(e => e.Nivel).HasColumnName("NIVEL").IsRequired();
                entity.Property(e => e.CargaHoraria).HasColumnName("CARGA_HORARIA");
                entity.Property(e => e.FocoPrincipal).HasColumnName("FOCO_PRINCIPAL");
            });


            modelBuilder.Entity<Matricula>(entity =>
            {
                entity.ToTable("MATRICULAS");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
                entity.Property(e => e.UsuarioId).HasColumnName("USUARIO_ID");
                entity.Property(e => e.TrilhaId).HasColumnName("TRILHA_ID");
                entity.Property(e => e.DataInscricao).HasColumnName("DATA_INSCRICAO");
                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_MATRICULA_USUARIO");

                entity.HasOne(d => d.Trilha)
                    .WithMany(p => p.Matriculas)
                    .HasForeignKey(d => d.TrilhaId)
                    .HasConstraintName("FK_MATRICULA_TRILHA");
            });
        }
    }
}