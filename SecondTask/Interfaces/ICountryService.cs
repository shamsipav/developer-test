using SecondTask.Models;

namespace SecondTask.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>?> GetAllCountriesAsync(HttpClient httpClient);
        Task<Country?> GetCountryByName(HttpClient httpClient, string countryName);
    }
}
