using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System;
using PackingCygest.Data;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class PackingCygestTypeViewModel : BaseViewModel
    {
        private IPageService _pageSercice;
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;
        private ObservableCollection<string> ListItem { get; set; }
        private string _titleItem;
        private string _btn1Name;
        private string _btn2Name;
        private string _selectedItem;
        private int _codeIndex;
        private string _barCode;
        private bool _mountItemView = true;
        private bool _umsView;
        public string LblOk { get; private set; }
        public string LblError { get; private set; }
        public string ErrorMsg { get; private set; }
        private string _lblAssignBarcode;
        private string _lblAssignAnother;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
       
        #region Properties


        /// <summary>
        /// Gets or sets the page sercice.
        /// </summary>
        /// <value>
        /// The page sercice.
        /// </value>
        public IPageService PageSercice
        {
            get => _pageSercice;
            set => _pageSercice = value;
        }

        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        /// <value>
        /// The list items.
        /// </value>
        public ObservableCollection<string> ListItems
        {
            get { return ListItem; }
            set
            {
                ListItem = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the title item.
        /// </summary>
        /// <value>
        /// The title item.
        /// </value>
        public string TitleItem
        {
            get
            {
                return _titleItem;
            }
            set
            {
                SetValue(ref _titleItem, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the BTN1.
        /// </summary>
        /// <value>
        /// The name of the BTN1.
        /// </value>
        public string Btn1Name
        {
            get
            {
                return _btn1Name;
            }
            set
            {
                SetValue(ref _btn1Name, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the BTN2.
        /// </summary>
        /// <value>
        /// The name of the BTN2.
        /// </value>
        public string Btn2Name
        {
            get
            {
                return _btn2Name;
            }
            set
            {
                SetValue(ref _btn2Name, value);
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>
        /// The selected item.
        /// </value>
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                SetValue(ref _selectedItem, value);
            }
        }

        /// <summary>
        /// Gets or sets the index of the code.
        /// </summary>
        /// <value>
        /// The index of the code.
        /// </value>
        public int CodeIndex
        {

            get
            {
                return _codeIndex;
            }
            set
            {
                SetValue(ref _codeIndex, value);
            }

        }

        /// <summary>
        /// Gets or sets the scaned bar code.
        /// </summary>
        /// <value>
        /// The scaned bar code.
        /// </value>
        public string ScanedBarCode
        {

            get
            {
                return _barCode;
            }
            set
            {
                SetValue(ref _barCode, value);
            }

        }
        #endregion

        #region ButtonCommands
        public ICommand Back { get; set; }
        public ICommand ScanCode { get; set; }
        public ICommand Btn1Clicked { get; set; }
        public ICommand Btn2Clicked { get; set; }
        public bool MountItemView { get => _mountItemView; set => _mountItemView = value; }
        public bool UMSView { get => _umsView; set => _umsView = value; }
        public string LblAssignBarcode
        {
            get
            {
                return _lblAssignBarcode;
            }
            set
            {
                SetValue(ref _lblAssignBarcode, value);
            }
        }
        public string LblAssignAnother
        {

            get
            {
                return _lblAssignAnother;
            }
            set
            {
                SetValue(ref _lblAssignAnother, value);
            }

            
        }
        private readonly DatabaseAccess _db;

        private string _barcodeError;

        public string BarCodeError
        {
            get { return _barcodeError; }
            set
            {
                SetValue(ref _barcodeError, value);
            }
        }

        private string _pressButton;

        public string PressButton
        {
            get { return _pressButton; }
            set
            {
                SetValue(ref _pressButton, value);
            }

        }


        #endregion
        public PackingCygestTypeViewModel()
        {

        }
    /// <summary>
    /// Initializes a new instance of the <see cref="PackingCygestTypeViewModel"/> class.
    /// </summary>
    /// <param name="pageService">The page service.</param>
    /// <param name="loading">The loading.</param>
    /// <param name="fileInfo">The file information.</param>
    public PackingCygestTypeViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            ListItems = new ObservableCollection<string>();
            _loading = loading;
            _fileInfo = fileInfo;         
            PageSercice = pageService;
            _db = new DatabaseAccess();
            Back = new Command(GoBack);
            GenerateButtons();
            Btn1Clicked = new Command(Btn1_Clicked);
            Btn2Clicked = new Command(Btn2_Clicked);
            if(!string.IsNullOrEmpty(loading.FileItemModel.Name))
            {
                LblAssignBarcode = loading.FileItemModel.Name;
            }

           // ScanCode = new Command(ScanBarCode);
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
        }

      

        public void HandleTranslation(string cultureInfo)
        {
            //LblAssignBarcode = Localize.GetString("LblAssignBarcode", cultureInfo);
            LblAssignAnother = Localize.GetString("LblAssignAnother", cultureInfo);       
            LblOk = Localize.GetString("lblOK", cultureInfo);
            LblError = Localize.GetString("lblError", cultureInfo);
            ErrorMsg = Localize.GetString("LblScanItemError", cultureInfo);
            BarCodeError = Localize.GetString("lblBarCodeError",cultureInfo);
            PressButton = Localize.GetString("lblPressButton",cultureInfo);
        }

            /// <summary>
            /// Goes the back.
            /// </summary>
            public void GoBack()
            {               
                PageSercice.PushAsync(new ItemSelection(_loading, _fileInfo));
            }


        /// <summary>
        /// Scans the bar code.
        /// </summary>
       


        /// <summary>
        /// Generates the buttons.
        /// </summary>
        public void GenerateButtons()
        {

            Btn1Name = PBModel.PBO.ToString();//PBO
            Btn2Name = PBModel.PBM.ToString();//PBM            

        }

        /// <summary>
        /// BTN1s the clicked.
        /// </summary>
        public void Btn1_Clicked()
        {
            LoadComment();
            _loading.FileItemModel.Packed_by = PBModel.PBO.ToString();
            // PageSercice.PushAsync(new LoadingOption(_loading, _fileInfo));

            if ((_loading.FileItemModel.Mounting_dismounting != null && _loading.FileItemModel.Mounting_dismounting.Equals("True"))
                || (_loading.FileItemModel.Item_MS != null && _loading.FileItemModel.Item_MS.Equals("True")))
            {
                PageSercice.PushAsync(new LoadingOption(_loading, _fileInfo));

            }
            else
            {
                PageSercice.PushAsync(new PhotoAndCommentPage(_loading, true, _fileInfo));

            }



        }

        /// <summary>
        /// BTN2s the clicked.
        /// </summary>
        private void Btn2_Clicked()
        {
            LoadComment();
            _loading.FileItemModel.Packed_by = PBModel.PBM.ToString();
           // PageSercice.PushAsync(new LoadingOption(_loading, _fileInfo));

            if ((_loading.FileItemModel.Mounting_dismounting != null && _loading.FileItemModel.Mounting_dismounting.Equals("True"))
               || (_loading.FileItemModel.Item_MS != null && _loading.FileItemModel.Item_MS.Equals("True")))
            {
                PageSercice.PushAsync(new LoadingOption(_loading, _fileInfo));

            }
            else
            {
                PageSercice.PushAsync(new PhotoAndCommentPage(_loading, true, _fileInfo));

            }
           
        }

        private void LoadComment()
        {
            if (_loading.Search && _loading.SelectedBarcode != null)
            {
                var newList = (_db.GetItem(_loading.SelectedBarcode));

                if (_loading.FileItemModel.Barecode.Equals(_loading.SelectedBarcode))
                {
                    //same barcode no change
                    _loading.FileItemModel.Comments_loading = newList.Comments_loading;

                }
                else
                {
                    //set comment to null ,not to show previous barcode comment
                    _loading.FileItemModel.Comments_loading = "";

                }
            }
        }

    }
}
