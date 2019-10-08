
using PackingCygest.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    /// <summary>
    /// Branch Service Interface
    /// </summary>
    public interface IBranchService
    {
        /// <summary>
        /// Gets the branch details asynchronous.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="brandId">The brand identifier.</param>
        /// <returns></returns>
        Task<ObservableCollection<BranchModel>> GetBranchDetailsAsync(int countryId, string language, int brandId);
    }
}
