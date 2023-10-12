using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderExample
{
   static class EmailSender
    {
        public static void Send(string to, string message)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            NetworkCredential credential = new NetworkCredential("technopat61@gmail.com", "ihfk roiv rdxw thnb");
            smtpClient.Credentials = credential;
            MailAddress gonderen = new MailAddress("technopat61@gmail.com", "BAŞLIK");
            MailAddress alici = new MailAddress(to);
            MailMessage mail = new MailMessage(gonderen, alici);
            mail.Subject = "example";
            mail.Body=message;
            smtpClient.Send(mail);
           

        }
    }
}
