using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjetoLinx.webApi;
using ProjetoLinx.webApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoLinxTestes.Integracao.Setup
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Startup>, IDisposable
    {
        public ProjetoLinxContext Context { get; private set; }
        public IServiceScope Scope { get; private set; }
        public IMapper Mapper { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                var integrationAppSettings = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.Integration.json")
                    .Build();
                config.AddConfiguration(integrationAppSettings);
            });

            builder.ConfigureServices(services =>
            {
                Scope = services.BuildServiceProvider().CreateScope();
                Context = Scope.ServiceProvider.GetRequiredService<ProjetoLinxContext>();
                Mapper = Scope.ServiceProvider.GetRequiredService<IMapper>();
            });
        }

        // O código que vem aqui será executado depois de passar por todos os testes
        public new void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
            Scope.Dispose();
        }
    }
}
