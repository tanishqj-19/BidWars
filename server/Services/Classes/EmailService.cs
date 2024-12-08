using System.Net.Mail;
using System.Net;
using server.Models.Mail;

namespace server.Services.Classes
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;


        public EmailService(IConfiguration config)
        {
            _configuration = config;

        }
        public async Task<bool> SendEmail(string to, string subject, string body)
        {
            bool emailStatus = false;
            try
            {
                GetEmailSetting getEmailSetting = new GetEmailSetting()
                {
                    SecretKey = _configuration.GetValue<string>("EmailSettings:SecretKey"),
                    From = _configuration.GetValue<string>("EmailSettings:From"),
                    SmtpServer = _configuration.GetValue<string>("EmailSettings:SmtpServer"),
                    Port = _configuration.GetValue<int>("EmailSettings:Port"),
                    EnableSSL = _configuration.GetValue<bool>("EmailSettings:EnableSSL")
                };

                MailMessage mailMessage = new MailMessage()
                {
                    From = new MailAddress(getEmailSetting.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(to);
                SmtpClient smtpClient = new SmtpClient(getEmailSetting.SmtpServer)
                {
                    Port = getEmailSetting.Port,
                    Credentials = new NetworkCredential(getEmailSetting.From, getEmailSetting.SecretKey),
                    EnableSsl = getEmailSetting.EnableSSL
                };

                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {
                emailStatus = false;
                throw new Exception(ex.Message);
            }

            return emailStatus;
        }
    }
}
