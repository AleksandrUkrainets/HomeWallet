using HomeWallet.PwaApp.HttpRepository;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeWallet.PwaApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://homewalletserver.azurewebsites.net/api/") });
            builder.Services.AddScoped<IBalanceHttpRepository, BalanceHttpRepository>();
            builder.Services.AddScoped<ICategoryHttpRepository, CategoryHttpRepository>();
            builder.Services.AddScoped<IOperationHttpRepository, OperationHttpRepository>();

            await builder.Build().RunAsync();
        }
    }
}