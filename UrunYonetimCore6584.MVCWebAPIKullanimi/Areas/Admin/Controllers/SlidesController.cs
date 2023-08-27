using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.MVCWebAPIKullanimi.Utils;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class SlidesController : Controller
    {
        private readonly HttpClient _httpClient;

        public SlidesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Slides/";
        // GET: SlidesController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Slide>>(_apiAdres);
            return View(model);
        }

        // GET: SlidesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlidesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlidesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Slide collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);
                }
                var response = await _httpClient.PostAsJsonAsync(_apiAdres, collection);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: SlidesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slide>(_apiAdres + id);
            return View(model);
        }

        // POST: SlidesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Slide collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image);
                }
                var response = await _httpClient.PutAsJsonAsync(_apiAdres + id, collection);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: SlidesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slide>(_apiAdres + id);
            return View(model);
        }

        // POST: SlidesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Slide collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
