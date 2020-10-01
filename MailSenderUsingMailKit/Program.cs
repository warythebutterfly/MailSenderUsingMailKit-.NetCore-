using Microsoft.Extensions.Configuration;
using System;

namespace MailSenderUsingMailKit
{
    class Program
    {
        static void Main(string[] args)
        {

            //var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


            //IConfiguration Configuration = new ConfigurationBuilder()
            //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //        .AddEnvironmentVariables()
            //        .AddCommandLine(args)
            //        .Build();
            ////var builder = new ConfigurationBuilder()
            ////    .AddJsonFile($"appsettings.json", true, true)
            ////    .AddJsonFile($"appsettings.{environmentName}.json", true, true)
            ////    .AddEnvironmentVariables();
            ////var configuration = builder.Build();
            ////var myConnString = configuration.GetConnectionString("EmailAddress");

            //var section = Configuration.GetSection("EmailAddress");
            ////var section1 = configuration.GetValue("Onesetting");
            //Console.WriteLine(section);
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
