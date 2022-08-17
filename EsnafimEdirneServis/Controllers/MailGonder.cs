using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EsnafimEdirneServis.Controllers
{
    public class MailGonder
    {
        public void SendEmail(string mail_adres, string userName,string onaykodu,string metin, string title, string url, string description)
        {
            string body = this.PopulateBody(userName,onaykodu,metin, title, url, "");
            this.SendHtmlFormattedEmail(mail_adres, "recipient@gmail.com", "New article published!", body);
        }

        private string PopulateBody(string userName,string onaykodu,string metin, string title, string url, string description)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Views/Home/EmailTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", userName).Replace("{onaykodu}",onaykodu).Replace("{metin}",metin);
            return body;
        }

        private void SendHtmlFormattedEmail(string mail_adres, string recepientEmail, string subject, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("ersay@catsbilisim.net", "Edirne Esnafım");
                    mailMessage.Subject = "Edirne Esnafım' a Hoşgeldiniz.";
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.To.Add(mail_adres.ToString());
                    mailMessage.Priority = MailPriority.High;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "webmail.catsbilisim.net";
                    smtp.EnableSsl = false;
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "ersay@catsbilisim.net";
                    NetworkCred.Password = "Balkes10@";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);
                }
            }
            catch (Exception)
            {

                
            }
            
        }
    }
}