using MailSenderUsingMailKit.EmailHelper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MailSenderUsingMailKit
{
    class Program
    {
        static void Main(string[] args)
        {
            Mailer testMail = new Mailer();
            var regexItem = new Regex("@.");
           
          
            Console.WriteLine("Mail Sender Using MailKit!");
           
           
            
            var isValid = true;
            while (isValid == true)
            {
                Console.Write("To: ");
                var to = Console.ReadLine();
                if (regexItem.IsMatch(to))
                {
                    Console.Write("Subject: ");
                    var subject = Console.ReadLine();
                    Console.Write("Message: ");
                    var message = Console.ReadLine();
                    var result = testMail.sendTestMail(to, subject, message).ConfigureAwait(false);
                    Console.WriteLine(result);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid email address. Try again");
                   
                }
            }

        }
    }
}
