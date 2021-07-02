using Microsoft.Extensions.Configuration;
using QuickRentalHousing.FE.Extensions;
using QuickRentalHousing.Models.Genders;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace QuickRentalHousing.FE.Services
{
    public class GenderModuleService : IGenderModuleService
    {
        private const string REQUEST_URI = "gender-module/";

        private readonly HttpClient _httpClient;

        public GenderModuleService(IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.GetQuickRentalHousingApiClient(configuration);
        }

        public async Task<IEnumerable<GenderSelectionRespondModel>> GetSelectionModelsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<GenderSelectionRespondModel>>(
                REQUEST_URI + "get-selection-models");

            return result;
        }
    }

    public interface IGenderModuleService
    {
        Task<IEnumerable<GenderSelectionRespondModel>> GetSelectionModelsAsync();
    }
}
