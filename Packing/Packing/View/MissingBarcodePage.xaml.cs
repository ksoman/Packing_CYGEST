using PackingCygest.Model;
using PackingCygest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissingBarcodePage : ContentPage
    {
        private NewLoadingModel _loading;
        private FileInfoModel _fileInfo;
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingBarcodePage"/> class.
        /// </summary>
        /// <param name="barCodeLoading">The bar code loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public MissingBarcodePage(NewLoadingModel barCodeLoading, FileInfoModel fileInfo)
        {

            InitializeComponent();
            _loading = barCodeLoading;
            _fileInfo = fileInfo;
            BindingContext = new MissingBarcodeViewModel(new PageService(),_loading,_fileInfo);

        }

        /// <summary>
        /// Handles the ItemSelected event of the ListView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            listView.SelectedItem = null;
        }
    }
}