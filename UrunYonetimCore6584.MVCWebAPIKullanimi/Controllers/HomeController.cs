using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.MVCWebAPIKullanimi.Models;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Products/";
        private readonly string _apiMarkaAdres = "https://localhost:7246/api/Brands/";
        private readonly string _apiKategoriAdres = "https://localhost:7246/api/Categories/";

        public async Task<IActionResult> IndexAsync()
        {
            var model = new HomePageViewModel()
            {
                Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiMarkaAdres),
                Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres)
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}