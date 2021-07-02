using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace QuickRentalHousing.FE.Extensions
{
    public static class HttpClientsExtension
    {
        public static HttpClient GetQuickRentalHousingApiClient(
            this IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            var result = httpClientFactory.CreateClient(configuration["QuickRentalHousing.Api:Name"]);

            return result;
        }
    }
}
