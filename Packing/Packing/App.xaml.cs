using PackingCygest.Data;
using Xamarin.Forms;

namespace PackingCygest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DatabaseAccess dataAccess = new DatabaseAccess();

            if (dataAccess.HasConfigData())
            {
                MainPage = new NavigationPage(new View.PackingCygest());
            }
            else
            {
                MainPage = new NavigationPage(new View.ConfigurationPage());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
