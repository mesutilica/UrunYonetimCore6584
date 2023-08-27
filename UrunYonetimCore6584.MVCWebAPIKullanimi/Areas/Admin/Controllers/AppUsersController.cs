using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.MVCWebAPIKullanimi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class AppUsersController : Controller
    {
        private readonly HttpClient _httpClient;

        public AppUsersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private readonly string _apiAdres = "https://localhost:7246/api/AppUsers";

        // GET: AppUsersController
        public async Task<ActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<AppUser>>(_apiAdres);
            return View(model);
        }

        // GET: AppUsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppUsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AppUser collection)
        {
            try
            {
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

        // GET: AppUsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: AppUsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, AppUser collection)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, collection);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
            return View();
        }

        // GET: AppUsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<AppUser>(_apiAdres + "/" + id);
            return View(model);
        }

        // POST: AppUsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, AppUser collection)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
