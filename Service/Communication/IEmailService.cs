using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Communication
{
    public interface IEmailService
    {
        MailMessage CreateMail(string from, string senderName, string subject, string bodyText, string to);
        Task Execute(MailMessage mail);
    }
}
