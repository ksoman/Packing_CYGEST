using PackingCygest.Interface;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;
using PackingCygest.Utils;

namespace PackingCygest.ViewModel
{
    public class CompletedViewModel : BaseViewModel
    {

        private readonly IPageService _pageService;
        public ICommand BackToPackingCygest { get; set; }
        public IPageService PageService { get; set; }      
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string SvgPathCompleted => "PackingCygest.Image.complete.svg";
        private string _lblCompleteMsg1;
        private string _lblCompleteMsg2;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private string _lblcompletedTile;

        /// <summary>
        /// Gets or sets the label complete MSG1.
        /// </summary>
        /// <value>
        /// The label complete MSG1.
        /// </value>
        public string LblCompleteMsg1
        {
            get { return _lblCompleteMsg1; }
            set { SetValue(ref _lblCompleteMsg1, value); }
        }
        /// <summary>
        /// Gets or sets the label complete MSG2.
        /// </summary>
        /// <value>
        /// The label complete MSG2.
        /// </value>
        public string LblCompleteMsg2
        {
            get { return _lblCompleteMsg2; }
            set { SetValue(ref _lblCompleteMsg2, value); }
        }
        /// <summary>
        /// Gets the SVG assemblies.
        /// </summary>
        /// <value>
        /// The SVG assemblies.
        /// </value>
        public Assembly SvgAssemblies
        {
            get { return typeof(App).GetTypeInfo().Assembly; }
        }


        /// <summary>
        /// Gets or sets the label completed tile.
        /// </summary>
        /// <value>
        /// The label completed tile.
        /// </value>
        public string LblCompletedTile
        {
            get { return _lblcompletedTile; }
            set { SetValue(ref _lblcompletedTile, value); }            
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CompletedViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        public CompletedViewModel(IPageService pageService,string status)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo,status);
            _pageService = pageService;
            BackToPackingCygest = new Command(LoadPackingCygest);
        }
        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo,string status)
        {
            if(status.Equals("Loading"))
            {
                LblCompleteMsg1 = Localize.GetString("CompleteMsg1", cultureInfo);
                LblCompleteMsg2 = Localize.GetString("CompleteMsg2", cultureInfo);
                LblCompletedTile = Localize.GetString("LblCompletedTile", cultureInfo);
            }
            else if(status.Equals("Delivery"))
            {
                LblCompleteMsg1 = Localize.GetString("DeliveryMsg1", cultureInfo);
                LblCompleteMsg2 = Localize.GetString("DeliveryMsg2", cultureInfo);
                LblCompletedTile = Localize.GetString("LblCompletedTile", cultureInfo);
            }
            else
            {                //PackingCygest
                LblCompleteMsg1 = Localize.GetString("CompleteMsg1", cultureInfo);
                LblCompleteMsg2 = Localize.GetString("CompleteMsg2", cultureInfo);
                LblCompletedTile = Localize.GetString("LblCompletedTile", cultureInfo);
            }

        }
        /// <summary>
        /// Loads the PackingCygest.
        /// </summary>
        public void LoadPackingCygest()
        {
            _pageService.PushAsync(new View.PackingCygest());
        }
    }
}
