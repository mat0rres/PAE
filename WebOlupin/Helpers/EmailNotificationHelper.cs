using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Helpers
{
    public class EmailNotificationHelper
    {
        public readonly Microsoft.Extensions.Configuration.IConfiguration configuration;

        public EmailNotificationHelper(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void NotifyByEmailForProcuctForContactToCommercials(Microsoft.AspNetCore.Http.HttpRequest request, ProductForContactRecord contactRecord, Commercial[] commercials)
        {
            using var notificationEmailMessage = BuildBaseNotificationEmailMessage(commercials);
            
            notificationEmailMessage.Subject = "A new purchase intention contact has been received";

            if (configuration["EmailNotification:SiteBaseUrl"] == null)
            {
                notificationEmailMessage.Body = string.Format("<a href=\"{0}://{1}/management/contacts/productforcontactrecords/details/{2}\">Details</a>", request.Scheme, request.Host, contactRecord.Id);
            }
            else
            {
                notificationEmailMessage.Body = string.Format("<a href=\"{0}/management/contacts/productforcontactrecords/details/{1}\">Details</a>", configuration["EmailNotification:SiteBaseUrl"], contactRecord.Id);
            }

            using var smtpClient = BuildSmtpClient();
            
            smtpClient.Send(notificationEmailMessage);
        }

        public void NotifyByEmailForCommonContactToCommercials(Microsoft.AspNetCore.Http.HttpRequest request, CommonContactRecord contactRecord, Commercial[] commercials)
        {
            using var notificationEmailMessage = BuildBaseNotificationEmailMessage(commercials);
            
            notificationEmailMessage.Subject = "A new general purpose contact has been received";

            if (configuration["EmailNotification:SiteBaseUrl"] == null)
            {
                notificationEmailMessage.Body = string.Format("<a href=\"{0}://{1}/management/contacts/commoncontactrecords/details/{2}\">Details</a>", request.Scheme, request.Host, contactRecord.Id);
            }
            else
            {
                notificationEmailMessage.Body = string.Format("<a href=\"{0}/management/contacts/commoncontactrecords/details/{1}\">Details</a>", configuration["EmailNotification:SiteBaseUrl"], contactRecord.Id);
            }

            using var smtpClient = BuildSmtpClient();
            
            smtpClient.Send(notificationEmailMessage);
        }

        private MailMessage BuildBaseNotificationEmailMessage(Commercial[] commercials)
        {
            var result = new MailMessage
            {
                From = new MailAddress(configuration["NetMail:Sender"]),
                IsBodyHtml = true
            };

            foreach (var model in commercials)
            {
                result.To.Add(new MailAddress(model.EmailAddress, model.FullName));
            }

            return result;
        }

        private SmtpClient BuildSmtpClient()
        {
            var result = new SmtpClient
            {
                Host = configuration["NetMail:Host"],
                Port = int.Parse(configuration["NetMail:Port"]),
                EnableSsl = bool.Parse(configuration["NetMail:UseSSL"]),
                UseDefaultCredentials = true
            };

            if (configuration["NetMail:Credentials"] != null)
            {
                result.UseDefaultCredentials = false;
                result.Credentials = new NetworkCredential(configuration["NetMail:Credentials:Login"], configuration["NetMail:Credentials:Password"]);
            }

            return result;
        }
    }
}
