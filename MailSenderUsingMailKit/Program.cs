using MailSenderUsingMailKit.EmailHelper;
using Microsoft.Extensions.Configuration;
using System;

namespace MailSenderUsingMailKit
{
    class Program
    {
        static void Main(string[] args)
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

            // parse all settings into the Settings class structure
            var settings = configuration.Get<Settings>();

            //retrieve any of your keys like this
            Console.WriteLine(settings.AppSettings.FirstSetting);


            Console.WriteLine("Mail Sender Using MailKit!");

            EmailHelper.Mailer testMail = new EmailHelper.Mailer();
            Console.Write("To: ");
            var to = Console.ReadLine();
            Console.Write("Subject: ");
            var subject = Console.ReadLine();
            Console.Write("Message: ");
            var message = Console.ReadLine();
            var result = testMail.sendTestMail(to, subject, message).ConfigureAwait(false);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
