using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;

namespace EventPlannerApp.Pages
{
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            var name = Request.Form["name"];
            var email = Request.Form["emailaddress"];
            var message = Request.Form["message"];
            SendMail(name, email, message);
        }

        public bool SendMail(string name, string email, string message1)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            message.From = new MailAddress("rezervare@gmail.com");
            message.To.Add("sirbmaria27@gmail.com"); //aici punem email-ul unde vrem sa se trimita mail-urile (adica adresa adminului)
            message.Subject = "Test email";
            message.IsBodyHtml = true;
            message.Body = "<p>Name: " + name + "</p" + "<p>Email: " + email + "</p" + "<p>Message: " + message1 + "</p";

            smtpClient.Port = 587; //aici punem hosting provider
            smtpClient.Host = "sirbmaria27@gmail.com"; //si aici ceva cu hosting provider
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Send(message);
            return true;
        }
    }
}
