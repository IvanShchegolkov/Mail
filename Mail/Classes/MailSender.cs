using Mail.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Classes
{
    public class MailSender
    {
        private readonly IConfiguration Configuration;

        public MailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string SendMail(string email, string subject, string body)
        {
            //var mailAddress = new MailAddress(ConfigurationManager.AppSettings["SmtpAddress"], ConfigurationManager.AppSettings["SmtpDisplayName"], Encoding.UTF8);

            var test = Configuration["EmailSettings"];

            var mailAddress = new MailAddress(Configuration["EmailSettings"],
                Configuration["EmailSettings"], Encoding.UTF8);
            MailMessage message = new MailMessage(mailAddress, new MailAddress(email))
            {
                Subject = subject,
                BodyEncoding = Encoding.UTF8,
                Body = body,
                IsBodyHtml = true,
                SubjectEncoding = Encoding.UTF8
            };
            return Send(message);
        }

        private string Send(MailMessage message)
        {
            try
            {
                //SmtpClient client = new SmtpClient
                //{
                //    Host = ConfigurationManager.AppSettings["SmtpServer"],
                //    Port = 25,
                //    UseDefaultCredentials = false,
                //    //EnableSsl = false,
                //    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SmtpUser"], ConfigurationManager.AppSettings["SmtpPassword"]),
                //    DeliveryMethod = SmtpDeliveryMethod.Network
                //};
                //client.SendCompleted += (s, e) => {
                //    client.Dispose();
                //    message.Dispose();
                //};
                //client.Send(message);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
