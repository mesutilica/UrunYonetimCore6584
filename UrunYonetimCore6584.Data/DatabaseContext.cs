using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; } // veritabanı tablolarımızı temsil eden dbset ler
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Slide> Slides { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=.; database=UrunYonetimCore6584; trusted_connection=true; TrustServerCertificate=True");
            // optionsBuilder.UseSqlServer(@"server=mssql.siteadi.com; database=UrunYonetimCore6584; username=dbkullaniciadi; password=DbSifre; TrustServerCertificate=True"); // Canlı sunucuya bağlanırken
            /*
             * Uygulama çıktısı alma;
             * Mvc projesini başlangıç projesi yap
             * Mvc projesine sağ tık > Publish
             * Önce klasörü seçerek profil oluşturduk
             * Publish butonuna basarak çıktı aldık
             * Çıktıdan sonra sunucuya atılacak dosyalar
             * runtimes klasörü
             * 2 karakterli dil paketleri atılsa da atılmasada olur
             * wwwroot klasörünü sunucuya yüklüyoruz
             * klasörleri altındaki tüm dll dosyalarını ftp ile sunucuya yüklüyoruz.
             * */
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.ApplyConfiguration(new AppUserConfigurations()); // bu şekilde her yapılandırma class ını tek tek ekleyebiliriz
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // bu şekilde uygulamadaki tüm yapılandırma class larını otomatik olarak çalıştırabiliriz.
            modelBuilder.Entity<AppUser>().HasData( // HasData metodu db oluştuktan sonra db ye kayıt oluşturmak için data seed işlemi yapar
                new AppUser
                {
                    Id = 1,
                    Email = "admin@aribilgi.co",
                    Name = "Admin",
                    Password = "1236"
                }
            );
            modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Elektronik"
            },
            new Category
            {
                Id = 2,
                Name = "Bilgisayar"
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
