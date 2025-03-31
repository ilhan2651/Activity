using App.Services.Services.Abstract;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

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
        mail.To.Add(new MailboxAddress("Dİlara Faflıoğullları", "dilara.fahli@outlook.com"));

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
}
