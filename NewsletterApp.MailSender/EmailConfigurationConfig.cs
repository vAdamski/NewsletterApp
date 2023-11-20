using NewsletterApp.Shared.Models.Common;

namespace NewsletterApp.MailSender;

public static class EmailConfigurationConfig
{
    public static EmailConfiguration GetEmailConfiguration()
    {
        string From = Environment.GetEnvironmentVariable("EMAIL_FROM");
        string SmtpServer = Environment.GetEnvironmentVariable("EMAIL_SMTP_SERVER");
        int SmtpPort = int.Parse(Environment.GetEnvironmentVariable("EMAIL_SMTP_PORT"));
        string Username = Environment.GetEnvironmentVariable("EMAIL_USERNAME");
        string Password = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
        
        if (From == null || SmtpServer == null || SmtpPort == 0 || Username == null || Password == null)
        {
            throw new Exception("Email configuration is not set");
        }
        
        return new EmailConfiguration
        {
            From = From,
            SmtpServer = SmtpServer,
            SmtpPort = SmtpPort,
            Username = Username,
            Password = Password
        };
    }
}