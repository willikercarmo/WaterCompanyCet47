using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WaterCompanyCet47.Web.Models;

namespace WaterCompanyCet47.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(SendMail sendMail)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient sc = new SmtpClient();

                mail.From = new MailAddress("williker.do.carmo@formandos.cinel.pt");
                mail.To.Add(new MailAddress("williker.do.carmo@formandos.cinel.pt"));
                mail.Subject = sendMail.Subject;

                mail.IsBodyHtml = true;

                mail.Body = $"<h3><b>Pedido de Contato</b></h3><br/>" +
                    $"<b>Nome:</b> {sendMail.Name}.<br/>" +
                    $"<b>Email:</b> {sendMail.Email}.<br/><br/>" +
                    $"<b>Mensagem:</b><br/>" +
                    $"{sendMail.Message}";

                sc.Host = "smtp.office365.com";
                sc.Port = 587;

                
                sc.Credentials = new NetworkCredential("williker.do.carmo@formandos.cinel.pt", "Lisboa0220.");
                sc.EnableSsl = true;

                sc.Send(mail);

                ViewBag.Message = "Pedido enviado";

                ModelState.Clear();

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
