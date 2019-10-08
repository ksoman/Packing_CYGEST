using PackingCygest.Interface;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Connectivity;
using PackingCygest.Model;
using System.Collections.Generic;
using PackingCygest.Service;
using PackingCygest.Data;
using System.Collections.ObjectModel;
using PackingCygest.Utils;
using System.Reflection;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// Configuartion Class model
    /// </summary>
    public class ConfigurationViewModel : BaseViewModel
    {
        #region private declaration
        private LoadingDetails _loadingDetails;
        private readonly IPageService _pageService;
        private ImageSource _imageSource;
        private bool _hasActivtyLoad;       
        private readonly ISqLitePath _sqlitepath;
        private readonly IEmailDump _emailSender;
        private ObservableCollection<CountryModel> _countries;
        private ObservableCollection<BranchModel> _branch;
        private CountryModel _countryDetails;
        private BranchModel _branchDetails;
        private string _mobileNumber;
        private const int BrandId = 0;
        private string _pckCountry;
        private string _lblConfig;
        private string _pckLanguage;
        private string _pckPhotoQuality;
        private string _pckBranch;
        private string _mobileNumberPlaceholder;
        private string _pckCountryDisplay;
        private string _pckLanguageDisplay;
        private string _pckBranchDisplay;
        private string _mobileNumberPlaceholderDisplay;
        private bool _activityIndicatorVisibility;
        private bool _stackLayoutVisibility;
        private bool _PackingCygestPageLayoutVisibility;
        private double _PackingCygestPageLayoutOpacity;
        private double _actOpacity;
        private bool _lblLoadingTextVisibility;
        private bool _mainStackLayout;
        private string _status;
        private string _telCode;
        private readonly BranchService _branchService = new BranchService();
        private readonly CountryService _countryService = new CountryService();
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private readonly LanguageModel _selectedLanguage = new LanguageModel();
        private List<ConfigurationModel> _configurationModel;
      
        private readonly DatabaseAccess  _dataAccess;
        private IList<LanguageModel> _languages;
        private string _pckAssistance;
        private ImageSource _lowClickImageSource;
        private ImageSource _mediumClickImageSource;
        private ImageSource _highClickImageSource;
        private string _lowRed;
        private string _medium;
        private string _high;
        private string _low;
        private string _mediumRed;
        private string _highRed;
        private string _lblNoNetwork;
        private string _lblPleaseEnableYourNetwork;
        private string _instanceVariablePhotoQuality;
        private bool _newConfig;
        #endregion

        #region Public declaration
        //Parvesh - Start
        public ICommand DumpClick { get; set; }
        public ICommand LowClicked { get; set; }
        public ICommand MediumClicked { get; set; }
        public ICommand HighClicked { get; set; }
        public ICommand LowUnClick { get; set; }
        public string QualityButtonValue { get; private set; }
        public string PhotoQualityButtons;
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string FillSvg => "PackingCygest.Image.fill.svg";
        public string LblDumpTitle { get; private set; }
        public string LblDumpNotSent { get; private set; }
        public string LblDumpSent { get; private set; }
        public string LblOk { get; private set; }
        public string LblCancel { get; private set; }
        public string LblCompleted { get; set; }
        public string LblError { get; private set; }
        public string LblNoConnection { get; private set; }
        public string LblSaveError { get; private set; }
        public string LblSaveSuccessfully { get; private set; }
        public string LblSave { get; private set; }
        public ISqLiteDb SqlDb { get; }
        public ICommand GoToPackingCygest { get; set; }


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

        public string InstanceVariablePhotoQuality
        {
            get { return _instanceVariablePhotoQuality; }
            set { SetValue(ref _instanceVariablePhotoQuality, value); }
        }

        public string LowRed
        {
            get { return _lowRed; }
            set { SetValue(ref _lowRed, value); }
        }

        public string Medium
        {
            get { return _medium; }
            set { SetValue(ref _medium, value); }
        }

        public string High
        {
            get { return _high; }
            set { SetValue(ref _high, value); }
        }

        public string Low
        {
            get { return _low; }
            set { SetValue(ref _low, value); }
        }

        public string MediumRed
        {
            get { return _mediumRed; }
            set { SetValue(ref _mediumRed, value); }
        }

        public string HighRed
        {
            get { return _highRed; }
            set { SetValue(ref _highRed, value); }
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        /// <value>
        /// The image source.
        /// </value>
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            set { SetValue(ref _imageSource, value); }
        }

        /// <summary>
        /// Gets or sets image of LOW quality button.
        /// </summary>
        public ImageSource LowClickImageSource
        {
            get { return _lowClickImageSource; }
            set { SetValue(ref _lowClickImageSource, value); }
        }

        /// <summary>
        /// Gets or sets image of Medium quality button.
        /// </summary>
        public ImageSource MediumClickImageSource
        {
            get { return _mediumClickImageSource; }
            set { SetValue(ref _mediumClickImageSource, value); }
        }

        /// <summary>
        /// Gets or sets image of High quality button.
        /// </summary>
      
        public ImageSource HighClickImageSource
        {
            get { return _highClickImageSource; }
            set { SetValue(ref _highClickImageSource, value); }

        }

        /// <summary>
        /// Sets the status of the activity load
        /// </summary>
        /// <value>
        ///   <c>Loading data...</c> if this instance has activty load; otherwise, <c>Saving data...</c>.
        /// </value>
        public string Status
        {
            get { return _status; }
            set { SetValue(ref _status, value); }
        }

        public string TelCode
        {
            get { return _telCode; }
            set { SetValue(ref _telCode, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has activty load.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has activty load; otherwise, <c>false</c>.
        /// </value>
        public bool HasActivtyLoad
        {
            get { return _hasActivtyLoad; }
            set { SetValue(ref _hasActivtyLoad, value); }
        }

        public ObservableCollection<CountryModel> Countries
        {
            get { return _countries; }
            set { SetValue(ref _countries, value); }
        }

        public ObservableCollection<BranchModel> Branch
        {
            get { return _branch; }
            set { SetValue(ref _branch, value); }
        }

        public IList<LanguageModel> Languages
        {
            get { return _languages; }
            set { SetValue(ref _languages, value); }
        }

        public IList<LanguageModel> LanguageDetails { get; } = new List<LanguageModel>();

        public string LblConfig
        {
            get { return _lblConfig; }
            set { SetValue(ref _lblConfig, value); }
        }


        public string PckCountry
        {
            get { return _pckCountry; }
            set { SetValue(ref _pckCountry, value); }
        }

        public string PckCountryDisplay
        {
            get { return _pckCountryDisplay; }
            set { SetValue(ref _pckCountryDisplay, value); }
        }
        
        public string PckLanguageDisplay
        {
            get { return _pckLanguageDisplay; }
            set { SetValue(ref _pckLanguageDisplay, value); }
        }

        public string MobileNumberplaceholderDisplay
        {
            get { return _mobileNumberPlaceholderDisplay; }
            set { SetValue(ref _mobileNumberPlaceholderDisplay, value); }
        }

        public string PckBranchDisplay
        {
            get { return _pckBranchDisplay; }
            set { SetValue(ref _pckBranchDisplay, value); }
        }
        public string PckLanguage
        {
            get { return _pckLanguage; }
            set { SetValue(ref _pckLanguage, value); }
        }

        public string PckPhotoQuality
        {
            get { return _pckPhotoQuality; }
            set { SetValue(ref _pckPhotoQuality, value); }
        }

        public string PckBranch
        {
            get { return _pckBranch; }
            set { SetValue(ref _pckBranch, value); }
        }

        public string MobileNumber
        {
            get { return _mobileNumber; }
            set { SetValue(ref _mobileNumber, value); }
        }

        public string MobileNumberPlaceholder
        {
            get { return _mobileNumberPlaceholder; }
            set { SetValue(ref _mobileNumberPlaceholder, value); }
        }

        public string PckAssistance
        {
            get { return _pckAssistance; }
            set { SetValue(ref _pckAssistance, value); }
        }

        public bool ActivityIndicatorVisibility
        {
            get { return _activityIndicatorVisibility; }
            set { SetValue(ref _activityIndicatorVisibility, value); }
        }
        public bool MainStackLayout
        {
            get { return _mainStackLayout; }
            set { SetValue(ref _mainStackLayout, value); }
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
        public double ActOpacity
        {
            get { return _actOpacity; }
            set { SetValue(ref _actOpacity, value); }
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

        public bool NewConfig
        {
            get { return _newConfig; }
            set { SetValue(ref _newConfig, value); }
        }

        #endregion

        /// <summary>
        /// Populate the languages.
        /// </summary>
        public void ShowLanguages()
        {
            int i = 0;

            while (i < 2)
            {
                LanguageModel language = new LanguageModel();

                if (i == 0)
                {
                    language.Id = 1;
                    language.Name = "English";
                    language.Culture = "en-US";
                }
                else
                {
                    language.Id = 2;
                    language.Name = "French";
                    language.Culture = "fr-FR";
                }
                LanguageDetails.Add(language);
                i++;
            }
            Languages = LanguageDetails;
        }

        /// <summary>
        /// Handles the translation of text found on the configuration form
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblConfig = Localize.GetString("LblConfig", cultureInfo);
            PckCountry = Localize.GetString("PckCountry", cultureInfo);
            PckLanguage = Localize.GetString("PckLanguage", cultureInfo);
            PckBranch = Localize.GetString("PckBranch", cultureInfo);
            PckCountryDisplay = Localize.GetString("PckCountryDisplay", cultureInfo);
            PckLanguageDisplay = Localize.GetString("PckLanguageDisplay", cultureInfo);
            PckBranchDisplay = Localize.GetString("PckBranchDisplay", cultureInfo);
            PckPhotoQuality = Localize.GetString("PckPhotoQuality", cultureInfo);
            MobileNumberPlaceholder = Localize.GetString("MobileNumberPlaceholder", cultureInfo);
            MobileNumberplaceholderDisplay = Localize.GetString("MobileNumberplaceholderDisplay", cultureInfo);
            PckAssistance = Localize.GetString("PckAssistance", cultureInfo);
            LblDumpTitle  = Localize.GetString("lblDumpTitle", cultureInfo);
            LblDumpSent = Localize.GetString("lblDumpSent", cultureInfo);
            LblDumpNotSent = Localize.GetString("lblDumpNotSent", cultureInfo);
            LblOk = Localize.GetString("lblOK", cultureInfo);
            LblCancel = Localize.GetString("lblCancel", cultureInfo);
            LblError = Localize.GetString("lblError", cultureInfo);
            LblNoConnection = Localize.GetString("lblNoConnection", cultureInfo);
            LblSaveError = Localize.GetString("lblSaveError", cultureInfo); 
            LblSaveSuccessfully = Localize.GetString("lblSaveSuccessfully", cultureInfo);
            LblSave = Localize.GetString("lblSave", cultureInfo);

            LowRed = Localize.GetString("LowRed", cultureInfo);
            MediumRed = Localize.GetString("MediumRed", cultureInfo);
            HighRed = Localize.GetString("HighRed", cultureInfo);

            Low = Localize.GetString("Low", cultureInfo);
            Medium = Localize.GetString("Medium", cultureInfo);
            High = Localize.GetString("High", cultureInfo);

            LblNoNetwork = Localize.GetString("LblNoNetwork", cultureInfo);
            LblPleaseEnableYourNetwork = Localize.GetString("LblPleaseEnableYourNetwork", cultureInfo);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        public ConfigurationViewModel(IPageService pageService)
        {            
            TelCode = "( I D D )";
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            SetActivityIndicatorBlurred(true);
            ShowLanguages();
            SqlDb = DependencyService.Get<ISqLiteDb>();
            _sqlitepath = DependencyService.Get<ISqLitePath>();
            SqlDb.GetConnection();
            _pageService = pageService;
            GoToPackingCygest = new Command(LoadPackingCygest);
            _emailSender = DependencyService.Get<IEmailDump>();
            DumpClick = new Command(DumpAsync);
            LowClicked = new Command(LowButton);
            MediumClicked = new Command(MediumButton);
            HighClicked = new Command(HighButton);
            LowClickImageSource = ImageSource.FromFile("Low_Button.png");
            MediumClickImageSource = ImageSource.FromFile("Medium_Button.png");
            HighClickImageSource = ImageSource.FromFile("High_Button.png");


            QualityButtonValue = "Null";
            PackingCygestPageLayoutOpacity = 1;
            if (_appViewModel.DefaultedCultureInfo != null)
            {
                GetAllCountries(_appViewModel.DefaultedCultureInfo);
            }
           
            _dataAccess = new DatabaseAccess();
            PckCountryDisplay = "Countries";
            PckLanguageDisplay = "Language";
            PckBranchDisplay = "Branch";
            MobileNumberplaceholderDisplay = "Mobile Number";
            NewConfig = true;
            PopulateExistingUserDetails();
        }

        /// <summary>
        /// Loads the PackingCygest.
        /// </summary>
        public void LoadPackingCygest()
        {
            _pageService.PushAsync(new View.PackingCygest());
        }
       
        /// <summary>
        /// Lows the button.
        /// </summary>
        public void LowButton()
        {          
            QualityButtonValue = "";
            LowClickImageSource = ImageSource.FromFile(LowRed);
            MediumClickImageSource = ImageSource.FromFile(Medium);
            HighClickImageSource = ImageSource.FromFile(High);

            QualityButtonValue = "Low";
            InstanceVariablePhotoQuality = "Low";
            SelectedPhotoQuality(QualityButtonValue);
        }
        /// <summary>
        /// Mediums the button.
        /// </summary>
        public void MediumButton()
        {
            QualityButtonValue = "";
            LowClickImageSource = ImageSource.FromFile(Low);
            MediumClickImageSource = ImageSource.FromFile(MediumRed);
            HighClickImageSource = ImageSource.FromFile(High);

            QualityButtonValue = "Medium";
            InstanceVariablePhotoQuality = "Medium";
            SelectedPhotoQuality(QualityButtonValue);

        }

        /// <summary>
        /// Highes the button.
        /// </summary>
        public void HighButton()
        {
            QualityButtonValue = "";
            LowClickImageSource = ImageSource.FromFile(Low);
            MediumClickImageSource = ImageSource.FromFile(Medium);
            HighClickImageSource = ImageSource.FromFile(HighRed);

            QualityButtonValue = "High";
            InstanceVariablePhotoQuality = "High";

            SelectedPhotoQuality(QualityButtonValue);
        }
        /// <summary>
        ///the dump functionality.
        /// </summary>
        public async Task DumpFunctionAsync()
        {
            
            SetActivityIndicatorBlurred(true);
            ActOpacity = 1;
            MainStackLayout = false;
            PackingCygestPageLayoutOpacity = 0.4;
            var path = _sqlitepath.GetSqLitePath("PackingCygestAppDB.db3");
            var success = await _emailSender.SendEmailDump(path);
            if (success)
            {
                await _pageService.DisplayAlert(LblDumpTitle ,  LblDumpSent, LblOk);
                SetActivityIndicatorBlurred(false);
                MainStackLayout = true;
                PackingCygestPageLayoutOpacity = 1;
                Status = "";
            }
            else
            {
                await _pageService.DisplayAlert(LblDumpTitle, LblDumpNotSent, LblOk, LblCancel);
                SetActivityIndicatorBlurred(false);
                MainStackLayout = true;
                PackingCygestPageLayoutOpacity = 1;
                Status = "";
            }   
        }

        public async Task SaveConfig()
        {
            var connected = CrossConnectivity.Current.IsConnected;

            if(connected)
            {
               // bool isNumeric = int.TryParse(MobileNumberplaceholderDisplay, out int n);

                if (_selectedLanguage.Culture != null && _selectedLanguage.Name != null && _countryDetails != null && _branchDetails != null && PhotoQualityButtons != null &&  !string.IsNullOrEmpty(MobileNumber))
                {
                    SetActivityIndicatorBlurred(true);
                    ActOpacity = 1;
                    MainStackLayout = false;
                    PackingCygestPageLayoutOpacity = 0.4;
                    Status = "Saving data...";
                    _configurationModel = _dataAccess.GetAllConfigData();

                    var configurationModel = new ConfigurationModel
                    {
                        Id = _countryDetails.Id,
                        Country = _countryDetails.Name,
                        TelCode = TelCode,
                        MobileNumber = MobileNumber,
                        Language = _selectedLanguage.Name,
                        CultureInfo = _selectedLanguage.Culture,
                        PhotoQuality = PhotoQualityButtons,
                        Branch = _branchDetails.Name,
                        BranchCode= _branchDetails.Code
                    };

                    if (_configurationModel.Count != 0)
                    {
                        configurationModel.Id = 1;
                        _dataAccess.UpdateConfigurationData(configurationModel);
                    }
                    else
                    {
                        _dataAccess.InsertConfig(configurationModel);
                    }
                    await GetGenericDetailsAsync();

                    await _pageService.DisplayAlert(LblSave, LblSaveSuccessfully, LblOk);

                    HandleTranslation(_selectedLanguage.Culture);

                    LowRed = Localize.GetString("LowRed", _selectedLanguage.Culture);
                    MediumRed = Localize.GetString("MediumRed", _selectedLanguage.Culture);
                    HighRed = Localize.GetString("HighRed", _selectedLanguage.Culture);
                    Low = Localize.GetString("Low", _selectedLanguage.Culture);
                    Medium = Localize.GetString("Medium", _selectedLanguage.Culture);
                    High = Localize.GetString("High", _selectedLanguage.Culture);

                    if (InstanceVariablePhotoQuality.Equals("Low"))
                    {
                        LowClickImageSource = ImageSource.FromFile(LowRed);
                        MediumClickImageSource = ImageSource.FromFile(Medium);
                        HighClickImageSource = ImageSource.FromFile(High);

                        InstanceVariablePhotoQuality = "Low";
                    }
                    else if (InstanceVariablePhotoQuality.Equals("Medium"))
                    {
                        LowClickImageSource = ImageSource.FromFile(Low);
                        MediumClickImageSource = ImageSource.FromFile(MediumRed);
                        HighClickImageSource = ImageSource.FromFile(High);

                        InstanceVariablePhotoQuality = "Medium";
                    }
                    else if (InstanceVariablePhotoQuality.Equals("High"))
                    {
                        LowClickImageSource = ImageSource.FromFile(Low);
                        MediumClickImageSource = ImageSource.FromFile(Medium);
                        HighClickImageSource = ImageSource.FromFile(HighRed);

                        InstanceVariablePhotoQuality = "High";
                    }
                    else
                    {
                        LowClickImageSource = ImageSource.FromFile(Low);
                        MediumClickImageSource = ImageSource.FromFile(Medium);
                        HighClickImageSource = ImageSource.FromFile(High);
                    }

                    NewConfig = false;
                }
                else
                {
                    await _pageService.DisplayAlert(LblError, LblSaveError, LblOk);
                }
                SetActivityIndicatorBlurred(false);
                MainStackLayout = true;
                PackingCygestPageLayoutOpacity = 1;
            }
            else
            {
                await _pageService.DisplayAlert(LblNoNetwork, LblPleaseEnableYourNetwork, "ok");

            }



        }
        /// <summary>
        /// Gets the generic details asynchronous.
        /// </summary>
        /// <returns></returns>
        private async Task GetGenericDetailsAsync()
        {
            _loadingDetails = new LoadingDetails();
            await _loadingDetails.GetRooms();
            await _loadingDetails.GetRubrics();
            await _loadingDetails.GetItemDetails();
        }

        /// <summary>
        /// Gets all countries.
        /// </summary>
        /// <param name="cultureDetails">The culture details.</param>
        /// <returns></returns>
        public async Task GetAllCountries(string cultureDetails)
        {
            try
            {
                SetActivityIndicatorBlurred(true);
                ActOpacity = 1;
                MainStackLayout = false;
                PackingCygestPageLayoutOpacity = 0.4;
                Status = "Loading data...";
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    Countries = await _countryService.GetAllCountriesAsync(cultureDetails.Substring(0, 2));


                    if (Countries != null)
                    {                        
                        SetActivityIndicatorBlurred(false);
                        MainStackLayout = true;
                        PackingCygestPageLayoutOpacity = 1;
                        Status = "";
                    }
                    else
                    {
                        Countries = new ObservableCollection<CountryModel>();
                        await _pageService.DisplayAlert(LblError, LblNoConnection, LblOk);
                    }
                    SetActivityIndicatorBlurred(false);
                    MainStackLayout = true;
                    PackingCygestPageLayoutOpacity = 1;
                    Status = "";
                }
            }
          
            catch (Exception e)
            {
                await _pageService.DisplayAlert(LblError, LblNoConnection, LblOk);
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });

            }

        }


        /// <summary>
        /// Gets the branches details.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="cultureInfo">The culture information.</param>
        /// <returns></returns>
        public async Task GetBranchesDetails(int countryId, string cultureInfo)
        {
            try
            {
                SetActivityIndicatorBlurred(true);
                ActOpacity = 1;
                MainStackLayout = false;
                PackingCygestPageLayoutOpacity = 0.4;
                Status = "Loading data...";
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    Branch = await _branchService.GetBranchDetailsAsync(countryId, cultureInfo, BrandId);

                    if(Branch.Count==0 && _appViewModel.DefaultedCultureInfo != "fr-FR")
                    {
                        BranchModel tempBranch = new BranchModel
                        {
                            Id = 0,
                            Code = "Unknown",
                            Name = "Unknown"
                        };
                        Branch.Add(tempBranch);
                    }
                    else
                    {
                        BranchModel tempBranch = new BranchModel
                        {
                            Id = 0,
                            Code = "Inconnu",
                            Name = "Inconnu"
                        };
                        Branch.Add(tempBranch);

                    }

                    SetActivityIndicatorBlurred(false);
                    MainStackLayout = true;
                    PackingCygestPageLayoutOpacity = 1;
                    Status = "";

                }
            }
            catch (Exception e)
            {
                await _pageService.DisplayAlert(LblError, LblNoConnection, LblOk); 
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });

            }
        }

        /// <summary>
        /// Selecteds the language details.
        /// </summary>
        /// <param name="language">The language.</param>
        public void SelectedLanguageDetails(LanguageModel language)
        {
            _selectedLanguage.Culture = language.Culture;
            _selectedLanguage.Name = language.Name;
            EnableOkButton();

        }

        /// <summary>
        /// Selecteds the country details.
        /// </summary>
        /// <param name="country">The country.</param>
        public async Task SelectedCountryDetails(CountryModel country)
        {
            _countryDetails = country;
            EnableOkButton();
            if (_countryDetails != null)
            {
                if(!string.IsNullOrEmpty(_countryDetails.TelCode))
                {
                    TelCode = _countryDetails.TelCode;
                }
                else
                {
                    TelCode = "000";
                }

                string _lang = "";
                if(!string.IsNullOrEmpty(_appViewModel.DefaultedCultureInfo))
                {
                    _lang = _appViewModel.DefaultedCultureInfo.Substring(0, 2);
                }
                else
                {
                    _lang = "en";

                }
                await GetBranchesDetails(_countryDetails.Id, _lang);
            }
        }

        /// <summary>
        /// Selecteds the branch.
        /// </summary>
        /// <param name="branch">The branch.</param>
        public void SelectedBranch(BranchModel branch)
        {
            _branchDetails = branch;
            EnableOkButton();
        }
        /// <summary>
        /// Dumps the asynchronous.
        /// </summary>
        public async void DumpAsync()
        {
            await DumpFunctionAsync();
        }
        /// <summary>
        /// Selecteds the photo quality.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public string SelectedPhotoQuality(string photo)
        {
            if (photo != null)
            {
                PhotoQualityButtons = photo;
            }
            EnableOkButton();

            return PhotoQualityButtons;
          
        }

        /// <summary>
        /// Populates existing user's configuration details.
        /// </summary>
        /// <returns></returns>
        public async void PopulateExistingUserDetails()
        {
            List<ConfigurationModel> GetExistingDetails =  _dataAccess.GetAllConfigData();

            string photoQuality = "";

            foreach(var details in GetExistingDetails)
            {
                PckCountryDisplay = details.Country;
                TelCode = details.TelCode;
                MobileNumberplaceholderDisplay = details.MobileNumber;
                PckBranchDisplay = details.Branch;
                PckLanguageDisplay = details.Language;
                photoQuality = details.PhotoQuality;
                InstanceVariablePhotoQuality = details.PhotoQuality;

                if (photoQuality.Equals("Low"))
                {
                    LowButton();
                }
                if (photoQuality.Equals("Medium"))
                {
                    MediumButton();
                }
                if (photoQuality.Equals("High"))
                {
                    HighButton();
                }
            }

            if (GetExistingDetails.Count != 0){
                NewConfig = false;
            }
        }


        /// <summary>
        /// Enables the ok button.
        /// </summary>
        public void EnableOkButton(){

            if (_selectedLanguage.Culture != null && _selectedLanguage.Name != null && _countryDetails != null && _branchDetails != null && PhotoQualityButtons != null){
                NewConfig = true;
            }
        }

        /// <summary>
        /// Sets the activity indicator.
        /// </summary>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        private void SetActivityIndicatorBlurred(bool visibility)
        {
            ActivityIndicatorVisibility = visibility;
            StackLayoutVisibility = visibility;
            LblLoadingTextVisibility = true;
            MainStackLayout = false;

        }

    }
}
