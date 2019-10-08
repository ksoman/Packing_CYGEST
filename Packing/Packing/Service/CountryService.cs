using PackingCygest.Interface;
using System;
using System.Threading.Tasks;
using PackingCygest.Model;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable;
using PackingCygest.Data;
using System.Collections.ObjectModel;

namespace PackingCygest.Service
{
    /// <summary>
    /// Country Service to pull infrom from Webservice
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.ICountryService" />
    public class CountryService : ICountryService
    {
        /// <summary>
        /// Gets all countries asynchronous.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<CountryModel>> GetAllCountriesAsync(string language)
        {
            ObservableCollection<CountryModel> allCountries = new ObservableCollection<CountryModel>();

            try
            {

                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("CountryTelCode", Method.GET);
                    request.AddQueryParameter("lang", language);

                    var result = await client.Execute<ObservableCollection<CountryModel>>(request).ConfigureAwait(false);

                    allCountries = result.Data;
                }
            }

            catch (Exception e)
            {
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return allCountries;
        }
    }
}
