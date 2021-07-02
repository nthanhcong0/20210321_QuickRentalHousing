using Microsoft.Extensions.Configuration;
using QuickRentalHousing.FE.Extensions;
using QuickRentalHousing.Models.Districts;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace QuickRentalHousing.FE.Services
{
    public class DistrictModuleService : IDistrictModuleService
    {
        private const string REQUEST_URI = "district-module/";

        private readonly HttpClient _httpClient;

        public DistrictModuleService(IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.GetQuickRentalHousingApiClient(configuration);
        }

        public async Task<IEnumerable<DistrictSelectionRespondModel>> GetSelectionModelsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<DistrictSelectionRespondModel>>(
                REQUEST_URI + "get-selection-models");

            return result;
        }
    }

    public interface IDistrictModuleService
    {
        Task<IEnumerable<DistrictSelectionRespondModel>> GetSelectionModelsAsync();
    }
}
