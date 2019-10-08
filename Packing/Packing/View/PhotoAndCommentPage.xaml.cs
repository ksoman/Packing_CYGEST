using PackingCygest.Model;
using PackingCygest.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Photo and Comments
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoAndCommentPage : ContentPage
    {
        private readonly ItemDeliveredModel _itemModel;
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;

        private bool _appear;

        public bool Appear
        {
            get { return _appear; }
            set { _appear = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAndCommentPage"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="appear">if set to <c>true</c> [appear].</param>
        /// <param name="fileInfo">The file information.</param>
        public PhotoAndCommentPage(NewLoadingModel loading, bool appear , FileInfoModel fileInfo)
        {
            InitializeComponent();
            _loading = loading;
            _fileInfo = fileInfo;
            CallLabel(appear,true);
            Appear = appear;
            BindingContext = new PhotoAndCommentViewModel( new PageService(),_loading, _fileInfo, "loading");
            if (!string.IsNullOrEmpty(_loading.FileItemModel.Comments_loading))
            {
                MyEditor.Text = _loading.FileItemModel.Comments_loading;
            }
            else
            {
               MyEditor.Text = "";
            }

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAndCommentPage"/> class.
        /// </summary>
        public PhotoAndCommentPage(FileInfoDeliveryModel fileInfo, ItemDeliveredModel cartonModel)
        {
            InitializeComponent();
            _itemModel = cartonModel;
            CallLabel(true,false);
            Appear = true;
            BindingContext = new PhotoAndCommentViewModel(new PageService(), fileInfo, cartonModel, "delivery");
            if (!string.IsNullOrEmpty(_itemModel.comments_Delivery))
            {
                MyEditor.Text = _itemModel.comments_Delivery;
            }
            else
            {
                MyEditor.Text = "";
            }
            Appear = false;


        }



        /// <summary>
        /// Calls the label.
        /// </summary>
        /// <param name="appear">if set to <c>true</c> [appear].</param>
        public void CallLabel(bool appear,bool isLoading)
        {
           // stackSave.IsEnabled = appear;
            stackSave.IsVisible = appear;
            BtnSave.IsVisible = appear;
            BtnSave.IsEnabled = appear;
            if (isLoading)
            {
                stackLabel.IsEnabled = appear;
                stackLabel.IsVisible = appear;
                LabelBarcode.IsVisible = appear;

                if (_loading.FileItemModel.Barecode != null)
                {
                    LabelBarcode.Text = _loading.FileItemModel.Barecode;
                }
                else
                {
                    LabelBarcode.Text = "Barcode Missing!";
                }
            }
         
            
        }

        /// <summary>
        /// Handles the Completed event of the MyEditor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MyEditor_Completed(object sender, EventArgs e)
        {
            var text = ((Editor)sender).Text;
            var photo = BindingContext as PhotoAndCommentViewModel;
            photo.GetComments(text);
         

        }

      
    }
}