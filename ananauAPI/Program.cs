﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ananauAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            //BuildWebHost(args).Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //public static IWebHost BuildWebHost(string[] args) =>
        //   WebHost.CreateDefaultBuilder(args)
        //       .ConfigureAppConfiguration((ctx, builder) =>
        //       {
        //           var keyVaultEndpoint = GetKeyVaultEndpoint();
        //           if (!string.IsNullOrEmpty(keyVaultEndpoint))
        //           {
        //               var azureServiceTokenProvider = new AzureServiceTokenProvider();
        //               var keyVaultClient = new KeyVaultClient(
        //                   new KeyVaultClient.AuthenticationCallback(
        //                       azureServiceTokenProvider.KeyVaultTokenCallback));
        //               builder.AddAzureKeyVault(
        //                   keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
        //           }
        //       }
        //    ).UseStartup<Startup>()
        //     .Build();
    }
}
