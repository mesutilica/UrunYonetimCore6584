using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.MVCWebAPIKullanimi.Utils;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/Categories/";
        // GET: CategoriesController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);
            return View(model);
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Category collection, IFormFile? Image)
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

        // GET: CategoriesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + id);
            return View(model);
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Category collection, IFormFile? Image)
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

        // GET: CategoriesController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>(_apiAdres + id);
            return View(model);
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Category collection)
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
