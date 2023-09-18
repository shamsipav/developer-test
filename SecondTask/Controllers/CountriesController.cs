using Microsoft.AspNetCore.Mvc;
using SecondTask.Interfaces;
using SecondTask.Models;
using System.Diagnostics;

namespace SecondTask.Controllers
{
    public class CountriesController : Controller
    {
        private ICountryService _countryService;
        private readonly HttpClient _httpClient;

        public CountriesController(HttpClient httpClient, ICountryService countryService)
        {
            _httpClient = httpClient;
            _countryService = countryService;
        }

        public async Task<IActionResult> Index(string sortOrder = "", string search = "")
        {
            ViewBag.SortOrder = sortOrder;
            ViewBag.Search = search;

            var countries = await _countryService.GetAllCountriesAsync(_httpClient);
            
            if (countries?.Count > 0)
            {
                countries = ApplySearchFilter(countries, search);
                countries = ApplySortOrder(countries, sortOrder);
            }

            return View(countries);
        }

        private List<Country> ApplySearchFilter(List<Country> countries, string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                return countries.Where(country => country.Name.Common.ToLower().Contains(search) || country.Name.Official.ToLower().Contains(search)).ToList();
            }

            return countries;
        }

        private List<Country> ApplySortOrder(List<Country> countries, string sortOrder)
        {
            switch (sortOrder)
            {
                case "asc":
                    return countries.OrderBy(country => country.Name.Common, StringComparer.OrdinalIgnoreCase).ToList();
                case "desc":
                    return countries.OrderByDescending(country => country.Name.Common, StringComparer.OrdinalIgnoreCase).ToList();
                default:
                    return countries;
            }
        }

        public async Task<IActionResult> Details(string countryName)
        {
            var country = await _countryService.GetCountryByName(_httpClient, countryName);
            return View(country);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}