using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PizzeriaStorageApp
{
    public class Program
    {
        public static bool Autorized = false;

        public static void Main(string[] args)
        {
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
