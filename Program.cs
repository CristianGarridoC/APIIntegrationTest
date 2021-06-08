using System;
using System.Threading.Tasks;
using APIIntegrationTest.Api;
using APIIntegrationTest.Output;
using APIIntegrationTest.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;

namespace APIIntegrationTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceProvider(configuration);
            var app = serviceProvider.GetRequiredService<PokeSearchApplication>();
            await app.RunAsync(args);
        }
        
        private static IConfigurationRoot BuildConfiguration()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            return configuration;
        }

        private static ServiceProvider BuildServiceProvider(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(configuration, services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<PokeSearchApplication>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            services.AddSingleton<IPokeSearchService, PokeSearchService>();
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddRefitClient<IPokeApi>()
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new []
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                }))
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(configuration["PokeApi:BaseAddress"]);
                });
        }
    }
}