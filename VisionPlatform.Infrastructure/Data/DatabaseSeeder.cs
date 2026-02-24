using Microsoft.EntityFrameworkCore;
using VisionPlatform.Domain.Entities;
using VisionPlatform.Infrastructure.Auth;

namespace VisionPlatform.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(VisionDbContext context)
        {
            await context.Database.MigrateAsync();

            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Nome = "Administrador" },
                    new Role { Nome = "QA Junior" },
                    new Role { Nome = "QA Pleno" },
                    new Role { Nome = "QA Senior" },
                    new Role { Nome = "Gerente" }
                };

                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }

            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>
                {
                    new Permission { Nome = "User.View", Modulo = "User" },
                    new Permission { Nome = "User.Create", Modulo = "User" },
                    new Permission { Nome = "User.Update", Modulo = "User" },
                    new Permission { Nome = "User.Delete", Modulo = "User" },

                    new Permission { Nome = "Version.Create", Modulo = "Version" },
                    new Permission { Nome = "Version.Delete", Modulo = "Version" },

                    new Permission { Nome = "VersionTask.Create", Modulo = "VersionTask" },
                    new Permission { Nome = "VersionTask.Update", Modulo = "VersionTask" }
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }

            // Vincular todas permissões ao Administrador
            var adminRole = context.Roles.FirstOrDefault(r => r.Nome == "Administrador");

            if (adminRole != null && !context.RolePermissions.Any())
            {
                var allPermissions = context.Permissions.ToList();

                var rolePermissions = allPermissions.Select(p =>
                    new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = p.Id
                    }).ToList();

                context.RolePermissions.AddRange(rolePermissions);
                await context.SaveChangesAsync();
            }

            // Criar usuário admin inicial
            if (!context.Users.Any())
            {
                var hasher = new PasswordHasherService();
                var adminRoleId = context.Roles.First(r => r.Nome == "Administrador").Id;

                context.Users.Add(new User
                {
                    Nome = "Admin Vision",
                    Email = "admin@vision.com",
                    PasswordHash = hasher.HashPassword("123456"),
                    RoleId = adminRoleId,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
