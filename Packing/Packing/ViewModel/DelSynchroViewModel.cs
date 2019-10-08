using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.Utils;
using PackingCygest.View;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PackingCygest.ViewModel
{
    /// <summary>
    /// DelSynchroViewModel
    /// </summary>
    class DelSynchroViewModel:BaseViewModel
    {
        private int _progressValue;
        public IPageService PageService { get; set; }
        public ICommand FinshSynchro { get; set; }
        public ICommand Back { get; set; }
        public FileItemModel SynchroDelivery { get; }
        public FileInfoDeliveryModel InfoModel { get; }
        readonly DatabaseAccessAsync _dbAccess;
        private DeliveryEndControls _endControls;
        private string _loadingText;
        private bool _loadingIndicator;
        private string _loadingTextDone;
        private string _loadingTextWait;
        private bool _sentPhoto=false;
        private bool _sentItem= false;
        private string _lblSynchro;
        private string _lblProgress;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        public int ProgressValue
        {
            get { return _progressValue; }
            set { SetValue(ref _progressValue, value); }
        }
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
        public bool LoadingIndicator
        {
            get { return _loadingIndicator; }
            set { SetValue(ref _loadingIndicator, value); }
        }
        public string LoadingText
        {
            get { return _loadingText; }
            set { SetValue(ref _loadingText, value); }
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
        readonly DatabaseAccess _db;

        private DeliveryService _service= new DeliveryService();

        private bool _isSucess;



        /// <summary>
        /// Initializes a new instance of the <see cref="DelSynchroViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="infomodel">The infomodel.</param>
        public DelSynchroViewModel(IPageService pageService, FileInfoDeliveryModel infomodel)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageService = pageService;
            InfoModel = infomodel;
            _dbAccess = new DatabaseAccessAsync();
            _db = new DatabaseAccess();
            Back = new Command(GoBack);
            ProgressValue = 0;
            LoadingText = LoadingTextWait;
            LoadingIndicator = true;
            PerfomCall();

        }
        public void HandleTranslation(string cultureInfo)
        {
            LblSynchro = Localize.GetString("LblSynchro", cultureInfo);
            LblProgress = Localize.GetString("LblProgress", cultureInfo);
            LoadingTextWait = Localize.GetString("LoadingTextWait", cultureInfo);
            LoadingTextDone = Localize.GetString("LoadingTextDone", cultureInfo);

        }
        private async void PerfomCall()
        {
            var connected = CrossConnectivity.Current.IsConnected;
            if(connected)
            {
                _isSucess = true;
                await PostPhoto();
                await PostItem();
                await GetSummaryDelDetails();
            }
            else
            {
                _isSucess = false;
               await PageService.DisplayAlert("Synchronation Fail", "Check your network", "ok");
                await GetSummaryDelDetails();
            }

        }

        /// <summary>
        /// Goes to controls.
        /// </summary>
        public  void UpdateSync()    
        {
            if(_sentItem)
            {
                _db.SentDataToServer();

            }

        }

        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {
             PageService.PopAsync();
        }


        private async Task PostPhoto()
        {
            var photoListDetails = _db.GetPhotoListDetail();
            List<ItemPhotoModel> listForOneBarcode;

            if (photoListDetails.Count > 0)
            {
                for(int i=0;i<photoListDetails.Count;i++)
                {
                    listForOneBarcode = new List<ItemPhotoModel>();
                    if (!string.IsNullOrEmpty(photoListDetails[i].PhotoPath))
                    {
                        string[]  photoPathListForEachBarcode = photoListDetails[i].PhotoPath.Split(',');

                        for(int j=0;j< photoPathListForEachBarcode.Length;j++)
                        {
                            string image64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(photoPathListForEachBarcode[j]);

                            ItemPhotoModel photoModel = new ItemPhotoModel()
                            {
                                Barecode = photoListDetails[i].Barecode,
                                Telephone = InfoModel.Mobile_Number,
                               // Telephone = "230",
                                Comments = photoListDetails[i].comments_Delivery,
                                OSDS = Constants.Delivery,
                                Photo = image64
                            };
                            listForOneBarcode.Add(photoModel);
                        }
                    }

                    _sentPhoto=await _service.PostItemsPhotos(listForOneBarcode);
                }
               
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> No Photo To send");

            }

            ProgressValue = 50;
       
            LoadingIndicator = true;
        }

        private async Task PostItem()
        {
            List<ItemDeliveredModel> item;
            item = _db.GetItemDeliveryDetail();
            if (item.Count > 0)
            {
                  _sentItem=  await  _service.PostItemsDelivery(item);
                  UpdateSync();

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> No data To send");

            }

            ProgressValue = 90;
            LoadingIndicator = true;
            await Task.Delay(TimeSpan.FromSeconds(1));

        }

        private async Task GetSummaryDelDetails()
        {
            if (InfoModel.File_number != null)
            {
                 _endControls = await _service.GetDeliveryEndControl(InfoModel.File_number);

            }
            else
            {
                _endControls.file_number = null;
            }
            ProgressValue = 100;
            LoadingText = "";
            LoadingIndicator = false;
            await Task.Delay(TimeSpan.FromSeconds(1));
            await PageService.PushAsync(new DelFinalSummary(InfoModel, _endControls,_isSucess));

        }


    }
}
