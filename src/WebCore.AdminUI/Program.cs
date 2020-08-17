using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System;
using System.IO;
using System.Threading.Tasks;
using WebCore.Domain.Database;
using WebCore.Domain.Entities;

namespace WebCore.AdminUI
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ConfigureLogger(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();

                    var userManager = services.GetRequiredService<UserManager<Administrator>>();

                    await DataContextSeed.SeedAsync(userManager);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .UseSerilog();

        private static void ConfigureLogger(string[] args)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true)
                 .AddCommandLine(args)
                 .Build();

            var connectionString = config.GetConnectionString("DbConnection");

            var options = new ColumnOptions();
            options.Store.Remove(StandardColumn.MessageTemplate);

            Serilog.Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Warning()
                            .WriteTo.MSSqlServer(connectionString, $"{nameof(WebCore.Domain.Entities.Log)}s", columnOptions: options)
                            .CreateLogger();
        }
    }
}
