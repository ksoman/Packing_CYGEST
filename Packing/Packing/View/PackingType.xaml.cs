using System;
using PackingCygest.Model;
using PackingCygest.Shared;
using PackingCygest.Utils;
using PackingCygest.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Load Selection 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackingCygestType : ContentPage
    {
        private NewLoadingModel _loading;

        private BarCodeScannerViewModel BarCodeScannerViewModel;

        private PackingCygestTypeViewModel PackingCygestTypeViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackingCygestType"/> class.
        /// </summary>
        public PackingCygestType()
        {
            InitializeComponent();
            BarCodeScannerViewModel = new BarCodeScannerViewModel();
            PackingCygestTypeViewModel = new PackingCygestTypeViewModel();
            InitializeLaser();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PackingCygestType"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public PackingCygestType(NewLoadingModel loading, FileInfoModel fileInfo)
        {
            InitializeComponent();
            _loading = loading;
            AdjustStackLayouts(loading);
            BindingContext = PackingCygestTypeViewModel = new PackingCygestTypeViewModel(new PageService(), loading, fileInfo);
            InitializeLaser();
        }
        private void InitializeLaser()
        {
            if (DeviceInfo.Model.Equals("PX320"))
            {

                //TO DO : Make label to use bar code scaner
                BarcodeText.Text = PackingCygestTypeViewModel.PressButton;
                MessagingCenter.Subscribe<BarCodeReader, string>(this, "barcode", async (obj, code) =>
                {
                    MessagingCenter.Unsubscribe<BarCodeReader>(this, "barcode");
                    System.Diagnostics.Debug.WriteLine($"Bar code is: {code} ");
                //comparing the file to the scanned bar code
                if (Localize.BaseCode.Contains(code.Substring(0, code.Length - 3)))
                    {

                    // result = code.Substring(0, code.Length - 3);
                    AdjustLayout(code);
                    }
                    else
                    {
                        await DisplayAlert(PackingCygestTypeViewModel.LblError, PackingCygestTypeViewModel.BarCodeError, PackingCygestTypeViewModel.LblOk);

                    }
                });

            }
        }
        /// <summary>
        /// Adjusts the stack layouts.
        /// </summary>
        /// <param name="loading">The loading.</param>
        public void AdjustStackLayouts(NewLoadingModel loading)
        {
            if (string.IsNullOrEmpty(loading.FileItemModel.Barecode) && string.IsNullOrEmpty(loading.SelectedBarcode))
            {

                //hide digit stacklayout and increase height of scanner stackloayout,hide pbo pbm button
                BarcodeDigitStackLayout.IsVisible = false;
                BarcodeScannerStackLayout.HeightRequest = 250;
                StackButton.IsVisible = false;

            }
            else
            {
                //show digit stacklayout and decrease height of scanner stack ,show pbo pbm button
                BarcodeDigitStackLayout.IsVisible = true;
                string ThreeDigitBarcode = "";

                if (!string.IsNullOrEmpty(loading.SelectedBarcode))
                {
                    ThreeDigitBarcode = loading.SelectedBarcode.Substring(loading.SelectedBarcode.Length - 3, 3);
                }

                if (!string.IsNullOrEmpty(loading.FileItemModel.Barecode))
                {
                    ThreeDigitBarcode = loading.FileItemModel.Barecode.Substring(loading.FileItemModel.Barecode.Length - 3, 3);
                }

                BarcodeText.Text = ThreeDigitBarcode;
                BarcodeText.HorizontalTextAlignment = TextAlignment.Center;
                BarcodeScannerStackLayout.HeightRequest = 75;
                StackButton.IsVisible = true;

            }
        }

        /// <summary>
        /// Handles the Tapped event of the TapGestureRecognizer control.NOT IN USE
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string result = "";

            //The Messaging centre gets the value scanned
            if (!DeviceInfo.Model.Equals("PX320"))
            {


                if (BarCodeScannerViewModel == null)
                {
                    BarCodeScannerViewModel = new BarCodeScannerViewModel();

                }
                result = await BarCodeScannerViewModel.BarcodePage(new PageService());
                //reading -3 the barcode.
                if (!string.IsNullOrEmpty(result))
                {

                    //    result = result.Substring(0, result.Length - 3);
                    AdjustLayout(result);
                }
                else
                {

                    //TO DO: Add localize to the messages
                    //DisplayAlert(Localize.GetString());
                    await DisplayAlert(PackingCygestTypeViewModel.LblError, PackingCygestTypeViewModel.BarCodeError, PackingCygestTypeViewModel.LblOk);
                }
            }
        }

        public void AdjustLayout(string result)
        {
            if (!string.IsNullOrEmpty(result))
            {
                _loading.FileItemModel.Barecode = result;
                AdjustStackLayouts(_loading);
            }

        }
    }
}