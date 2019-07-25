using log4net.Extensions.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace EP.SeaBattle.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .ConfigureLogging(builder => builder.ClearProviders()
                    .AddDebug()
                    .AddProvider(new log4netLogProvider(true))
                    )
                .UseStartup<Startup>();
    }
}
