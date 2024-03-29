﻿using CsvJoin.Services;
using CsvJoin.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CsvJoin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);            
            var serviceProvider = services.BuildServiceProvider();            
            await serviceProvider.GetRequiredService<Application>()
                .RunAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            AddServices(services);
            AddApplication(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services
                .AddTransient<ISqlReader, SqlReader>()
                .AddTransient<ISqlExecutor, SqlExecutor>();
        }

        private static void AddApplication(IServiceCollection services)
        {
            services.AddTransient<Application>();
        }
    }
}
