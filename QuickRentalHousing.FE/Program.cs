using ITHenry.Commons.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickRentalHousing.FE.Handlers;
using QuickRentalHousing.FE.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace QuickRentalHousing.FE
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            builder.Services.AddScoped<QuickRentalHouseApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient(builder.Configuration["QuickRentalHousing.Api:Name"],
                client => client.BaseAddress = new Uri(builder.Configuration["QuickRentalHousing.Api:BaseAddress"]))
                .AddHttpMessageHandler<QuickRentalHouseApiAuthorizationMessageHandler>();
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient(builder.Configuration["QuickRentalHousing.Api:Name"]));

            builder.Services.RegisterNonGenericClassesInAssemblyAsScoped(
                typeof(IGenderModuleService).Assembly.GetName().Name);

            await builder.Build().RunAsync();
        }
    }
}
