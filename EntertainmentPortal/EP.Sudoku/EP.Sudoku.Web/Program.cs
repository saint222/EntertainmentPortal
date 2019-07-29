using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace EP.Sudoku.Web
{
    /// <summary>
    /// Class Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Method Main
        /// </summary>
        public static void Main(string[] args)
        {            
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method CreateWebHostBuilder
        /// </summary>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>();           
    }
}
