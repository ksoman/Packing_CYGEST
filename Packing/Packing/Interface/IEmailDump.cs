using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    /// <summary>
    /// Interface for Email Dump
    /// </summary>
    public interface IEmailDump
    {
        /// <summary>
        /// Sends the email dump.
        /// </summary>
        /// <param name="attachment">The attachment.</param>
        /// <returns></returns>
        Task<bool> SendEmailDump(string attachment);
    }
}
