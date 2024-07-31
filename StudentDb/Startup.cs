using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using StudentDb.Models;

[assembly: FunctionsStartup(typeof(LearningDb.Startup))]

namespace LearningDb
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = config["Values:SqlConnectionString"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString", "Connection string 'SqlConnectionString' is null or empty.");
            }

            builder.Services.AddDbContext<StudentDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
