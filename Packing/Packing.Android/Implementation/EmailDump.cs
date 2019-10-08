using System;
using System.Threading.Tasks;
using PackingCygest.Droid.Implementation;
using PackingCygest.Interface;
using Xamarin.Forms;
using System.Net.Mail;
using PackingCygest.Model;
using PackingCygest.Data;
using System.Globalization;


[assembly: Dependency(typeof(EmailDump))]
namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// Sending Email to dump all data of  the local database 
    /// This class is used by Dependency injection 
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.IEmailDump" />
    public class EmailDump : IEmailDump
    {
        /// <summary>
        /// Sends the email dump.
        /// The Catch expection will log the error in SQLlite DB
        /// The dump functionality will allow the error to  send by mail 
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        public async  Task<bool> SendEmailDump(string attachment)
        {
            bool success;

            try
            {
                //TODO adding personal email add for testing
                MailMessage mail = new MailMessage("app-PackingCygest@cygest.fr", "erreur@cygest.fr", "Dump  PackingCygest ", "");

                SmtpClient smtpclient = new SmtpClient("46.105.132.129")
                {
                    Credentials = new System.Net.NetworkCredential("app-PackingCygest@cygest.fr", "z5$!&2FnZNKS3CjJ")
                };
                mail.Attachments.Add(new Attachment(attachment));

                await smtpclient.SendMailAsync(mail);

                success = true;
            }
            catch (Exception e)
            {
                success = false;
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture), MethodName = e.Source
                });

            }

            return success;
        }


       
    }
}