using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Del Item
    /// <Summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DelItem : ContentPage
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="DelItem"/> class.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="itemDelModel">The item delete model.</param>
        public DelItem (FileInfoDeliveryModel fileInfo, ItemDeliveredModel itemDelModel)
		{
			InitializeComponent ();
            BindingContext = new DelItemViewModel(new PageService(), fileInfo,itemDelModel);
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