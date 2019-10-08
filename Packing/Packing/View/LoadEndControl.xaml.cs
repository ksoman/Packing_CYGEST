using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Delivery ControlPage
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadEndControl : ContentPage
    {
        private NewLoadingModel _loading;
        private FileInfoModel _fileInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadEndControl"/> class.
        /// </summary>
        public LoadEndControl()
        {
            InitializeComponent();          

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadEndControl"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadEndControl(NewLoadingModel loading, FileInfoModel fileInfo)
        {
            InitializeComponent();
            _loading = loading;
            _fileInfo = fileInfo;
            BindingContext = new LoadEndControlViewModel(new PageService(), loading, fileInfo);
        }

        /// <summary>
        /// Handles the ItemSelected event of the ListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            controlListView.SelectedItem = null;

        }

        /// <summary>
        /// Handles the ItemTapped event of the ListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs"/> instance containing the event data.</param>
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as LoadingEndControlModel;
            if(item.Index == 1)
            {
                 Navigation.PushAsync(new MissingBarcodePage(_loading,_fileInfo));

            }
            else
            {
                //do nothing
            }

        }
    }
}