﻿using CsvJoin.Abstractions;
using CsvJoin.Sql;
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

            await serviceProvider.GetService<Application>().RunAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISqlPreparator, SqlPreparator>();

            services.AddTransient<ISqlExecutor, SqlExecutor>();

            services.AddTransient<ISqlSaver, SqlSaver>();

            services.AddTransient<Application>();
        }
    }
}