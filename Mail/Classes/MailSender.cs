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
using MimeKit;
using MailKit.Net.Smtp;

namespace Mail.Classes
{
    /// <summary>
    /// Класс реализующий получение данных SMTP из файла конфигурации и отправку сообщений
    /// </summary>
    public class MailSender
    {
        private readonly IConfiguration Configuration;

        public MailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Метод реализующий отправку сообщений
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="subject">Тема</param>
        /// <param name="message">Тело письма</param>
        /// <returns></returns>
        public async Task<string> SendEmailAsync(string email, string subject, string message)
        {
            //Получение данных SMTP из файла конфигурации appsetting.json
            var emailMessage = new MimeMessage();
            EmailSettings emailSettings = new EmailSettings();
            emailSettings.From = Configuration.GetValue<string>("EmailSettings:From");
            emailSettings.Username = Configuration.GetValue<string>("EmailSettings:Username");
            emailSettings.Password = Configuration.GetValue<string>("EmailSettings:Password");
            emailSettings.Host = Configuration.GetValue<string>("EmailSettings:Host");
            emailSettings.Port = Convert.ToInt32(Configuration.GetValue<string>("EmailSettings:emailPort"));

            emailMessage.From.Add(new MailboxAddress("Admin", emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            //Отправка сообщения
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(emailSettings.Host, emailSettings.Port, false);
                    await client.AuthenticateAsync(emailSettings.Username, emailSettings.Password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);

                    return "OK";
                }
                catch(Exception ex)
                {
                    //Возврат ошибки
                    return ex.ToString();
                }
            }
        }
    }
}
