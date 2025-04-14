using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Services.Abstract
{
    public interface IMailService
    {
        Task SendContactMailAsync(string name, string email, string subject, string message);
        Task SendBulkEventCreatedMailAsync(List<string> emails, string eventTitle,string eventContent, string eventDate, string eventTime, string eventLocation);

    }
}
