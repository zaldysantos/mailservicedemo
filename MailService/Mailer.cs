using System.Net;
using System.Net.Mail;

namespace MailService
{
    public class Mailer
    {
        const string _FROM_ADDR = "no_reply@mail.svc"; // your mail service account
        const string _FROM_PWD = "N0Reply"; // your mail service account's password
        const string _SMTP_ADDR = "127.0.0.1"; // your smtp address goes here
        const int _SMTP_PORT = 123; // your smtp port number goes here

        public static void Send(string toEmail, string subject, string message)
        {
            MailMessage mail = new();
            try
            {
                mail.To.Add(new MailAddress(toEmail));
                mail.From = new MailAddress(_FROM_ADDR);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                SmtpClient smtp = new(_SMTP_ADDR, _SMTP_PORT)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = true
                };
                if (!smtp.UseDefaultCredentials) smtp.Credentials = new NetworkCredential(_FROM_ADDR, _FROM_PWD);
                if (mail.To.Count > 0 || mail.CC.Count > 0 || mail.Bcc.Count > 0) smtp.Send(mail);
            }
            catch
            {
                throw;
            }
            finally
            {
                mail.Dispose();
            }
        }
    }
}
