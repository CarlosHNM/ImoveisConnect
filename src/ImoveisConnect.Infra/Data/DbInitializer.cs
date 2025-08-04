using ImoveisConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace ImoveisConnect.Infra.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(AppDbContext context, IServiceProvider services)
        {
            // Solução para o problema do logger com classe estática
            var logger = services.GetService<ILoggerFactory>()?.CreateLogger("DbInitializer");

            logger?.LogInformation("Iniciando inicialização do banco de dados...");

            try
            {
                // 1. Criar Perfil Master se não existir
                var perfilMaster = await context.Perfils
                    .FirstOrDefaultAsync(p => p.DsPerfilSistema == "MASTER");

                if (perfilMaster == null)
                {
                    logger?.LogInformation("Criando perfil Master...");
                    perfilMaster = new Perfil
                    {
                        DsPerfil = "Master",
                        DsPerfilSistema = "MASTER"
                    };
                    context.Perfils.Add(perfilMaster);
                    await context.SaveChangesAsync();
                    logger?.LogInformation($"Perfil Master criado com ID: {perfilMaster.PerfilId}");
                }

                // 2. Criar Menus se não existirem
                if (!await context.Menus.AnyAsync())
                {
                    logger?.LogInformation("Criando menus básicos...");

                    var menuAcesso = new Menu
                    {
                        DsMenu = "Acesso",
                        DsCaminho = "/Acesso"
                    };

                    var menuVenda = new Menu
                    {
                        DsMenu = "Venda",
                        DsCaminho = "/Venda"
                    };

                    context.Menus.AddRange(menuAcesso, menuVenda);
                    await context.SaveChangesAsync();

                    logger?.LogInformation($"Menus criados com IDs: Acesso={menuAcesso.MenuId}, Venda={menuVenda.MenuId}");
                }

                // 3. Criar Usuário Admin se não existir
                if (!await context.Usuarios.AnyAsync(u => u.Email == "admin@ImoveisConnect.com.br"))
                {
                    logger?.LogInformation("Criando usuário administrador...");

                    // Gerar hash da senha "Admin@1234"
                    var (passwordHash, passwordSalt) = CreatePasswordHash("Admin@1234");

                    perfilMaster = await context.Perfils
                        .FirstAsync(p => p.DsPerfilSistema == "MASTER");

                    var adminUser = new Usuario
                    {
                        Email = "admin@ImoveisConnect.com.br",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        Nome = "Administrador do Sistema",
                        Role = "Administrador",
                        DataCadastro = DateTime.Now,
                        DsLogin = "admin",
                        IsAtivo = true,
                        PerfilId = perfilMaster.PerfilId
                    };

                    context.Usuarios.Add(adminUser);
                    await context.SaveChangesAsync();

                    logger?.LogInformation($"Usuário admin criado com ID: {adminUser.UsuarioId}");
                }

                logger?.LogInformation("Banco de dados inicializado com sucesso!");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Erro durante a inicialização do banco de dados");
                throw;
            }
        }

        private static (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA256();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }
    }
}