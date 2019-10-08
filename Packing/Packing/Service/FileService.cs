using PackingCygest.Data;
using PackingCygest.Model;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;
using System;
using System.Threading.Tasks;

namespace PackingCygest.Service
{
    /// <summary>
    /// This Class Gets the file info
    /// </summary>
    public class FileService
    {
        /// <summary>
        /// Gets the file information asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<FileInfoModel> GetFileInfoAsync(string fileBarcode, string phoneNumber )
        {
           FileInfoModel fileInfo = new FileInfoModel();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetFileInfosLoading", Method.GET);
                    request.AddQueryParameter("file_number", fileBarcode);
                    request.AddQueryParameter("mobile_number", phoneNumber);
                    var result = await client.Execute<FileInfoModel>(request).ConfigureAwait(false);
                    fileInfo = result.Data;
                  
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return fileInfo;
        }

    }
}
