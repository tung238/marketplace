﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using TNMarketplace.Repository.EfCore;

namespace TNMarketplace.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).UseSetting("detailedErrors", "true")
                .UseIISIntegration()
                .CaptureStartupErrors(true).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var databaseInitializer = services.GetRequiredService<IDatabaseInitializer>();
                    databaseInitializer.SeedAsync(Startup.Configuration).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogCritical("Error creating/seeding database - " + ex);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
                  WebHost.CreateDefaultBuilder(args)
                      .UseStartup<Startup>();
    }
}
