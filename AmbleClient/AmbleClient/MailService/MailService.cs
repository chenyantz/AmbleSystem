using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace AmbleClient.MailService
{
    public static class MailService
    {
        static MailAddress from = new MailAddress("system@ambleasia.com", "AmbleInfo System");

        public static void SendMail(List<string> receiverAddress, List<string> ccAddress,string subject, string content)
        {
            MailMessage mail = new MailMessage();
            mail.From = from;

            foreach (string revAddr in receiverAddress)
            {
                if (string.IsNullOrWhiteSpace(revAddr))
                    continue;
                mail.To.Add(revAddr);
            }
            foreach (string ccAddr in ccAddress)
            {
                if (string.IsNullOrWhiteSpace(ccAddr))
                    continue;
                mail.CC.Add(ccAddr);
            }

            if (mail.To.Count == 0)
            {
                return;
            }


            mail.Subject = subject;
            mail.Body = content;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            try
            {

                SmtpClient client = new SmtpClient();
                client.Host = "smtpcom.263xmail.com";
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("system@ambleasia.com", "System1234");

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

            }

        }




    }
}
