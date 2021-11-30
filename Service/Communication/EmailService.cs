using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Communication
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration config)
        {
            this.config = config;
        }

        public MailMessage CreateMail(string from, string senderName, string subject, string bodyText, string to)
        {
            MailAddress fromMail = new MailAddress(from, senderName);
            MailAddress toMail = new MailAddress(to);
            MailMessage message = new MailMessage(fromMail, toMail);

            message.IsBodyHtml = true;
            message.Body = bodyText;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            return message;
        }

        public async Task Execute(MailMessage mail)
        {
            var server = config.GetValue<string>("Smtp:Server");
            var port = config.GetValue<int>("Smtp:Port");
            var enableSsl = config.GetValue<bool>("Smtp:EmailEnableSSL");
            var username = config.GetValue<string>("Smtp:EmailSmtpUsername");
            var password = config.GetValue<string>("Smtp:EmailSmtpPassword");

            var client = new SmtpClient(server, port)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = enableSsl
            };
            var credentials = new NetworkCredential(username, password);
            client.Credentials = credentials;
            client.SendCompleted += new SendCompletedEventHandler(smtp_SendCompleted);

            try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {
                var message = e.Message;
            }
        }

        void smtp_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                throw new Exception(e.Cancelled ? "Email wasn't sent." : "Error: " + e.Error.ToString());
            }
        }
    }
}
