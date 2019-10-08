using PackingCygest.Model;
using PackingCygest.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// The class of Del Dismounting 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingOption : ContentPage
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingOption"/> class.
        /// </summary>
        public LoadingOption ()
		{
			InitializeComponent ();
         
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingOption"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadingOption(NewLoadingModel loading, FileInfoModel fileInfo)
        {
            InitializeComponent();
            BindingContext = new LoadingOptionViewModel(new PageService(), loading, fileInfo);

        }
    }
}