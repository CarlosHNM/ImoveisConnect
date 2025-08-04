using ImoveisConnect.Domain.Interfaces;
using ImoveisConnect.Domain.Interfaces.Services;
using ImoveisConnect.Domain.Services;
using ImoveisConnect.Infra.Data;
using System.Diagnostics.CodeAnalysis;

namespace ImoveisConnect.API.Core.Extensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Injeção de dependencia dos servicos do domínio
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void UseDomainServices([NotNull] this IServiceCollection serviceCollection)
        {
            //[INJEÇÃO DE DEPENDÊNCIA]
            // Registro do UnitOfWork
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            //injeta os services
            serviceCollection.AddScoped<IAccountService, AccountService>();
            serviceCollection.AddScoped<IClienteService, ClienteService>();
            serviceCollection.AddScoped<IApartamentoService, ApartamentoService>();
            serviceCollection.AddScoped<IVendaService, VendaService>();
        }
    }
}
