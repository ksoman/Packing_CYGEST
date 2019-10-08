using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.View;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Connectivity;
using PackingCygest.Utils;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    public class LoadSynchroViewModel: BaseViewModel
    {
        #region Private   
        private int _progressValue;
        private readonly int _progressSentPhoto = 90;
        private readonly int _progressComplete = 100;
        private readonly int _progressStart = 0;
        private string _loadingText;
        private bool _loadingIndicator;
        private string _loadingTextDone;
        private string _loadingTextWait;
        private readonly DatabaseAccess _db;
        private readonly DatabaseAccessAsync _dbAsync;
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;       
        private bool _syncComplete;
        private List<FileItemModel> _item;
        private readonly NewLoadingService _newLoadingService = new NewLoadingService();
        private bool _sentItem ;
        private string _lblSynchro;
        private string _lblProgress;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        #endregion

        #region Public
        public string LblSynchro
        {
            get { return _lblSynchro; }
            set { SetValue(ref _lblSynchro, value); }
        }
        public string LblProgress
        {
            get { return _lblProgress; }
            set { SetValue(ref _lblProgress, value); }
        }

        public string LoadingTextDone
        {
            get { return _loadingTextDone; }
            set { SetValue(ref _loadingTextDone, value); }
        }

        public string LoadingTextWait
        {
            get { return _loadingTextWait; }
            set { SetValue(ref _loadingTextWait, value); }
        }


        /// <summary>
        /// Gets or sets the page service.
        /// </summary>
        /// <value>
        /// The page service.
        /// </value>
        public IPageService PageService { get; set; }
        /// <summary>
        /// Gets or sets the finsh synchro.
        /// </summary>
        /// <value>
        /// The finsh synchro.
        /// </value>
        public ICommand FinshSynchro { get; set; }
        /// <summary>
        /// Gets or sets the synchronize complete.
        /// </summary>
        /// <value>
        /// The synchronize complete.
        /// </value>
        public bool SyncComplete
        {  
            get { return _syncComplete; }
            set { SetValue(ref _syncComplete, value); }
        }

        /// <summary>
        /// Gets or sets the progress value.
        /// </summary>
        /// <value>
        /// The progress value.
        /// </value>
        public int ProgressValue
        {
            get { return _progressValue; }
            set { SetValue(ref _progressValue, value); }
           
        }

        public string LoadingText
        {
            get { return _loadingText; }
            set { SetValue(ref _loadingText, value); }
        }

        public bool LoadingIndicator
        {
            get { return _loadingIndicator; }
            set { SetValue(ref _loadingIndicator, value); }
        }


        /// <summary>
        /// The end control loading
        /// </summary>
        public EndControlsLoadingModel EndControlLoading = new EndControlsLoadingModel();
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSynchroViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadSynchroViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageService = pageService;
            _loading = loading;
            _fileInfo = fileInfo;           
            _db = new DatabaseAccess();
            _dbAsync = new DatabaseAccessAsync();            
            PerfomCall();
            _db.EmptyDatabaseForLoadSynchro();
            FinshSynchro = new Command(GoToControls);
            LoadingText = LoadingTextWait;
            LoadingIndicator = true;
            SyncComplete = false;

        }
        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblSynchro = Localize.GetString("LblSynchro", cultureInfo);
            LblProgress = Localize.GetString("LblProgress", cultureInfo);
            LoadingTextWait = Localize.GetString("LoadingTextWait", cultureInfo);
            LoadingTextDone = Localize.GetString("LoadingTextDone", cultureInfo);

        }
        /// <summary>
        /// Goes to controls.
        /// </summary>
        public async void GoToControls()
        {          
            await PageService.PushAsync(new LoadEndControl(_loading, _fileInfo));
        }

        /// <summary>
        /// Inserts the end control.
        /// </summary>
        private async Task InsertEndControlAsync()
        {
            if (EndControlLoading != null)
            {

              await  _dbAsync.InsertEndControlsLoading(EndControlLoading);

            }

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
            ProgressValue = _progressStart;

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
            ProgressValue = _progressSentPhoto;
            //LoadingText = LoadingTextWait;
            LoadingIndicator = true;
        }

        /// <summary>
        /// Posts the loading item.
        /// </summary>
        /// <returns></returns>
        private async Task PostLoadingItem()
        {
            _item = _db.GetAllDetails();

            if (_item.Count > 0)
            {
                _sentItem= await _newLoadingService.PostPackingCygestNewLoaddingItems(_item);

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> No data To send");

            }
            UpdateSyncLoading();
        }

        /// <summary>
        /// Updates the synchronize loading.
        /// </summary>
        private void UpdateSyncLoading()
        {
            if(_sentItem)
            {

                _db.UpdateLoadingToTrue();

            }
            SyncComplete = true;

        }

        /// <summary>
        /// Gets the loading end controls data.
        /// </summary>
        /// <returns></returns>
        private async Task GetLoadingEndControlsData()
        {
            if(_loading.FileItemModel.File_Number!=null)
            {
                EndControlLoading = await _newLoadingService.GetLoadingEndControls(_loading.FileItemModel.File_Number);
                await InsertEndControlAsync();
                ProgressValue = _progressComplete;
                //LoadingText = LoadingTextDone;
                LoadingText = "";
                LoadingIndicator = false;
                SyncComplete = true;
            }

        }


        /// <summary>
        /// Perfoms the call.
        /// </summary>
        private async void PerfomCall()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                await PostLoadingPhoto();
                await PostLoadingItem();
                await GetLoadingEndControlsData();
            }
        }

    }
}
