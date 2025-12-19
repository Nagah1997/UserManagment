using MailKit.Net.Smtp;
using MailKit.Security;
using MicroSlate.Service.Abstracts.Email;
using MicroSlate.Service.Helper;
using MimeKit;
using System.Net.Security;


namespace MicroSlate.Service.Implementions.Email
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region Constructors
        public EmailsService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        #endregion
        #region Handle Functions
        //public async Task<string> SendEmaile(string email, string Message, string? reason)
        //{
        //    try
        //    {
        //        //sending the Message of passwordResetLink
        //        using (var client = new SmtpClient())
        //        {
        //            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //            client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
        //            var bodybuilder = new BodyBuilder
        //            {
        //                HtmlBody = $"{Message}",
        //                TextBody = "welcome",
        //            };
        //            var message = new MimeMessage
        //            {
        //                Body = bodybuilder.ToMessageBody()
        //            };
        //            message.From.Add(new MailboxAddress("MicroSlate Team", _emailSettings.FromEmail));
        //            message.To.Add(new MailboxAddress("testing", email));
        //            message.Subject = reason == null ? "No Submitted" : reason;
        //            await client.SendAsync(message);
        //            await client.DisconnectAsync(true);
        //        }
        //        //end of sending email
        //        return "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Failed";
        //    }
        //}


        //public async Task<string> SendEmail(string toEmail, string Message, string? body)
        //{
        //    try
        //    {
        //        var email = new MimeMessage();
        //        email.From.Add(new MailboxAddress("MicroSlate Team", _emailSettings.FromEmail));
        //        email.To.Add(new MailboxAddress("", toEmail));
        //        email.Subject = Message;

        //        email.Body = new TextPart("html")
        //        {
        //            Text = body
        //        };

        //        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        //        await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, true);
        //        await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
        //        await smtp.SendAsync(email);
        //        await smtp.DisconnectAsync(true);
        //        return "Success";

        //    }
        //    catch (Exception ex)
        //    {
        //        return $"{ex.InnerException}Failed";
        //    }

        //}
        public async Task<string> SendEmail(string email, string message, string reason)
        {
            try
            {
                var mime = new MimeMessage();
                mime.From.Add(new MailboxAddress("MicroSlate Team", _emailSettings.FromEmail));
                mime.To.Add(new MailboxAddress("", email));
                mime.Subject = reason ?? "No Subject";

                var builder = new BodyBuilder
                {
                    HtmlBody = message
                };
                mime.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                    {
                        // Gmail certificate is valid; only allow revocation failure to be ignored
                        if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateChainErrors)
                            return true;

                        return sslPolicyErrors == SslPolicyErrors.None;
                    };

                    await client.ConnectAsync("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                    await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);
                    await client.SendAsync(mime);
                    await client.DisconnectAsync(true);

                    return "Success";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion
    }

}

