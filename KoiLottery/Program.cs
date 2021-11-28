using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiLottery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                    var config = builder.Build();

                    var vaultName = config["vault:name"];
                    if (string.IsNullOrEmpty(vaultName))
                    {
                        return;
                    }

                    var creds = new ChainedTokenCredential(
                        new ManagedIdentityCredential()
                    );

                    builder.AddAzureKeyVault(
                        new Uri($"https://{vaultName}.vault.azure.net/"),
                        creds
                    );
                });
    }
}
