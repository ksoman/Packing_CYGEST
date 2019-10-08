using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    /// <summary>
    /// Interface for Barcode Scanner
    /// </summary>
    public interface IBarcodeScanner
    {
        /// <summary>
        /// Gets the scanned bar code.
        /// </summary>
        /// <returns></returns>
        Task<string> GetScannedBarCode();
    }
}
