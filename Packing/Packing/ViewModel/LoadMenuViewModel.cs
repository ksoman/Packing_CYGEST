using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.Utils;
using PackingCygest.View;
using Plugin.Connectivity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PackingCygest.ViewModel
{
    /// <summary>
    /// LoadMenuViewModel
    /// </summary>
    public class LoadMenuViewModel : BaseViewModel
    {
        private readonly DatabaseAccess _db;
        private readonly FileInfoModel _fileInfo;
        private string _lblFileNumber;
        private string _lblClientNumber;
        private string _volume;
        private string _loadingCountry;
        private string _destinationCountry;
        private string _lblFile;
        private string _lblClient;
        private string _lblSize;

        private readonly NewLoadingModel _newLoading;
        private IPageService _pageSercice;
        private string _myImagePath;
        private string _newItemButtonColour;
        private string _endPackingCygestButtonColour;
        private string _newItemPath;
        private string _endPackingCygestPath;

        private string _lblNewItem;
        private string _lblEndPackingCygest;
        private string _lblSync;
        private bool _enableSyncButton;

        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string Bedroom => "PackingCygests.Image.addNew.svg";
        public string BoxSvg => "PackingCygests.Image.box.svg";
        public string FillSvg => "PackingCygests.Image.fill.svg";
        public static bool NewItemSelected;
        public static bool EndItemSelected;
        public List<ItemsSearchModel> NewListItems;

        public string LblNewItem
        {
            get { return _lblNewItem; }
            set { SetValue(ref _lblNewItem, value); }
        }

        public string LblEndPackingCygest
        {
            get { return _lblEndPackingCygest; }
            set { SetValue(ref _lblEndPackingCygest, value); }
        }

        public string MyImagePath
        {
            get { return _myImagePath; }
            set { SetValue(ref _myImagePath, value); }

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
        public string LblSync
        {
            get { return _lblSync; }
            set { SetValue(ref _lblSync, value); }

        }

        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        /// <value>
        /// The list items.
        /// </value>
        private ObservableCollection<PackItemModel> _listItem;
        public ObservableCollection<PackItemModel> ListItems
        {
                get { return _listItem; }
                set { SetValue(ref _listItem, value); }
        }
      
        /// <summary>
        /// Gets or sets the back to PackingCygest.
        /// </summary>
        /// <value>
        /// The back to PackingCygest.
        /// </value>
        public ICommand BackToPackingCygest { get; set; }
        /// <summary>
        /// Gets or sets to rooms.
        /// </summary>
        /// <value>
        /// To rooms.
        /// </value>
        public ICommand AddItems { get; set; }

        /// <summary>
        /// Gets or sets the end PackingCygest.
        /// </summary>
        /// <value>
        /// The end PackingCygest.
        /// </value>
        public ICommand EndPackingCygest { get; set; }

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
        /// Gets or sets the label file number.
        /// </summary>
        /// <value>
        /// The label file number.
        /// </value>
        public string LblFileNumber
        {
            get { return _lblFileNumber; }
            set { SetValue(ref _lblFileNumber, value); }
        }

        /// <summary>
        /// Gets or sets the label client number.
        /// </summary>
        /// <value>
        /// The label client number.
        /// </value>
        public string LblClientNumber
        {
            get { return _lblClientNumber; }
            set { SetValue(ref _lblClientNumber, value); }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public string Volume
        {
            get { return _volume; }
            set { SetValue(ref _volume, value); }
        }

        /// <summary>
        /// Gets or sets the loading country.
        /// </summary>
        /// <value>
        /// The loading country.
        /// </value>
        public string LoadingCountry
        {
            get { return _loadingCountry; }
            set { SetValue(ref _loadingCountry, value); }
        }
        /// <summary>
        /// Gets or sets the destination country.
        /// </summary>
        /// <value>
        /// The destination country.
        /// </value>
        public string DestinationCountry
        {
            get { return _destinationCountry; }
            set { SetValue(ref _destinationCountry, value); }
        }

        /// <summary>
        /// Gets or sets the back button colour.
        /// </summary>
        /// <value>
        /// The back button colour.
        /// </value>
        public string NewItemButtonColour
        {
            get { return _newItemButtonColour; }
            set { SetValue(ref _newItemButtonColour, value); }
        }

        /// <summary>
        /// Gets or sets the image path.
        /// </summary>
        /// <value>
        /// The image path.
        /// </value>
        public string NewItemPath
        {
            get { return _newItemPath; }
            set { SetValue(ref _newItemPath, value); }
        }

        /// <summary>
        /// Gets or sets the end PackingCygest path.
        /// </summary>
        /// <value>
        /// The end PackingCygest path.
        /// </value>
        public string EndPackingCygestPath
        {
            get { return _endPackingCygestPath; }
            set { SetValue(ref _endPackingCygestPath, value); }
        }

        /// <summary>
        /// Gets or sets the end PackingCygest button colour.
        /// </summary>
        /// <value>
        /// The end PackingCygest button colour.
        /// </value>
        public string EndPackingCygestButtonColour
        {
            get { return _endPackingCygestButtonColour; }
            set { SetValue(ref _endPackingCygestButtonColour, value); }

        }

        private readonly NewLoadingService _newLoadingService = new NewLoadingService();
        private bool _sentItem=false;
        public ICommand SyncLoadingDetails { get; set; }
        private bool _mainStackLayout;
        private double _PackingCygestPageLayoutOpacity;
        private bool _stackLayoutVisibility;
        private bool _activityIndicatorVisibility;
        private bool _lblLoadingTextVisibility;
        private double _actOpacity;

        public double PackingCygestPageLayoutOpacity
        {
            get { return _PackingCygestPageLayoutOpacity; }
            set { SetValue(ref _PackingCygestPageLayoutOpacity, value); }
        }

        public bool MainStackLayout
        {
            get { return _mainStackLayout; }
            set { SetValue(ref _mainStackLayout, value); }
        }
        public bool StackLayoutVisibility
        {
            get { return _stackLayoutVisibility; }
            set { SetValue(ref _stackLayoutVisibility, value); }
        }
        public bool ActivityIndicatorVisibility
        {
            get { return _activityIndicatorVisibility; }
            set { SetValue(ref _activityIndicatorVisibility, value); }
        }

        public bool LblLoadingTextVisibility
        {
            get { return _lblLoadingTextVisibility; }
            set { SetValue(ref _lblLoadingTextVisibility, value); }
        }

        public double ActOpacity
        {
            get { return _actOpacity; }
            set { SetValue(ref _actOpacity, value); }
        }

        private bool _hasActivtyLoad;

        public bool HasActivtyLoad
        {
            get { return _hasActivtyLoad; }
            set { SetValue(ref _hasActivtyLoad, value); }
        }

        public bool EnableSyncButton
        {
            get { return _enableSyncButton; }
            set { SetValue(ref _enableSyncButton, value); }
        }

        public LoadMenuViewModel()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadMenuViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="newLoading">The room loading.</param>
        /// <param name="fileInfoModel"></param>
        public LoadMenuViewModel(IPageService pageService, NewLoadingModel newLoading, FileInfoModel fileInfoModel)
        {
            SetActivityIndicator(false);
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _db = new DatabaseAccess();
            PageSercice = pageService;
            _newLoading = newLoading;
            _fileInfo = fileInfoModel;
            SyncLoadingDetails = new Command(PerfomCall);

            MappingValue(_newLoading, _fileInfo);
            PopulateView();
            BackToPackingCygest = new Command(ToPackingCygest);
            AddItems = new Command(GoToRooms);
            if(EndItemSelected && NewItemSelected == false)
            {
                SetEndItemButtonRed();
            }
            else if (NewItemSelected && EndItemSelected == false)
            {
                SetNewItemButtonRed();
            }
            else
            {
                SetButtonColour();
                SetImageIcons();
            }
            EndPackingCygest = new Command(Synchonisation);
              
            SetValueByBinding(fileInfoModel);
            SetTransportImage(fileInfoModel);
            
           
        }


        /// <summary>
        /// Sets the image icons.
        /// </summary>
        private void SetImageIcons()
        {
            NewItemPath = "resource://PackingCygest.Image.AddUnselected.svg";
            EndPackingCygestPath = "resource://PackingCygest.Image.end.svg";
        }
        /// <summary>
        /// Sets the button colour.
        /// </summary>
        private void SetButtonColour()
        {
            NewItemButtonColour = Constants.UnSelectedButtonColour;
            EndPackingCygestButtonColour = Constants.UnSelectedButtonColour;
        }
        private void SetNewItemButtonRed()
        {
            NewItemButtonColour = Constants.SelectedButtonColour;
            EndPackingCygestButtonColour = Constants.UnSelectedButtonColour;
            NewItemPath = "resource://PackingCygest.Image.Add.svg";
            EndPackingCygestPath = "resource://PackingCygest.Image.end.svg";
        }
        private void SetEndItemButtonRed()
        {
            EndPackingCygestButtonColour = Constants.SelectedButtonColour;
            NewItemButtonColour = Constants.UnSelectedButtonColour;
            NewItemPath = "resource://PackingCygest.Image.AddUnselected.svg";
            EndPackingCygestPath = "resource://PackingCygest.Image.end_Selected.svg";
        }

        /// <summary>
        /// Sets the transport image.
        /// </summary>
        /// <param name="fileInfoModel">The file information model.</param>
        private void SetTransportImage(FileInfoModel fileInfoModel)
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
        /// <summary>
        /// Sets the value by binding.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        private void SetValueByBinding(FileInfoModel fileInfo)
        {
            LblFileNumber = fileInfo.File_number;
            LblClientNumber = fileInfo.Client_number;
            Volume = fileInfo.Volume;
            LoadingCountry = fileInfo.Country_loading;
            DestinationCountry = fileInfo.Country_delivery;

        }
        private void MappingValue(NewLoadingModel newLoading, FileInfoModel fileInfo)
        {
            newLoading.FileItemModel = new FileItemModel();
            newLoading.FileItemModel.File_Number = fileInfo.File_number;
            newLoading.FileItemModel.Client_Number = fileInfo.Client_number;
            newLoading.FileItemModel.Telephone_loading = _db.GetPhoneNumber();
            newLoading.FileItemModel.Branche_loading = _db.GetBranchCode();


        }
        /// <summary>
        /// To the PackingCygest.
        /// </summary>
        public async void ToPackingCygest()
        {
            await PageSercice.PushAsync(new View.PackingCygest());
        }
        /// <summary>
        /// Goes to rooms.
        /// </summary>
        public async void GoToRooms()
        {
            ItemSelectionViewModel.ClickedAdd = false;
            _newLoading.Search = false;
            NewItemSelected = true;
            EndItemSelected = false;
            SetNewItemButtonRed();
            _newLoading.SelectedBarcode = null;
            await PageSercice.PushAsync(new RoomSelection(_newLoading, _fileInfo));

        }

        /// <summary>
        /// Synchonisations this instance.
        /// </summary>
        private async void Synchonisation()
        {
            NewItemSelected = true;
            EndItemSelected = false;
            SetEndItemButtonRed();
            await PageSercice.PushAsync(new Synchro(_newLoading, _fileInfo));

        }
        /// <summary>
        /// Populates the view.
        /// </summary>
        public void PopulateView()
        {
            var items = PopulateItems();
            ListItems = new ObservableCollection<PackItemModel>(items);

        }

        /// <summary>
        /// Populates the items.
        /// </summary>
        /// <returns></returns>
        public List<PackItemModel> PopulateItems()
        {
            List<PackItemModel> allPack = new List<PackItemModel>();
            NewListItems = new List<ItemsSearchModel>();
            bool cubage;
            List<FileItemModel> allIdItem = _db.GetFileDetails(_newLoading.FileItemModel.File_Number);
            if (allIdItem.Count != 0)
            {
                if (_fileInfo.Info_Cubage)
                {
                    cubage = true;
                    NewListItems = _db.GetFileDetailsList(cubage, _newLoading.FileItemModel.File_Number, allIdItem);
                }
                else
                {
                    cubage = false;
                    NewListItems = _db.GetFileDetailsList(cubage, _newLoading.FileItemModel.File_Number, allIdItem);
                }

            }
            if (NewListItems.Count != 0)
            {
                for (int i = 0; i < NewListItems.Count; i++)
                {
                    PackItemModel pc = new PackItemModel
                    {
                        IdRoom = NewListItems[i].IdRoom,
                        IdRoomFile = NewListItems[i].IdFileRoom,
                        IdFileItem =NewListItems[i].IdFileItem,
                        IdItem = NewListItems[i].IdItem,
                        RoomName = NewListItems[i].RoomName,
                        Type = NewListItems[i].SvgImagePath,
                        BarCode = NewListItems[i].Barecode,
                        RubricsName = NewListItems[i].RubricsName,
                        IdRubrics = NewListItems[i].Idrubric,
                        Comments_loading = NewListItems[i].Comments_loading
                        
                    };
                    allPack.Add(pc);
                }
            }
            //enable disable sync button on list.count
            if (allPack.Count > 0)
            {
                EnableSyncButton = true;
            }
            else
            {
                EnableSyncButton = false;
            }

            return allPack;
        }

        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
           LblNewItem  = Localize.GetString("LblNewItem", cultureInfo);
           LblEndPackingCygest = Localize.GetString("LblEndPackingCygest", cultureInfo);
           LblFile = Localize.GetString("LblFile", cultureInfo);
           LblClient = Localize.GetString("LblClient", cultureInfo);
           LblSize = Localize.GetString("LblSize", cultureInfo);
           LblSync = Localize.GetString("TxtSync", cultureInfo);
        }

        /// <summary>
        /// Posts the loading photo.
        /// </summary>
        /// <returns></returns>
        private async Task PostLoadingPhoto()
        {
            List<FileItemModel> photoListLoadingDetails;
            photoListLoadingDetails = _db.GetLoadingPhotoListDetail();
            List<ItemPhotoModel> ListForOneBarcode;

            if (photoListLoadingDetails.Count > 0)
            {
                for (int i = 0; i < photoListLoadingDetails.Count; i++)
                {
                    ListForOneBarcode = new List<ItemPhotoModel>();
                    if (!string.IsNullOrEmpty(photoListLoadingDetails[i].PhotoPath))
                    {
                        string[] photoPathListForEachBarcode = photoListLoadingDetails[i].PhotoPath.Split(',');

                        for (int j = 0; j < photoPathListForEachBarcode.Length; j++)
                        {
                            string image64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(photoPathListForEachBarcode[j]);

                            ItemPhotoModel photoModel = new ItemPhotoModel()
                            {
                                Barecode = photoListLoadingDetails[i].Barecode,
                                Telephone = _fileInfo.Mobile_Number,
                                Comments = photoListLoadingDetails[i].Comments_loading,
                                OSDS = Constants.Loading,
                                Photo = image64
                            };
                            ListForOneBarcode.Add(photoModel);
                        }
                        await _newLoadingService.PackingCygestPostItemsPhotos(ListForOneBarcode);
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> No Photo To send");

            }
        }

        /// <summary>
        /// Posts the loading item.
        /// </summary>
        /// <returns></returns>
        private async Task PostLoadingItem()
        {
            List<FileItemModel> _item = _db.GetAllDetails();

            if (_item.Count > 0)
            {
                _sentItem = await _newLoadingService.PostPackingCygestNewLoaddingItems(_item);

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> No data To send");

            }
            UpdateSyncLoading();
        }

        private void UpdateSyncLoading()
        {
            if (_sentItem)
            {

                _db.UpdateLoadingToTrue();
                PopulateView();

            }
            SetActivityIndicator(false);
        }

        /// <summary>
        /// Perfoms the call.
        /// </summary>
        private async void PerfomCall()
        {
            SetActivityIndicator(true);

            if (CrossConnectivity.Current.IsConnected)
            {
                await PostLoadingPhoto();
                await PostLoadingItem();
            }
            else
            {
                //no internet conn
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> no internet conn");
                PageSercice.DisplayAlert("Synchronation Fail", "Check your network", "ok");
                SetActivityIndicator(false);

            }
        }



        private void SetActivityIndicator(bool status)
        {
            HasActivtyLoad = status;

        }



    }
}

