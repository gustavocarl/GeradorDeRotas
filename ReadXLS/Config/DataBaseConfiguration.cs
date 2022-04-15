using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadXLS.Config
{
    public class DataBaseConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static string Get()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(
                "appsettings.json",
                optional: true,
                reloadOnChange: true
                );

            Configuration = builder.Build();
            string con = Configuration["ConnectionStrings:DefaultConnection"];
            return con;

        }
    }
}
