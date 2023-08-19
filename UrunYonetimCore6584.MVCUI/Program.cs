using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Service.Abstract;
using UrunYonetimCore6584.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies; // oturum açma için gerekli kütüphane
using System.Security.Claims; // oturum için 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient(typeof(IService<>), typeof(Service<>)); // veritabaný iþlerini yapacak olan yazdýðýmýz servisi uygulamaya tanýttýk

//builder.Services.AddTransient<IProductService, ProductService>(); // 1. yazým türü
builder.Services.AddTransient(typeof(IProductService), typeof(ProductService)); // 2. yazým türü. ürün yönetimi için yaptýðýmýz özel servisi ekledik, bunu eklemezsek uygulama hata verir!
builder.Services.AddTransient<ICategoryService, CategoryService>();
// Uygulamaya Servis eklemede 3 farklý yöntem var
/*
 * builder.Services.AddTransient : Bu yöntem eðer kullanýmda nesne varsa onu kullanýr yoksa yeni nesne oluþturur.
 * builder.Services.AddSingleton : Bu yöntem uygulama çalýþtýðýnda nesneyi 1 kez oluþturur ve her istekte ayný nesneyi döndürür
 * builder.Services.AddScoped : Bu yöntemde servise gelen her istekte yeni nesne oluþturulur
 * */

// Oturum açma servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Admin/Login";
    x.LogoutPath = "/Admin/Logout";
    x.AccessDeniedPath = "/AccessDenied";
    x.Cookie.Name = "AdminLogin";
    x.Cookie.MaxAge = TimeSpan.FromDays(1); // oluþacak cookie nin yaþam süresi
    x.Cookie.IsEssential = true;
});

// Oturum açan kullanýcýnýn yetki kontrolü servisi
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

app.UseAuthentication(); // dikkat önce UseAuthentication gelecek
app.UseAuthorization();

app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
