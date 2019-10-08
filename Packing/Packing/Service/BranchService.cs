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
    /// This class involves getting the branch details 
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.IBranchService" />
    public class BranchService : IBranchService
    {
        /// <summary>
        /// Gets the branch details asynchronous.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="language">The language.</param>
        /// <param name="brandId">The brand identifier.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<BranchModel>> GetBranchDetailsAsync(int countryId, string language, int brandId)
        {
            ObservableCollection<BranchModel> branchDetails  = new   ObservableCollection<BranchModel>();
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("Branches", Method.GET);
                    request.AddParameter("lang", language);
                    request.AddParameter("country_id", countryId);
                   // request.AddParameter("brand_id", brandId);
         

                    var result = await client.Execute<ObservableCollection<BranchModel>>(request).ConfigureAwait(false); 

                    branchDetails = result.Data;
                }

            }
            catch (Exception e)
            {
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel 
                    { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });

            }
            return branchDetails;
        }

       
    }
}
