using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.ViewComponents
{
    public class Slides : ViewComponent
    {
        private readonly HttpClient _httpClient;

        public Slides(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Slides/";
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _httpClient.GetFromJsonAsync<List<Slide>>(_apiAdres));
        }
    }
}
