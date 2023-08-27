using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public Categories(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Categories/";
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres));
        }
    }
}
