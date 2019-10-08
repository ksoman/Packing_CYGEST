using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{


    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class DelControlViewModel : BaseViewModel
    {
        public IPageService PageService { get; set; }
        public ICommand Back { get; set; }
        public ICommand ScanBtn { get; set; }
        public ICommand GoToSynchronation { get; set; }
        readonly DatabaseAccess _databaseAccess;
        List<ItemDeliveredModel> _item;
        private readonly FileInfoDeliveryModel _fileInfo;
        private string _myImagePath;
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        private List<ListViewRubricModel> _listRubricDetails;


        /// <summary>
        /// Initializes a new instance of the <see cref="DelControlViewModel"/> class.
        /// </summary>
        private string _lblFileNumber;
        private string _lblClientNumber;
        private string _volume;
        private string _loadingCountry;
        private string _destinationCountry;
        private string _barcode;
        private int _numOfItem;
        private string _lblControl;
        private string _lblNumItem;
        private string _lblFile;
        private string _lblClient;
        private string _lblSize;
        private string _lblEnd;
        private string _titleWrongBarcode;
        private string _messageScanValidBarcode;
        private string _titleAlreadyScannedBarcode;
        private ObservableCollection<ListViewItemModel> _items;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        public string PathSvgFF { get; set; }
        public string LanguageSelected;

        public string TitleWrongBarcode
        {
            get { return _titleWrongBarcode; }
            set { SetValue(ref _titleWrongBarcode, value); }
        }

        private BarCodeScannerViewModel _barCodeScannerViewModel;

        public BarCodeScannerViewModel BarCodeScannerViewModel
        {
            get { return _barCodeScannerViewModel; }
            set { SetValue(ref _barCodeScannerViewModel, value); }
        }



        public string TitleAlreadyScannedBarcode
        {
            get { return _titleAlreadyScannedBarcode; }
            set { SetValue(ref _titleAlreadyScannedBarcode, value); }

        }


        public string MessageScanValidBarcode
        {
            get { return _messageScanValidBarcode; }
            set { SetValue(ref _messageScanValidBarcode, value); }
        }

        public string LblFile
        {
            get { return _lblFile; }
            set { SetValue(ref _lblFile, value); }
        }
        public string LblClient
        {
            get { return _lblClient; }
            set { SetValue(ref _lblClient, value); }
        }
        public string LblSize
        {
            get { return _lblSize; }
            set { SetValue(ref _lblSize, value); }
        }
        public string LblEnd
        {
            get { return _lblEnd; }
            set { SetValue(ref _lblEnd, value); }
        }
        public string LblNumItem
        {
            get { return _lblNumItem; }
            set { SetValue(ref _lblNumItem, value); }
        }
        public string LblControl
        {
            get { return _lblControl; }
            set { SetValue(ref _lblControl, value); }
        }
        public string LblFileNumber
        {
            get { return _lblFileNumber; }
            set { SetValue(ref _lblFileNumber, value); }
        }

        public string LblClientNumber
        {
            get { return _lblClientNumber; }
            set { SetValue(ref _lblClientNumber, value); }
        }

        public string Volume
        {
            get { return _volume; }
            set { SetValue(ref _volume, value); }
        }

        public string LoadingCountry
        {
            get { return _loadingCountry; }
            set { SetValue(ref _loadingCountry, value); }
        }
        public string DestinationCountry
        {
            get { return _destinationCountry; }
            set { SetValue(ref _destinationCountry, value); }
        }
        public string Barcode
        {
            get { return _barcode; }
            set { SetValue(ref _barcode, value); }
        }

        public int NumOfItem
        {
            get { return _numOfItem; }
            set { SetValue(ref _numOfItem, value); }
        }

        public ObservableCollection<ListViewItemModel> Items
        {
            get { return _items; }
            set { SetValue(ref _items, value); }
        }

        public string MyImagePath
        {
            get { return _myImagePath; }
            set { SetValue(ref _myImagePath, value); }

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DelControlViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="fileInfo">The file information.</param>
        public DelControlViewModel(IPageService pageService, FileInfoDeliveryModel fileInfo)
        {
            _databaseAccess = new DatabaseAccess();
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            LanguageSelected = _appViewModel.DefaultedCultureInfo;
            PageService = pageService;
            SetValueByBinding(fileInfo);
            _fileInfo = fileInfo;
            ScanBtn = new Command(ScanBarcode);
            GetDeliveryItem(_fileInfo.File_number);
            GetCountOfRubric(_fileInfo.File_number);
            GoToSynchronation = new Command(Synchronation);
            Back = new Command(GoBack);
            BarCodeScannerViewModel = new BarCodeScannerViewModel();

        }

        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblControl = Localize.GetString("LblControl", cultureInfo);
            LblNumItem = Localize.GetString("LblNumItem", cultureInfo);
            LblFile = Localize.GetString("LblFile", cultureInfo);
            LblClient = Localize.GetString("LblClient", cultureInfo);
            LblSize = Localize.GetString("LblSize", cultureInfo);
            LblEnd = Localize.GetString("LblEnd", cultureInfo);
            TitleWrongBarcode = Localize.GetString("TitleWrongBarcode", cultureInfo);
            MessageScanValidBarcode = Localize.GetString("MessageScanValidBarcode", cultureInfo);
            TitleAlreadyScannedBarcode= Localize.GetString("TitleAlreadyScannedBarcode", cultureInfo);
        }


            /// <summary>
            /// Go to Synchronation Page.
            /// </summary>
            public async void Synchronation()
        {
            await PageService.PushAsync(new Synchro(_fileInfo));

        }


        /// <summary>
        /// SetValueByBinding field UI with value from paramater of constructor .
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        private void SetValueByBinding(FileInfoDeliveryModel fileInfo)
        {
  
            LblFileNumber = fileInfo.File_number;
            LblClientNumber = fileInfo.Client_number;
            Volume = fileInfo.Volume;
            LoadingCountry = fileInfo.Country_loading;
            DestinationCountry = fileInfo.Country_delivery;
            SetTransportImage(fileInfo);

        }

        /// <summary>
        /// Gets the delivery item.
        /// display data in listview
        /// </summary>
        private void GetDeliveryItem(string filenumber)
        {
            Items = new ObservableCollection<ListViewItemModel>();
            _listRubricDetails = new List<ListViewRubricModel>();
            _listRubricDetails = _databaseAccess.GetAllRubricDeliveryDetails(filenumber);

            if (_listRubricDetails.Count>0)
            {
                for(int i=0;i< _listRubricDetails.Count;i++)
                {
                    var itemName = (LanguageSelected.Equals("fr-FR") ? _listRubricDetails[i].Name_fr : _listRubricDetails[i].Name_en);
                    ListViewItemModel temp = new ListViewItemModel()
                    {
                        
                        SvgPath = _listRubricDetails[i].ImageSVGPath,
                        ItemName = itemName,
                        Quantity = _listRubricDetails[i].CountRubric,
                        AlreadyScan= _listRubricDetails[i].CountAlreadyScannedRubric

                    };
                    Items.Add(temp);
                }

            }
            else
            {
                Items = new ObservableCollection<ListViewItemModel>();
            }

        }

        /// <summary>
        /// Gets the count of rubric.
        /// </summary>
        private void GetCountOfRubric(string filenumber)
        {
            NumOfItem = _databaseAccess.GetCountAllRubricDeliveryDetails(filenumber);
            
        }

        /// <summary>
        /// Goes the back.
        /// </summary>
        public async void GoBack()
        {
            await PageService.PushAsync(new View.PackingCygest());
        }

        /// <summary>
        /// Scans the barcode.
        /// it launches barcode scanner and scan barcode of items
        /// take details of barcode from database and pass it via 
        /// parameter to next page
        /// </summary>
        public async void ScanBarcode()
        {
            var scannedText = await BarCodeScannerViewModel.BarcodePage(new PageService());

            if (scannedText != null)
            {
                _item = _databaseAccess.GetItemDetails(scannedText);

                if (_item.Count > 0 && _item[0].AlreadyScanned.Equals("False"))
                {
                    await PageService.PushAsync(new DelItem(_fileInfo, _item[0]));
                }
                else if(_item.Count > 0 && _item[0].AlreadyScanned.Equals("True"))
                {
                    await PageService.DisplayAlert(TitleAlreadyScannedBarcode,MessageScanValidBarcode, "OK");
                }
                else
                {
                    await PageService.DisplayAlert(TitleWrongBarcode, MessageScanValidBarcode, "OK");
                }

            }
        }

        /// <summary>
        /// Sets the transport image.
        /// </summary>
        /// <param name="fileInfoModel">The file information model.</param>
        private void SetTransportImage(FileInfoDeliveryModel fileInfoModel)
        {
            if (fileInfoModel.Transportation == Constants.Air)
            {
                MyImagePath = "resource://PackingCygest.Image.plane.svg";
            }
            else if (fileInfoModel.Transportation == Constants.Road)
            {
                MyImagePath = "resource://PackingCygest.Image.truck.svg";
            }
            else
            {
                MyImagePath = "resource://PackingCygest.Image.boat.svg";
            }
        }
    }
}
