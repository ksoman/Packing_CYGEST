using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.View;
using System.Windows.Input;
using Xamarin.Forms;
using PackingCygest.Data;
using System.Collections.ObjectModel;
using System.Reflection;
using PackingCygest.Service;
using System.Threading.Tasks;
using System.Collections.Generic;
using Plugin.Connectivity;
using System;
using PackingCygest.Utils;
using System.Linq;
using Xamarin.Essentials;
using PackingCygest.Shared;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class PackingCygestViewModel:BaseViewModel
    {
        #region declaration
        /// <summary>
        /// Gets or sets the scan bar code.
        /// </summary>
        /// <value>
        /// The scan bar code.
        /// </value>
        public ICommand ScanBarCode { get; set; }

        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public ICommand GoToNewItem { get; set; }
        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public ICommand Back { get; set; }
        /// <summary>
        /// Gets or sets the page service.
        /// </summary>
        /// <value>
        /// The page service.
        /// </value>
        public IPageService PageService { get; set; }
        /// <summary>
        /// Gets or sets my property.
        /// </summary>
        /// <value>
        /// My property.
        /// </value>
        public int MyProperty { get; set; }

        /// <summary>
        /// Gets or sets the file information model.
        /// </summary>
        /// <value>
        /// The file information model.
        /// </value>
        public FileInfoModel FileInfoModel { get; set; }
        /// <summary>
        /// Assembly
        /// </summary>
        /// 
        private FileInfoDeliveryModel _fileInfoDel;

        public string Loading { get; private set; }

        public string AlertInvalidBarcodeTitle{ get; set;}

        public string AlertScanBarcodeTitle { get; set; }

        public string AlertInvalidBarcodeMsg { get; set; }

        public string AlertScanBarcodeMsg { get; set; }

        public string Delivery { get; private set; }

        private string _loadingData;
        public string LoadingData
        {
            get { return _loadingData; }
            set { SetValue(ref _loadingData, value); }
        }

        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;

        public string NewLoadingImage => "PackingCygest.Image.house.svg";

        public string NewDeliveryImage => "PackingCygest.Image.truck.svg";

        public string ConfigurationImage => "PackingCygest.Image.Configuration.svg";

        private string _lblNoNetwork;

        private string _lblPleaseEnableYourNetwork;

        private string _branch;

        private string _phoneNumber;

        public string LblNoNetwork
        {
            get { return _lblNoNetwork; }
            set { SetValue(ref _lblNoNetwork, value); }
        }

        public string LblPleaseEnableYourNetwork
        {
            get { return _lblPleaseEnableYourNetwork; }
            set { SetValue(ref _lblPleaseEnableYourNetwork, value); }
        }

        /// <summary>
        /// Gets or sets the newloading.
        /// </summary>
        /// <value>
        /// The newloading.
        /// </value>
        public NewLoadingModel Newloading
        {            
            get { return _newloading;    }
            set { SetValue(ref _newloading, value);}
        }

        /// <summary>
        /// Gets or sets a value indicating whether [activity indicator].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [activity indicator]; otherwise, <c>false</c>.
        /// </value>
        public bool ActivityIndicatorVisibility
        {
            get { return _activityIndicatorVisibility; }
            set { SetValue(ref _activityIndicatorVisibility, value); }          
        }

        /// <summary>
        /// Gets or sets a value indicating whether [stack layout].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [stack layout]; otherwise, <c>false</c>.
        /// </value>
        public bool StackLayoutVisibility
        {
            get { return _stackLayoutVisibility; }
            set { SetValue(ref _stackLayoutVisibility, value); }          
        }

        /// <summary>
        /// Gets or sets a value indicating whether [PackingCygest page layout].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [PackingCygest page layout]; otherwise, <c>false</c>.
        /// </value>
        public bool PackingCygestPageLayoutVisibility
        {
            get { return _PackingCygestPageLayoutVisibility; }
            set { SetValue(ref _PackingCygestPageLayoutVisibility, value); }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        public double PackingCygestPageLayoutOpacity
        {         
            get { return _PackingCygestPageLayoutOpacity; }
            set { SetValue(ref _PackingCygestPageLayoutOpacity, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [label loading text visibility].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [label loading text visibility]; otherwise, <c>false</c>.
        /// </value>
        public bool LblLoadingTextVisibility
        {
            get { return _lblLoadingTextVisibility; }
            set { SetValue(ref _lblLoadingTextVisibility, value); }
       }

        private readonly DatabaseAccessAsync _dataAccessAsync;

        private readonly DatabaseAccess _dataAccess;

        private readonly FileService _fileInfo;    

        private NewLoadingModel _newloading = new NewLoadingModel();
      
        private DeliveryService _delService = new DeliveryService();


        private LoadingDetails _fileSepecific;

        private bool _stackLayoutVisibility;

        private bool _activityIndicatorVisibility;

        private bool _PackingCygestPageLayoutVisibility;

        private double _PackingCygestPageLayoutOpacity;

        private bool _mainStackLayout;

        private bool _lblLoadingTextVisibility;

        private ObservableCollection<ItemDeliveredModel> _itemDelivered;
        public ObservableCollection<ItemDeliveredModel> ItemDelivered { get => _itemDelivered; set => _itemDelivered = value; }
        private readonly DeliveryService _deliveryService = new DeliveryService();

        public bool MainStackLayout
        {
            get { return _mainStackLayout; }
            set { SetValue(ref _mainStackLayout, value); }
        }


        #endregion


        private BarCodeScannerViewModel _barCodeScannerViewModel;

        public BarCodeScannerViewModel BarCodeScannerViewModel
        {
            get { return _barCodeScannerViewModel; }
            set { SetValue(ref _barCodeScannerViewModel, value); }
        }


        private string _pressButton;

        public string PressButton
        {
            get { return _pressButton; }
            set { SetValue(ref _pressButton, value); }

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PackingCygestViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="fileInfo"></param>
        public PackingCygestViewModel(IPageService pageService, FileInfoModel fileInfo)
        {

            BarCodeScannerViewModel = new BarCodeScannerViewModel();
            _dataAccessAsync = new DatabaseAccessAsync();
            _dataAccess = new DatabaseAccess();
            _fileInfo = new FileService();
            PackingCygestPageLayoutOpacity = 1;
            PackingCygestPageLayoutVisibility = true;
            PageService = pageService;
            FileInfoModel = fileInfo;
            GetRoomDetails();
            GetRubricsDetails();
            GetItemDetails();
            ScanBarCode = new Command(BarcodePage);
            GoToNewItem = new Command(BarcodeDeliveryPage);
            Back = new Command(GoBack);
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            GetConfigurationDetails();
        }


        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            Loading = Localize.GetString("Loading", cultureInfo);
            Delivery = Localize.GetString("Delivery", cultureInfo);
            LoadingData = Localize.GetString("LoadingData", cultureInfo);
            AlertScanBarcodeTitle = Localize.GetString("LblScanBarcode", cultureInfo);
            AlertScanBarcodeMsg = Localize.GetString("MsgScanBarcode", cultureInfo);
            AlertInvalidBarcodeTitle = Localize.GetString("LblInvalidBarcode",cultureInfo);
            AlertInvalidBarcodeMsg = Localize.GetString("MsgInvalidBarcode", cultureInfo);
            PressButton = Localize.GetString("lblPressButton", cultureInfo);
            LblNoNetwork = Localize.GetString("LblNoNetwork", cultureInfo);
            LblPleaseEnableYourNetwork = Localize.GetString("LblPleaseEnableYourNetwork", cultureInfo);

        }


        /// <summary>
        /// Gets the file information asynchronous.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <param name="isLoading">if set to <c>true</c> [is loading].</param>
        /// <returns></returns>
        private async Task GetFileInformationAsync(string barcode, bool isLoading)
        {
           
            var connected = CrossConnectivity.Current.IsConnected;
            string phone = "";
            if (connected)
            {
                phone = _dataAccess.GetPhoneNumber();
                if (!string.IsNullOrEmpty(barcode))
                {
                    FileInfoModel = await _fileInfo.GetFileInfoAsync(barcode, phone);

                    if (FileInfoModel.File_number != null)
                    {
                        _dataAccess.InsertFileInfo(FileInfoModel);

                        if (isLoading)
                        {
                            await CheckCubageInfoAsync(FileInfoModel);
                        }
                    }
                }
            }

            SetActivityIndicator(false);
        }

        /// <summary>
        /// Gets the file information delivery asynchronous.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        private async Task GetFileInformationDeliveryAsync(string barcode)
        {

            var connected = CrossConnectivity.Current.IsConnected;
        
            if (connected)
            {
                _phoneNumber = _dataAccess.GetPhoneNumber();
                if (!string.IsNullOrEmpty(barcode))
                {
                    _fileInfoDel = await _delService.GetFileInfosDelivery(barcode, _phoneNumber);

                    if (_fileInfoDel.File_number != null)
                    {
                         _dataAccess.InsertFileInfoDel(_fileInfoDel);
                    }

                }
            }

        }
        /// <summary>
        /// Gets the configuration details.
        /// </summary>
        private void GetConfigurationDetails()
        {
            _branch= _dataAccess.GetBranchCode();         
        }
        /// <summary>
        /// Gets the file items delivery.
        /// </summary>
        /// <param name="fileNumber">The file number.</param>
        /// <returns></returns>
        private async Task GetFileItemsDelivery(string fileNumber)
        {
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    ItemDelivered = await _deliveryService.GetFileItemsDelivery(fileNumber, _appViewModel.DefaultedCultureInfo);
                    List<ItemDeliveredModel> _listItemDelivery = new List<ItemDeliveredModel>();

                    if (ItemDelivered != null)
                    {
                        foreach (var itemDel in ItemDelivered)
                        {
                            if(!string.IsNullOrEmpty(itemDel.Barecode))
                            {
                                var svgpath = DependencyService.Get<ISavePhoto>().SavetoSd(itemDel.ImageSVG, itemDel.Name, "DeliverySVG");
                                var ItemDeliveryTemp = new ItemDeliveredModel
                                {

                                    IdFileItem = itemDel.IdFileItem,
                                    IdFileRoom = itemDel.IdFileRoom,
                                    IdItem = itemDel.IdItem,
                                    Idrubric = itemDel.Idrubric,
                                    IdRoom = itemDel.IdRoom,
                                    File_Number = itemDel.File_Number,
                                    Client_Number = itemDel.Client_Number,
                                    Barecode = itemDel.Barecode,
                                    Name = itemDel.Name,
                                    cub_Valued = itemDel.cub_Valued,
                                    cub_Dismount = itemDel.cub_Dismount,
                                    cub_Reassembly = itemDel.cub_Reassembly,
                                    cub_Crate = itemDel.cub_Crate,
                                    comments_Loading = itemDel.comments_Loading,
                                    comments_Delivery = itemDel.comments_Delivery,
                                    sent_loading = itemDel.sent_loading,
                                    packed_by = itemDel.packed_by,
                                    Reassembling_type = itemDel.Reassembling_type,
                                    Dismounting_type = itemDel.Dismounting_type,
                                    mechanical_statement = itemDel.mechanical_statement,
                                    ImageSVG = itemDel.ImageSVG,
                                    Telephone_loading = itemDel.Telephone_loading,
                                    Branche_loading = itemDel.Branche_loading,
                                    Telephone_delivery = _phoneNumber,
                                    Branche_delivery = _branch,
                                    sent_delivery = itemDel.sent_delivery,
                                    ImageSVGPath = svgpath,
                                    AlreadyScanned="False"
                                };
                                _listItemDelivery.Add(ItemDeliveryTemp);
                                
                            }
                        }
                        // remove duplicate barcode by taking latest form API &insert list in db
                        var result = _listItemDelivery.GroupBy(test => test.Barecode).Select(grp => grp.Last()).ToList();
                        InsertDeliveryDataFunc(result);

                    }
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });

            }

        }


        /// <summary>
        /// Gets the item photo details by file number.
        /// </summary>
        /// <param name="fileNum">The file number.</param>
        /// <returns></returns>
        private async Task GetItemPhotoDetailsByFileNumber(string fileNum)
        {
            try
            {
                SetActivityIndicator(true);
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    List<DeliveryPhotoModel> listDelPhoto = new List<DeliveryPhotoModel>();
                    ObservableCollection<DownloadedPhotoModel> photoDetails;
                    photoDetails = await _deliveryService.GetItemsPhotos(fileNum);

                    if (photoDetails.Count > 0)
                    {
                        for (int i = 0; i < photoDetails.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(photoDetails[i].Photo) && !string.IsNullOrEmpty(photoDetails[i].Barecode)) 
                            {
                                var timeStamp= DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm:ss.fffff tt");
                                string tempName = "Dwl" + fileNum + photoDetails[i].Barecode + timeStamp.Replace(" ", "_");
                                if (tempName.Contains("/"))
                                {
                                    tempName = tempName.Replace("/", "_");
                                }
                                var path = DependencyService.Get<ISaveItemPhoto>().Base64ToByte(photoDetails[i].Photo, tempName, "DeliveryPNG");
                                var info = new DeliveryPhotoModel
                                {
                                    Barecode = photoDetails[i].Barecode,
                                    IsSent = true,
                                    PhotoPath = path

                                };
                                listDelPhoto.Add(info);
                            }
                        }
                        InsertPhotoDetailFunction(listDelPhoto);
                        await Task.Delay(TimeSpan.FromSeconds(1));
                    }
                    else
                    {
                        // GetPhotoPathFromDb(_delivery.Barecode);

                    }
                }
                SetActivityIndicator(false);
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });

            }

        }

        /// <summary>
        /// Inserts the delivery data function.
        /// </summary>
        /// <param name="itemDeliveredModel">The item delivered model.</param>
        public void InsertDeliveryDataFunc(List<ItemDeliveredModel> itemDeliveredModel)
        {
            _dataAccess.InsertDeliveryListData(itemDeliveredModel);
        }

        /// <summary>
        /// Inserts the photo detail function.
        /// </summary>
        /// <param name="delphoto">The delphoto.</param>
        private void InsertPhotoDetailFunction(List<DeliveryPhotoModel> delphoto)
        {
            _dataAccess.InsertPhotoDetails(delphoto);
        }

        /// <summary>
        /// Sets the activity indicator.
        /// </summary>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        private void SetActivityIndicator(bool visibility)
        {
            ActivityIndicatorVisibility = visibility;
            StackLayoutVisibility = visibility;      
            LblLoadingTextVisibility = true;

        }

        /// <summary>
        /// Checks the cubage information.
        /// </summary>
        /// <param name="fileInfoModel">The file information model.</param>
        private async Task CheckCubageInfoAsync(FileInfoModel fileInfoModel)
        {
            var connected = CrossConnectivity.Current.IsConnected;

            if (connected && fileInfoModel.Info_Cubage)
            {
                _fileSepecific = new LoadingDetails();
                Newloading.ItemsFiles = await _fileSepecific.GetItemDetailsFile(fileInfoModel.File_number,fileInfoModel.Info_Cubage);
                Newloading.RoomsFiles = await _fileSepecific.GetRoomsFile(fileInfoModel.File_number);        
           
            }
        }
        /// <summary>
        /// Gets the room details.
        /// </summary>
        private void GetRoomDetails()
        {
            Newloading.Rooms = _dataAccess.GetAllRooms();
        }
        /// <summary>
        /// Gets the rubrics details.
        /// </summary>
        private void GetRubricsDetails()
        {
            Newloading.Rubrics = _dataAccess.GetAllRubrics();
        }

        private void GetItemDetails()
        {
            Newloading.Items = _dataAccess.GetItemsDetails();

        }
        /// <summary>
        /// Barcodes the page.
        /// </summary>
        public async void BarcodePage()
        {
            string scannedBarcode = "";
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected)
            {
                SetActivityIndicator(true);
                MainStackLayout = false;
                PackingCygestPageLayoutOpacity = 0.4;

                LazerBarCode();

            }
            else
            {
                await PageService.DisplayAlert(LblNoNetwork, LblPleaseEnableYourNetwork, "ok");

            }




        }   
          private async void LazerBarCode()
        {
            if (DeviceInfo.Model.Equals("PX320"))
            {
                LoadingData = PressButton;
                MessagingCenter.Subscribe<BarCodeReader, string>(this, "barcode", async (obj, code) =>
                {
                    MessagingCenter.Unsubscribe<BarCodeReader>(this, "barcode");
                    LoadingData = $"Processing {code}";

                    System.Diagnostics.Debug.WriteLine($"Bar code is: {code} ");
              

                   var  result = code.Substring(0, code.Length - 3);
                    Finalize(result);
                });


            }
            else
            {

                var scannedBarcode = await BarCodeScannerViewModel.BarcodePage(new PageService());
                var result = scannedBarcode.Substring(0, scannedBarcode.Length - 3);
                Finalize(result);
            }
        }
        public async void Finalize( string scannedBarcode)
        {

            if (string.IsNullOrEmpty(Localize.BaseCode))
            {
                Localize.BaseCode = scannedBarcode;
            }
            MessagingCenter.Unsubscribe<BarCodeReader,string>(this, "barcode");
            if (!string.IsNullOrEmpty(scannedBarcode))
            {
                await GetFileInformationAsync(scannedBarcode, true);

                Newloading.FileItemModel = new FileItemModel();
                if (!string.IsNullOrEmpty(FileInfoModel.File_number))
                {
                    await PageService.PushAsync(new LoadMenu(Newloading, FileInfoModel));
                }
                else
                {
                    await PageService.DisplayAlert(AlertInvalidBarcodeTitle, AlertInvalidBarcodeMsg, "OK");
                    await PageService.PushAsync(new View.PackingCygest());
                }
            }
            else
            {
                await PageService.DisplayAlert(AlertScanBarcodeTitle, AlertScanBarcodeMsg, "OK");
                await PageService.PushAsync(new View.PackingCygest());
            }
        }
        
        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {
            PageService.PushAsync(new ConfigurationPage());
        }

        /// <summary>
        /// Barcodes the delivery page. //USE PX320 PHONE SETTING THE MESSAGES
        /// if file number exist then download del item info,photo and insert in db
        /// </summary>
        public async void BarcodeDeliveryPage()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if (connected)
            {
                SetActivityIndicator(true);
                MainStackLayout = false;
                PackingCygestPageLayoutOpacity = 0.4;
                LazerBarCode();


            }
            else
            {
                await PageService.DisplayAlert(LblNoNetwork, LblPleaseEnableYourNetwork, "ok");

            }
        }
        private async void FinalizePackingCygest(string scannedBarcode)
        {
            if (!string.IsNullOrEmpty(scannedBarcode))
            {
                await GetFileInformationDeliveryAsync(scannedBarcode);
                if (!string.IsNullOrEmpty(_fileInfoDel.File_number))
                {
                    await GetFileItemsDelivery(scannedBarcode);
                    await GetItemPhotoDetailsByFileNumber(_fileInfoDel.File_number);

                    List<RoomFileModel> tmpRoomFile = new List<RoomFileModel>();
                    LoadingDetails roomfile = new LoadingDetails();
                    tmpRoomFile = await roomfile.GetRoomsFile(_fileInfoDel.File_number);
                    await PageService.PushAsync(new DelControls(_fileInfoDel));

                }
                else
                {
                    await PageService.DisplayAlert(AlertInvalidBarcodeTitle, AlertInvalidBarcodeMsg, "OK");
                    await PageService.PushAsync(new View.PackingCygest());
                }
            }
            else
            {
                await PageService.DisplayAlert(AlertScanBarcodeTitle, AlertScanBarcodeMsg, "OK");
                await PageService.PushAsync(new View.PackingCygest());
            }
        }
     }
}
