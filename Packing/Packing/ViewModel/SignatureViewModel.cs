using System;
using System.IO;
using PackingCygest.Interface;
using Xamarin.Forms;
using PackingCygest.Model;
using PackingCygest.Data;
using PackingCygest.Service;
using System.Windows.Input;
using PackingCygest.Utils;
using System.Reflection;
using System.Threading.Tasks;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// SignatureViewModel
    /// </summary>
    public class SignatureViewModel : BaseViewModel
    {
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        private bool _activityRunning;
        private bool _activityEnabled;
        public ICommand BackToPackingCygest { get; set; }
        NewLoadingService service = new NewLoadingService();
        private IPageService _pageService;
        DatabaseAccess _dbAccess;
        public FileInfoModel InfoModel { get; }
        public FileInfoDeliveryModel InfoDelModel { get; }
        private string _myImagePath;
        private string _lblFileNumber;
        private string _lblClientNumber;
        private string _volume;
        private string _loadingCountry;
        private string _destinationCountry;
        private string _lblFile;
        private string _lblClient;
        private string _lblSize;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

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
        public IPageService PageSercice
        {
            get => _pageService;
            set => _pageService = value;
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

        /// <summary>
        /// Gets or sets a value indicating whether [activity running].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [activity running]; otherwise, <c>false</c>.
        /// </value>
        public bool ActivityRunning
        {
            get { return _activityRunning; }
            set { SetValue(ref _activityRunning, value); }

        }
        /// <summary>
        /// Gets or sets a value indicating whether [activity enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [activity enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool ActivityEnabled
        {
            get { return _activityEnabled; }
            set { SetValue(ref _activityEnabled, value); }

        }

        public string MyImagePath
        {
            get { return _myImagePath; }
            set { SetValue(ref _myImagePath, value); }

        }

        public string BtnDone { get; set; }
        public string BtnClear { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignatureViewModel"/> class.
        /// </summary>
        public SignatureViewModel()
        {
            
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
        /// Initializes a new instance of the <see cref="SignatureViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="info"></param>
        public SignatureViewModel(PageService pageService, FileInfoModel info)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _pageService = pageService;
            InfoModel = info;
            SetValueByBinding(InfoModel);
            
            BackToPackingCygest = new Command(ToPackingCygest);
            SetTransportImage(info);
        }

        public SignatureViewModel(PageService pageService, FileInfoDeliveryModel info)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _pageService = pageService;
            InfoDelModel = info;
            SetValueByBinding(InfoDelModel);
           
            BackToPackingCygest = new Command(ToPackingCygest);
            SetTransportImage(InfoDelModel);
        }
        public void HandleTranslation(string cultureInfo)
        {
            LblFile = Localize.GetString("LblFile", cultureInfo);
            LblClient = Localize.GetString("LblClient", cultureInfo);
            LblSize = Localize.GetString("LblSize", cultureInfo);
            BtnDone = Localize.GetString("LoadingTextDone", cultureInfo);
            BtnClear = Localize.GetString("BtnClear", cultureInfo);
        }
        private async void ToPackingCygest()
        {
            await PageSercice.PushAsync(new View.PackingCygest());
        }
        public void SetValueByBinding(FileInfoModel fileInfo)
        {
            LblFileNumber = fileInfo.File_number;
            LblClientNumber = fileInfo.Client_number;
            Volume = fileInfo.Volume;
            LoadingCountry = fileInfo.Country_loading;
            DestinationCountry = fileInfo.Country_delivery;

        }

        public void SetValueByBinding(FileInfoDeliveryModel fileInfo)
        {
            LblFileNumber = fileInfo.File_number;
            LblClientNumber = fileInfo.Client_number;
            Volume = fileInfo.Volume;
            LoadingCountry = fileInfo.Country_loading;
            DestinationCountry = fileInfo.Country_delivery;

        }


        /// <summary>
        /// Saves the image path.
        /// </summary>
        /// <param name="imgPath">The img path.</param>
        /// <param name="state">The state.</param>
        public async Task SaveImagePath(string imgPath,string state)
        {
            _dbAccess = new DatabaseAccess();
            ActivityEnabled = true;
            ActivityRunning = true;
            if (imgPath != null)
            {
                 _dbAccess.InsertPhoto(new PhotoShotModel
                {
                    Path = imgPath,
                });
                string imageBase64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(imgPath);
                if (state.Equals("Loading"))
                {
                    PostSignatureAsync(InfoModel, imageBase64);

                }
                else
                {
                    PostSignatureAsync(InfoDelModel, imageBase64);

                }
            }

        }

        private async void PostSignatureAsync(FileInfoModel fileInfo, string imageBase)
        {
            SignatureModel signatureModel = new SignatureModel();
            signatureModel.File_number = fileInfo.File_number;
            signatureModel.Telephone = fileInfo.Mobile_Number;
            signatureModel.OSDS = Constants.Loading;
            signatureModel.Image = imageBase;

            await service.PackingCygestPostSignature(signatureModel);
            ActivityEnabled = false;
            ActivityRunning = false;
        }

        private async void PostSignatureAsync(FileInfoDeliveryModel fileInfo, string imageBase)
        {
            SignatureModel signatureModel = new SignatureModel();
            signatureModel.File_number = fileInfo.File_number;
            signatureModel.Telephone = fileInfo.Mobile_Number;
            signatureModel.OSDS = Constants.Delivery;
            signatureModel.Image = imageBase;

            await service.PackingCygestPostSignature(signatureModel);
            ActivityEnabled = false;
            ActivityRunning = false;
        }


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
        /// <summary>
        /// Saves as picture.
        /// </summary>
        /// <param name="image">The image.</param>
        public void SaveAsPicture(Stream image)
        {
            var result = ConvertSignatureToBase64(image);
            DependencyService.Get<ISavePhoto>().SavePicture(result);
            _pageService.DisplayAlert("Operation", "Saved", "ok", "cancel");
        }

        /// <summary>
        /// Converts the signature to base64.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <returns></returns>
        private byte[] ConvertSignatureToBase64(Stream img)
        {
            try
            {
                byte[] data;
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var signatureMemoryStream = new MemoryStream();
                    img.CopyTo(signatureMemoryStream);
                    data = signatureMemoryStream.ToArray();
                }
                else
                {
                    var signatureMemoryStream = (MemoryStream)img;
                    data = signatureMemoryStream.ToArray();
                }

                return data;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return null;
        }
    }
       
}
