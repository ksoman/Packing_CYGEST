using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Take Photo  Menu
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TakePhotoMenu : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TakePhotoMenu"/> class.
        /// </summary>
        public TakePhotoMenu()
        {
            InitializeComponent();
            BindingContext = new TakePhotoViewModel(new PageService());
        }
    }
}