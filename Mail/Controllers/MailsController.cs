using Mail.Models;
using Mail.ViewModel;
using Mail.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System;

namespace Mail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private ApplicationContext db;
        public MailsController(ApplicationContext context)
        {
            db = context;
        }

        //public MailsController()
        //{

        //}

        [HttpGet]
        public async Task<ActionResult<Models.Mail>> Get()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<ViewMail>> Post(ViewMail viewMail)
        {
            if (viewMail == null)
            {
                return NotFound();
            }

            Models.Mail mail = new Models.Mail();

            mail.Recipient = System.Web.HttpUtility.HtmlEncode(viewMail.Recipient);
            mail.Subject = System.Web.HttpUtility.HtmlEncode(viewMail.Subject);
            mail.Body = System.Web.HttpUtility.HtmlEncode(viewMail.Body);
            mail.DateTime = DateTime.Now;

            Thread t = new Thread(delegate () { MailSender.SendMail(mail.Recipient, mail.Subject, mail.Body); });
            t.Start();

            db.Mail.Add(mail);
            await db.SaveChangesAsync();

            return Ok(mail);
        }
    }
}
