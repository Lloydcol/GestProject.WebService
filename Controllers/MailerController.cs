using C.Tool.Mailer;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailerController : MasterController
    {
        private readonly IEmailSender _emailSender;

        public MailerController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        //[HttpGet]
        //public async void Get()
        //{
        //    var message = new Message("hilyst@gmail.com", "Test email async", " Je suis l'email de test envoyé à Xavier. ");
        //    await _emailSender.SendEmailAsync(message);
        //}

        [HttpPost]
        public async void InviteUser(string mailDest, string body)
        {
            var message = new Message(mailDest, "Nouvelle demande d'invitation", body);
            await _emailSender.SendEmailAsync(message);
        }
    }
}
