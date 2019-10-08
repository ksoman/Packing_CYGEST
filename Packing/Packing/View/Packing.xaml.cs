using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class PackingCygest
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackingCygest : ContentPage
    {
        private readonly FileInfoModel _fileInfo;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="PackingCygest"/> class.
        /// </summary>
        public PackingCygest()
        {
            InitializeComponent();
            BindingContext = new PackingCygestViewModel(new PageService(), _fileInfo);
            
        }

        /// <summary>
        /// Handles the Tapped event of the TapGestureRecognizer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
                var pack = BindingContext as PackingCygestViewModel;
                pack.BarcodePage();           
       
        }

        /// <summary>
        /// Handles the Delivery event of the TapGestureRecognizer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TapGestureRecognizer_Delivery(object sender, System.EventArgs e)
        {           
                var pack = BindingContext as PackingCygestViewModel;
                pack.BarcodeDeliveryPage();          

        }
    }
}