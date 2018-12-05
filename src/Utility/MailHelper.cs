using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Utility
{
    public class MailMessageHelper
    {
        public string Body { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string From
        {
            get
            {
                return ConfigurationSettings.AppSettings["MailFrom"];
            }
        }

        public string SMTPServer
        {
            get
            {
                return ConfigurationSettings.AppSettings["SMTPServer"];
            }
        }

        public string UserName
        {
            get
            {
                return ConfigurationSettings.AppSettings["UserName"];
            }
        }

        public string Password
        {
            get
            {
                return ConfigurationSettings.AppSettings["Password"];
            }
        }

        public int Port
        {
            get
            {
                return Convert.ToInt32(ConfigurationSettings.AppSettings["Port"]);
            }
        }
    }

    public class MailHelper
    {
        public static bool MailSend(MailMessageHelper mailMessage)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(mailMessage.SMTPServer);

                mail.From = new MailAddress(mailMessage.From);
                mail.To.Add(mailMessage.To);
                mail.Subject = mailMessage.Subject;
                mail.Body = mailMessage.Body;

                SmtpServer.Port = mailMessage.Port ;
                SmtpServer.Credentials = new System.Net.NetworkCredential(mailMessage.UserName, mailMessage.Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
