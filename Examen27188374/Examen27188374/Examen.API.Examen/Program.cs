using Examen.API.Examen;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Examen.API.Examen.Contratos;
using Examen.API.Examen.Implementacion;
using Microsoft.Azure.Functions.Worker;
using static Microsoft.Azure.Functions.Worker.FunctionsApplicationInsightsExtensions;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication
    .ConfigureServices(services =>
    {
        var configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .Build();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<Contexto>(options => options.UseSqlServer(
                     configuration.GetConnectionString("cadenaConexion")));
        services.AddScoped<IPedidoLogic, PedidoLogic>();
    })
    .Build();

host.Run();
