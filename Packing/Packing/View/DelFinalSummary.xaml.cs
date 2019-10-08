using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class for Del Final Summary
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DelFinalSummary : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelFinalSummary"/> class.
        /// </summary>
        public DelFinalSummary(FileInfoDeliveryModel info,DeliveryEndControls end,bool success)
        {
            InitializeComponent();
            BindingContext = new DelFinalSummaryViewModel(new PageService(),info,end,success);

        }

        /// <summary>
        /// Handles the ItemSelected event of the ListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }

    }
}