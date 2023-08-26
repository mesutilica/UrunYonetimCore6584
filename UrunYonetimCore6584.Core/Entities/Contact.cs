using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UrunYonetimCore6584.Core.Entities
{
    public class Contact : IEntity
    {
        public int Id { get; set; }
        [DisplayName("Ad"), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Name { get; set; }
        [DisplayName("Soyad")]
        public string? Surname { get; set; }
        [DisplayName("Telefon")]
        public string? Telephone { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Email { get; set; }
        [MinLength(10, ErrorMessage = "{0} Minimum {1} Karakter Olmalıdır!"), DisplayName("Mesaj"), Required(ErrorMessage = "{0} Boş Geçilemez!")]
        public string Message { get; set; }
        [DisplayName("Mesaj Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
