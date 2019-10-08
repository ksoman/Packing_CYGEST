using PackingCygest.Data;
using PackingCygest.Droid.Implementation;
using PackingCygest.Interface;
using PackingCygest.Model;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using System.Globalization;
using Android.Content;
using Xamarin.Essentials;

[assembly: Dependency(typeof(BarCodeScanner))]
namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// Barcode Scanner class used as  a dependency Injection
    /// This class is used by Dependency injection 
    /// this class is used to scanner file barcode to extract details from sWeb Service
    /// The Catch expection will log the error in SQLlite DB
    /// The dump functionality will allow the error to  send by mail 
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.IBarcodeScanner" />
    public class BarCodeScanner :  IBarcodeScanner
    {

        private string code = "";
        /// <summary>
        /// Gets the scanned bar code.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetScannedBarCode()
        {
          
            try
            {
             
                MobileBarcodeScanner scanner = new MobileBarcodeScanner();
                scanner.BottomText = "Focus on Barcode";
                var result = await scanner.Scan();

                if (result != null)
                {
                    code = result.Text;
                }
            }
            catch (Exception ex)
            {
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = ex.Message, StackTrace = ex.StackTrace, TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture), MethodName = ex.Source
                });

            }


            return code;
        }

     
    }
}