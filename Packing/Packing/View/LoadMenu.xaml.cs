using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// class Load menu
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadMenu : ContentPage
    {
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileinfo;
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadMenu"/> class.
        /// </summary>
        public LoadMenu()
        {
            InitializeComponent();
            BindingContext = new LoadMenuViewModel();

    }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadMenu"/> class.
        /// </summary>
        /// <param name="barCodeLoading">The bar code loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadMenu(NewLoadingModel barCodeLoading , FileInfoModel fileInfo)
        {
            InitializeComponent();
            _loading = barCodeLoading;
            _fileinfo = fileInfo;
            BindingContext = new LoadMenuViewModel(new PageService(),barCodeLoading, fileInfo);
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }


        /// <summary>
        /// Handles the ItemSelectedItems event of the listView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void ListView_ItemSelectedItems(object sender, SelectedItemChangedEventArgs e)
        {

            listView.SelectedItem = null;

        }

        /// <summary>
        /// Handles the ItemTapped event of the listView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemTappedEventArgs"/> instance containing the event data.</param>
        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var items = e.Item as PackItemModel;
            _loading.Search = true;
            if (items != null)
            {
                _loading.SelectedData = items.RoomName;
                _loading.SelectedBarcode = items.BarCode;
                _loading.FileItemModel.Barecode= items.BarCode;
                _loading.FileItemModel.Comments_loading = items.Comments_loading;
                if (_fileinfo.Info_Cubage)
                {
                    _loading.SelectedIdRoom = items.IdRoomFile;
                    _loading.SelectedIdFileItem = items.IdFileItem;
                    _loading.Rubric = new RubricsModel();
                    _loading.Rubric.IdRubric = items.IdRubrics;
                    _loading.Rubric.Name_en = items.RubricsName;
                }
                else
                {
                    _loading.SelectedIdRoom = items.IdRoom;
                    _loading.SelectedIdItem = items.IdItem;
                    _loading.Rubric = new RubricsModel();
                    _loading.Rubric.IdRubric = items.IdRubrics;
                    _loading.Rubric.Name_en = items.RubricsName;
                }
            }

            Navigation.PushAsync(new ItemSelection(_loading, _fileinfo));
            listView.SelectedItem = null;
        }
    }
}