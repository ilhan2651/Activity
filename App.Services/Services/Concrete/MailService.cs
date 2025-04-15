using App.Services.Services.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using static System.Net.WebRequestMethods;

public class MailService : IMailService
{
    private readonly IConfiguration _config;
    public MailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendContactMailAsync(string name, string email, string subject, string message)
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(_config["MailSettings:DisplayName"], _config["MailSettings:From"]));

        mail.To.Add(new MailboxAddress("İlhan Randa", "ilhanrandakk@gmail.com"));
        mail.To.Add(new MailboxAddress("Barış Şükrü Yücedağ", "bar.sukru.fb@hotmail.com"));

        mail.Subject = $"Yeni İletişim Mesajı: {subject}";

        mail.Body = new TextPart("plain")
        {
            Text = $"Ad: {name}\nEmail: {email}\n\nMesaj:\n{message}"
        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["MailSettings:Host"], int.Parse(_config["MailSettings:Port"]), false);
        await smtp.AuthenticateAsync(_config["MailSettings:From"], _config["MailSettings:Password"]);
        await smtp.SendAsync(mail);
        await smtp.DisconnectAsync(true);
    }






    public async Task SendBulkEventCreatedMailAsync(List<string> emails,string eventTitle,string eventContent,string eventDate, string eventTime, string eventLocation)
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(_config["MailSettings:DisplayName"], _config["MailSettings:From"]));

        foreach (var email in emails)
        {
            if (!string.IsNullOrWhiteSpace(email))
                mail.Bcc.Add(MailboxAddress.Parse(email));
        }

        mail.Subject = $"Yeni Etkinlik Oluşturuldu: {eventTitle}";

        mail.Body = new TextPart("plain")
        {
            Text = $"Merhaba,\n\n" +
           $"Yeni bir etkinlik yayınlandı!\n\n" +
           $"Etkinlik Başlığı : {eventTitle}\n" +
           $"Etkinlik İçeriği : {eventContent}\n" +
           $"Etkinlik Tarihi  : {eventDate}\n" +
           $"Etkinlik Saati   : {eventTime}\n" +
           $"Etkinlik Yeri    : {eventLocation}\n\n" +
           $"Katılmak için hemen platformu ziyaret edin!\n\n" +
           "https://localhost:7006/MainPage"

        };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_config["MailSettings:Host"], int.Parse(_config["MailSettings:Port"]), false);
        await smtp.AuthenticateAsync(_config["MailSettings:From"], _config["MailSettings:Password"]);
        await smtp.SendAsync(mail);
        await smtp.DisconnectAsync(true);
    }
}
