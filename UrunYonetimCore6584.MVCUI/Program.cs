using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;
using UrunYonetimCore6584.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // oturum a�ma i�in gerekli k�t�phane
using System.Security.Claims; // oturum i�in 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // veritaban� i�lerini yapacak olan yazd���m�z servisi uygulamaya tan�tt�k

//builder.Services.AddTransient<IProductService, ProductService>(); // 1. yaz�m t�r�
builder.Services.AddTransient(typeof(IProductService), typeof(ProductService)); // 2. yaz�m t�r�. �r�n y�netimi i�in yapt���m�z �zel servisi ekledik, bunu eklemezsek uygulama hata verir!
builder.Services.AddTransient<ICategoryService, CategoryService>();
// Uygulamaya Servis eklemede 3 farkl� y�ntem var
/*
 * builder.Services.AddTransient : Bu y�ntem e�er kullan�mda nesne varsa onu kullan�r yoksa yeni nesne olu�turur.
 * builder.Services.AddSingleton : Bu y�ntem uygulama �al��t���nda nesneyi 1 kez olu�turur ve her istekte ayn� nesneyi d�nd�r�r
 * builder.Services.AddScoped : Bu y�ntemde servise gelen her istekte yeni nesne olu�turulur
 * */

// Oturum a�ma servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "AdminLogin";
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // olu�acak cookie nin ya�am s�resi
    x.Cookie.IsEssential = true;
});

// Oturum a�an kullan�c�n�n yetki kontrol� servisi
builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User"));
});

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // dikkat �nce UseAuthentication gelecek
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
