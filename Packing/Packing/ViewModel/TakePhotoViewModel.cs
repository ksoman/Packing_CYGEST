using PackingCygest.Interface;
using System.Reflection;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// Take pHoto View Mocel
    /// </summary>
    /// <seealso cref="PackingCygest.ViewModel.BaseViewModel" />
    public class TakePhotoViewModel : BaseViewModel
    {
        public IPageService PageService { get; set; }
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string SvgPathAdd => "PackingCygest.Image.Add.svg";

        /// <summary>
        /// Initializes a new instance of the <see cref="TakePhotoViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        public TakePhotoViewModel(IPageService pageService)
        {
            PageService = pageService;

        }


    }
}
