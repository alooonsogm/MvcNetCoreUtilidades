using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;

        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult SendMail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            string user = this.configuration.GetValue<string>("MailSettings:Credentials:User");
            //Objeto para la informacio de los mails.
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            //<h1>Hola</h1>
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;
            //Recuperamos los datos para el objeto que manda el propio mail.
            string password = this.configuration.GetValue<string>("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>("MailSettings:Server:Host");
            int puerto = this.configuration.GetValue<int>("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>("MailSettings:Server:Ssl");
            bool defaulCredentials = this.configuration.GetValue<bool>("MailSettings:Server:DefaultCredentials");
            SmtpClient client = new SmtpClient();
            client.Host = host;
            client.Port = puerto;
            client.EnableSsl = ssl;
            client.UseDefaultCredentials = defaulCredentials;
            //Credenciales para el email
            NetworkCredential credentials = new NetworkCredential(user, password);
            client.Credentials = credentials;
            await client.SendMailAsync(mail);
            ViewData["MENSAJE"] = "Mnesaje enviado correctamente";
            return View();
        }
    }
}
