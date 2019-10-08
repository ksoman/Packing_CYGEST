using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompletedPage : ContentPage
    {

        public CompletedPage(string status)
        {
            InitializeComponent ();
            BindingContext = new CompletedViewModel(new PageService(),status);

        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

    }
}