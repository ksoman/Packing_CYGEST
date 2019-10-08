using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.Utils;
using PackingCygest.View;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    public class MissingBarcodeViewModel : BaseViewModel
    {
        private readonly IPageService _pageService;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private ObservableCollection<string> _missingBarcodeItem;
        public string LblMissingBarcode { get; set; }
        public string LblNoNetwork { get; set; }
        public string LblPleaseEnableYourNetwork { get; set; }
        public ICommand Back { get; set; }
        private NewLoadingModel _barCodeLoading;
        private FileInfoModel _fileInfo;
        private  NewLoadingService _newLoadingService = new NewLoadingService();
        private bool _isDownloading;

        public bool IsDownloading
        {
            get { return _isDownloading; }
            set { SetValue(ref _isDownloading, value); }
        }



        public ObservableCollection<string> MissingBarcodeItem
        {

            get { return _missingBarcodeItem; }
            set { SetValue(ref _missingBarcodeItem, value); }
        }


        public MissingBarcodeViewModel(IPageService pageService, NewLoadingModel barCodeLoading, FileInfoModel fileInfo)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _barCodeLoading = barCodeLoading;
            _fileInfo = fileInfo;
            _pageService = pageService;
            PopulatedMissingBarcodeList();
            Back = new Command(GoBack);

        }

        private async void GoBack()
        {
            await _pageService.PushAsync(new LoadMenu(_barCodeLoading,_fileInfo));

        }

        private void PopulatedMissingBarcodeList()
        {
           
            MissingBarcodeItem = new ObservableCollection<string>();
            GetFileItemsDelivery(_fileInfo.File_number);

        }


        private async Task GetFileItemsDelivery(string fileNumber)
        {
            IsDownloading = true;
            ObservableCollection<string> lstMissingBarcode = new ObservableCollection<string>();
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    lstMissingBarcode = await _newLoadingService.GetMissingBarocde(fileNumber);

                    if (lstMissingBarcode != null)
                    {

                        MissingBarcodeItem = lstMissingBarcode;
                    }
                }
                else
                {
                    _pageService.DisplayAlert(LblNoNetwork, LblPleaseEnableYourNetwork, "ok");
                }
                IsDownloading = false;
            }
            catch (Exception e)
            {
                IsDownloading = false;
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });

            }

        }

        public void HandleTranslation(string cultureInfo)
        {
            LblMissingBarcode = Localize.GetString("LblMissingBarcode", cultureInfo);
            LblNoNetwork = Localize.GetString("LblNoNetwork", cultureInfo);
            LblPleaseEnableYourNetwork = Localize.GetString("LblPleaseEnableYourNetwork", cultureInfo);

        }

    }
}
