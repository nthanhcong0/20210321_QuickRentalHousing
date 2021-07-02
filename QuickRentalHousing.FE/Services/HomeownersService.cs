using Microsoft.Extensions.Configuration;
using QuickRentalHousing.FE.Extensions;
using QuickRentalHousing.Models.Homeowners;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace QuickRentalHousing.FE.Services
{
    public class HomeownersService : IHomeownersService
    {
        private const string REQUEST_URI = "homeowners/";

        private readonly HttpClient _httpClient;

        public HomeownersService(IConfiguration configuration,
            IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.GetQuickRentalHousingApiClient(configuration);
        }

        public async Task CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            string addressNumber,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description)
        {
            var model = new CreateHomeownerRequestModel();
            model.FirstName = firstName;
            model.MiddleName = middleName;
            model.LastName = lastName;
            model.GenderId = genderId;
            model.PID = pid;
            model.DOB = dob;
            model.AddressNumber = addressNumber;
            model.StreetName = streetName;
            model.DistrictId = districtId;
            model.PhoneNumbers = phoneNumbers;
            model.Emails = emails;
            model.Description = description;

            var response = await _httpClient.PostAsJsonAsync(REQUEST_URI, model);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<HomeownerRespondModel>> LoadAllAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<HomeownerRespondModel>>(REQUEST_URI);

            return result;
        }

        public async Task<HomeownerDetailRespondModel> LoadAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<HomeownerDetailRespondModel>(REQUEST_URI + id);

            return result;
        }

        public async Task UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            string addressNumber,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description)
        {
            var model = new UpdateHomeownerRequestModel();
            model.Id = id;
            model.FirstName = firstName;
            model.MiddleName = middleName;
            model.LastName = lastName;
            model.GenderId = genderId;
            model.PID = pid;
            model.DOB = dob;
            model.AddressNumber = addressNumber;
            model.StreetName = streetName;
            model.DistrictId = districtId;
            model.PhoneNumbers = phoneNumbers;
            model.Emails = emails;
            model.Description = description;

            var response = await _httpClient.PutAsJsonAsync(REQUEST_URI, model);
            response.EnsureSuccessStatusCode();
        }

        public async Task RemoveAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync(REQUEST_URI + id);
            response.EnsureSuccessStatusCode();
        }
    }

    public interface IHomeownersService
    {
        Task CreateAsync(string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            string addressNumber,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description);
        Task<IEnumerable<HomeownerRespondModel>> LoadAllAsync();
        Task<HomeownerDetailRespondModel> LoadAsync(Guid id);
        Task UpdateAsync(Guid id,
            string firstName,
            string middleName,
            string lastName,
            int genderId,
            string pid,
            DateTime dob,
            string addressNumber,
            string streetName,
            int districtId,
            IEnumerable<string> phoneNumbers,
            IEnumerable<string> emails,
            string description);
        Task RemoveAsync(Guid id);
    }
}
