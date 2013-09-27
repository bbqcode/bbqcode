using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Text;
using System.IO;

namespace BBQCode.Web.Helpers
{
    public class Email
    {
        private static string SmtpHost { get { return ConfigurationManager.AppSettings["Email.SmtpHost"]; } }
        private static int SmtpPort { get { return int.Parse(ConfigurationManager.AppSettings["Email.SmtpPort"]); } }
        private static string SmtpUsername { get { return ConfigurationManager.AppSettings["Email.SmtpUsername"]; } }
        private static string SmtpPassword { get { return ConfigurationManager.AppSettings["Email.SmtpPassword"]; } }

        private static SmtpClient Smtp
        {
            get
            {
                var s = new SmtpClient(SmtpHost);
                s.Port = SmtpPort;
                s.Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword);
                s.EnableSsl = true;
                return s;
            }
        }

        private static string ContactUsFullName { get { return ConfigurationManager.AppSettings["Email.ContactUsFullName"]; } }
        private static string ContactUsEmail { get { return ConfigurationManager.AppSettings["Email.ContactUsEmail"]; } }
        private static MailAddress ContactUsMailAddress { get { return new MailAddress(ContactUsEmail, ContactUsFullName); } }

        private static bool SendMail(MailMessage message)
        {
            try
            {
                Smtp.Send(message);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool SendContactUs(string fullNameFrom, string emailFrom, string message)
        {
            var m = new MailMessage();
            m.From = new MailAddress(emailFrom, fullNameFrom);
            m.To.Add(ContactUsMailAddress);

            m.Subject = "BBQCode.com - Contact Us message from " + emailFrom;
            
            string html = GetTemplate("~/Content/email/ContactUs.html");
            html = html.Replace("##FROM_NAME##", fullNameFrom);
            html = html.Replace("##FROM_EMAIL##", emailFrom);
            html = html.Replace("##MESSAGE##", message);
            html = html.Replace("##SENT_TIME##", DateTime.Now.ToString());

            var htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
            htmlView.LinkedResources.Add(GetResource("~/Content/email/img/logo.png", "logo", "image/png"));
            m.AlternateViews.Add(htmlView);

            return SendMail(m);            
        }

        private static string GetTemplate(string path)
        {
            var html = "";
            var mappedPath = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(mappedPath))
            {
                html = File.ReadAllText(mappedPath, Encoding.UTF8);
            }
            return html;
        }

        private static LinkedResource GetResource(string path, string id, string mimeType)
        {
            LinkedResource resource = null;
            var mappedPath = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(mappedPath))
            {
                resource = new LinkedResource(mappedPath, mimeType);
                resource.ContentId = id;
            }
            return resource;
        }

    }
}