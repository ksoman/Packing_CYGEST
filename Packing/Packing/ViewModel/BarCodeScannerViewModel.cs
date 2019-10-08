using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

using Xamarin.Essentials;
using PackingCygest.Utils;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// Bar code scanner class
    /// </summary>
    public class BarCodeScannerViewModel
    {

        public static IPageService PageService { get; set; }
        string result = "";
        /// <summary>
        /// Barcodes the page.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <returns></returns>
        public async Task<string> BarcodePage(IPageService pageService)
        {
            PageService = pageService;

            var barCode = DependencyService.Get<IBarcodeScanner>();
      
            Func<string, string> temp = (obj) => { return result; };
            try
            {

           
                result = await barCode.GetScannedBarCode();

                //moce result from here
                //result = fullBarCode.Substring(0, fullBarCode.Length - 3);
                var fileCode = result.Substring(0, result.Length - 3);
                if (Localize.BaseCode.Contains(fileCode)) 
                {
                    return result;
                }
                else
                {
                    return "";
                }
                
              
                //if (!string.IsNullOrEmpty(result))
                //{
                //    System.Diagnostics.Debug.WriteLine("Scanned Barcode: " + result);
                //}
            }
            catch (Exception ex)
            {

                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture),
                    MethodName = ex.Source
                });
               
            }
            return result;
        }
    }
}
