using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BlazorTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            string log = config.GetSection("Logging").GetValue<string>("PathFormat");
            log += string.Concat("\\log-",DateTime.Now.ToString("yyyyMMdd"), ".txt");
            
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File(log)

            .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
