using System.Threading.Tasks;
using Xamarin.Forms;

namespace PackingCygest.Interface
{
    /// <summary>
    /// interface IPageService
    /// </summary>
    public interface IPageService
    {
        /// <summary>
        /// Pushes the asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        Task PushAsync(Page page);

        /// <summary>
        /// Displays the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ok">The ok.</param>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);

        /// <summary>
        /// Displays the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ok">The ok.</param>
        /// <returns></returns>
        Task DisplayAlert(string title, string message, string ok);

        /// <summary>
        /// Pops the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task PopAsync();

        /// <summary>
        /// Pushes the modal asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        Task PushModalAsync(Page page);

        /// <summary>
        /// Pops the modal asynchronous.
        /// </summary>
        /// <returns></returns>
        Task PopModalAsync();

        Task Remove();
    }
}
