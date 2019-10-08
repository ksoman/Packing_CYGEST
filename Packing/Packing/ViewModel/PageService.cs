using PackingCygest.Interface;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    public class PageService : IPageService
    {
        /// <summary>
        /// Displays the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ok">The ok.</param>
        /// <param name="cancel">The cancel.</param>
        /// <returns></returns>
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        /// <summary>
        /// Displays the alert.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="ok">The ok.</param>
        /// <returns></returns>
        public async Task DisplayAlert(string title, string message, string ok)
        {
             await Application.Current.MainPage.DisplayAlert(title, message, ok);
        }
        /// <summary>
        /// Pops the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task PopAsync()
        {
          await  Application.Current.MainPage.Navigation.PopAsync();
        }
        /// <summary>
        /// Removes this instance.
        /// </summary>
        /// <returns></returns>
        public async Task Remove()
        {
            var count = Application.Current.MainPage.Navigation.NavigationStack.Count;
            if (count > 0)
            {
                var pages = Application.Current.MainPage.Navigation.NavigationStack.ToList();
                // for (int i = 1; i < pages.Count; i++)
                // {

                //  Application.Current.MainPage.Navigation.RemovePage(pages[i]  );
                // }
                 for (int i = pages.Count-2; i >= 0;  i--)
                 {

                    Application.Current.MainPage.Navigation.RemovePage(pages[i]);
                 }
            }

        }
        /// <summary>
        /// Pops the modal asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }

        /// <summary>
        /// Pushes the asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task PushAsync(Page page)
        {
            Remove();
           await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        /// <summary>
        /// Pushes the modal asynchronous.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task PushModalAsync(Page page)
        {
           await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }
    }
}
