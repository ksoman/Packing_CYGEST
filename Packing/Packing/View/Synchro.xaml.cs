using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// The Class Sel Synchro
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Synchro : ContentPage
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Synchro"/> class.
        /// </summary>
        /// <param name="delivery">The delivery.</param>
        public Synchro (FileInfoDeliveryModel infomodel)
		{
			InitializeComponent ();
            BindingContext = new DelSynchroViewModel(new PageService(), infomodel);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Synchro"/> class.
        /// </summary>
        /// <param name="newLoading">The new loading.</param>
        /// <param name="fileInformation">The file information.</param>
        public Synchro(NewLoadingModel newLoading, FileInfoModel fileInformation)
        {
            InitializeComponent();
            BindingContext = new LoadSynchroViewModel(new PageService(), newLoading, fileInformation);
        }
    }
}