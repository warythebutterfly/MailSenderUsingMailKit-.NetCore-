using System;
using System.Collections.Generic;
using System.Text;
using MailKit;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Authentication;
using Microsoft.Extensions.Configuration;

namespace MailSenderUsingMailKit.EmailHelper
{
    public class Mailer : IMailer
    {
        private readonly IConfiguration _config;


        public Mailer()
        {

        }
        public Mailer(IConfiguration configuration)
        {
          
            _config = configuration;
            _yourEmailAddresss = _config.GetConnectionString("EmailAddress");
            _yourName = _config.GetConnectionString("SenderName");
            _yourEmailPassword = _config.GetConnectionString("EmailPassword");

        }



        private string _yourEmailAddresss;
        private string _yourName;
        private string _yourEmailPassword;

        public async Task<string> sendTestMail(string email, string subject, string message)
        {
            try
            {
                //from address is your email address in appsettings
                string fromAddress = _yourEmailAddresss;

                //from addres address title id your name
                string fromAddressTitle = _yourName;

                //to address is the reciepient email
                string ToAddress = email;

                string Subject = subject;

                string body = message;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(fromAddressTitle, fromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var emailClient = new SmtpClient())
                {
                    try
                    {
                        emailClient.SslProtocols |= SslProtocols.Tls;
                        emailClient.CheckCertificateRevocation = false;
                        await emailClient.ConnectAsync("smtp.gmail.com", 465, true);
                        await emailClient.AuthenticateAsync(_yourEmailAddresss, _yourEmailPassword);
                    }
                    catch (Exception)
                    {
                        return "Gmail server failed to authenticate or connect";

                    }

                    await emailClient.SendAsync(mimeMessage).ConfigureAwait(false);
                    await emailClient.DisconnectAsync(true).ConfigureAwait(false);

                }
                return "The mail has been sent successfully";

            }
            catch (Exception ex)
            {

                return ex.Message + " - sorry, cannot send mail.";
            }
        }
    }
}
