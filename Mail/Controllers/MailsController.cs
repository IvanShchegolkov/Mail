using Mail.Models;
using Mail.ViewModel;
using Mail.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Mail.Controllers
{
    /// <summary>
    /// Добавлен API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        /// <summary>
        /// Добавлен контекст данных 
        /// </summary>
        private readonly ApplicationContext db;
        private readonly IConfiguration Configuration;
        public MailsController(ApplicationContext context, IConfiguration configuration)
        {
            db = context;
            Configuration = configuration;
        }
        /// <summary>
        /// Get запрос, возвращающий данные из базы данных в формате json
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(JsonConvert.SerializeObject(await db.Mail.ToListAsync()));
        }
        /// <summary>
        /// Post запрос, принимает параметры, обрабатывает, 
        /// вызывает отправку сообщений и производит запись дынных в базу
        /// </summary>
        /// <param name="viewMail">Аргумент, принимающий данные</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ViewMail>> Post(ViewMail viewMail)
        {
            if (viewMail == null)
            {
                return NotFound();
            }

            //Создание объекта класса Mail
            Models.Mail mail = new Models.Mail();

            //Присвоение данных
            mail.Recipient = System.Web.HttpUtility.HtmlEncode(viewMail.Recipient);
            mail.Subject = System.Web.HttpUtility.HtmlEncode(viewMail.Subject);
            mail.Body = System.Web.HttpUtility.HtmlEncode(viewMail.Body);
            mail.DateTime = DateTime.Now;
            mail.Result = "OK";

            //Создание объекта класса MailSender для отправки сообщений
            MailSender mailSender = new MailSender(Configuration);

            //Получение результата
            var res = await mailSender.SendEmailAsync(mail.Recipient, mail.Subject, mail.Body);

            //Проверка результата
            if(res != mail.Result)
            {
                mail.Result = "Failed";
                mail.FailedMessange = res;
            }

            //Запись данных в базу данных
            db.Mail.Add(mail);
            await db.SaveChangesAsync();

            return Ok();
        }
    }
}
