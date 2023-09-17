using Newtonsoft.Json;
using SecondTask.Interfaces;
using SecondTask.Models;
using Serilog;
using System.Net.Http;

namespace SecondTask.Services
{
    public class CountryService: ICountryService
    {
        private const string API_URL = "https://restcountries.com/v3.1";

        public async Task<List<Country>?> GetAllCountriesAsync(HttpClient httpClient)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/all?fields=name,capital,region,languages");

                if (response.IsSuccessStatusCode)
                {
                    var countries = JsonConvert.DeserializeObject<List<Country?>>(await response.Content.ReadAsStringAsync());

                    return countries;
                }

                Log.Error($"Ошибка при получении списка стран: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка при выполнении запроса: {ex.Message}");
            }

            return null;
        }

        public async Task<Country?> GetCountryByName(HttpClient httpClient, string countryName)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/name/{countryName}?fields=name,capital,region,languages");

                if (response.IsSuccessStatusCode)
                {
                    var findedCountries = JsonConvert.DeserializeObject<List<Country?>>(await response.Content.ReadAsStringAsync());

                    return findedCountries.FirstOrDefault();
                }

                Log.Error($"Ошибка при получении страны {countryName}: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Log.Error($"Ошибка при выполнении запроса: {ex.Message}");
            }
            return null;
        }
    }
}
