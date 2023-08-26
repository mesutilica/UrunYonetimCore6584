using System.Net; // mail için kütüphane
using System.Net.Mail;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.MVCUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync(Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient("mail.siteadresi.com", 587);
            smtpClient.Credentials = new NetworkCredential("info@siteadi.com", "mailşifresi");
            smtpClient.EnableSsl = true; // eğer gmail vb ssl sertifikalı mail kullanacaksak bunu true değilse false yapıyoruz
            MailMessage message = new MailMessage();
            message.From = new MailAddress("info@siteadi.com");
            message.To.Add("bilgi@mailingonderilecegiadres.co");
            message.To.Add("bilgi@mailingonderilecegiadres.co"); // birden fazla alıcıya da bu şekilde gönderebiliriz
            message.Subject = "Siteden mesaj geldi"; // mail konu başlığı
            message.Body = $"Mesaj Bilgileri <hr /> Adı Soyadı : {contact.Name} {contact.Surname} <br /> Email : {contact.Email} <br /> Telefon : {contact.Telephone}  <br /> Mesaj Tarihi : {contact.CreateDate}   <br /> Mesaj : {contact.Message} ";
            message.IsBodyHtml = true; // mesajda html içerikleri raw gibi yazdırmak için
            await smtpClient.SendMailAsync(message); // mesajı mail at
            smtpClient.Dispose(); // smtpClient ı bellekten at
        }
    }
}
