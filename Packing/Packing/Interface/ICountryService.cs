using PackingCygest.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    /// <summary>
    /// Icountry interface 
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Gets all countries asynchronous.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        Task<ObservableCollection<CountryModel>> GetAllCountriesAsync(string language);
    }
}
