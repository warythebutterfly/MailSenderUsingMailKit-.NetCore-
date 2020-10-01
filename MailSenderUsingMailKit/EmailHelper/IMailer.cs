using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MailSenderUsingMailKit.EmailHelper
{
    public interface IMailer
    {
        Task<string> sendTestMail(string email, string subject, string message);
    }
}
