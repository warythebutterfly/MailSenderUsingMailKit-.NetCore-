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
           

        }



        public async Task<string> sendTestMail(string email, string subject, string message)
        {
            try
            {
                
                    // pull in the environment variable configuration
                    var environmentConfiguration = new ConfigurationBuilder()
                        .AddEnvironmentVariables()
                        .Build();
                    var environment = environmentConfiguration["RUNTIME_ENVIRONMENT"];

                    // load the app settings into configuration
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{environment}.json", true, true)
                        .AddEnvironmentVariables()
                        .Build();
                    var settings = configuration.Get<Settings>();

                    var emailPassword = settings.AppSettings.EmailPassword;

                    //from address is your email address
                    string emailAddress = settings.AppSettings.EmailAddress;

                    //from address title is your name
                    string senderName = settings.AppSettings.SenderName;
                    if (emailAddress == null || emailPassword == null || senderName == null)
                    {
                        return "could not get environment variables from appsettings";
                    }
               
                //to address is the reciepient email
                string ToAddress = email;

                string Subject = subject;

                string body = message;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(senderName, emailAddress));
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
                        await emailClient.AuthenticateAsync(emailAddress, emailPassword);
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
