using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;

namespace VisionPlatform.Infrastructure.Data
{
    public class VisionDbContext : DbContext
    {
        public VisionDbContext(DbContextOptions<VisionDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ReleaseVersion> Versions { get; set; }
        public DbSet<VersionTask> VersionTasks { get; set; }
        public DbSet<TestEvidence> TestEvidences { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Configuração de Enums (Conversão para String no MySQL) ---

            modelBuilder.Entity<ReleaseVersion>()
                .Property(e => e.StatusVersao)
                .HasConversion<string>()
                .HasMaxLength(50);

            modelBuilder.Entity<VersionTask>(entity =>
            {
                entity.Property(e => e.StatusPlanejamento)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .HasConversion<string>()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TestEvidence>()
                .Property(e => e.Tipo)
                .HasConversion<string>()
                .HasMaxLength(30);

            // --- Relacionamentos e Chaves Estrangeiras ---

            modelBuilder.Entity<VersionTask>()
                .HasOne(v => v.UsuarioMerge)
                .WithMany()
                .HasForeignKey(v => v.QuemFezMerge)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VersionTask>()
                .HasOne(v => v.QaUser)
                .WithMany()
                .HasForeignKey(v => v.QaUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany()
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.User)
                .WithMany()
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UserPermission>()
                .HasOne(up => up.Permission)
                .WithMany()
                .HasForeignKey(up => up.PermissionId);
        }
    }
}