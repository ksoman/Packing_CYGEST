using PackingCygest.Data;
using PackingCygest.Model;
using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Service
{
   public class DeliveryService
    {
        /// <summary>
        /// Gets the file information for delivery asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<FileInfoDeliveryModel> GetFileInfosDelivery(string FileNumber, string MobileNumber)
        {
            FileInfoDeliveryModel fileInfoDel = new FileInfoDeliveryModel();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetFileInfosDelivery", Method.GET);
                    request.AddQueryParameter("file_number", FileNumber);
                    request.AddQueryParameter("mobile_number", MobileNumber);
                    var result = await client.Execute<FileInfoDeliveryModel>(request).ConfigureAwait(false);
                 
                    fileInfoDel = result.Data;
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }

            return fileInfoDel;
        }

        /// <summary>
        /// Gets the file items delivery information asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<ItemDeliveredModel>> GetFileItemsDelivery(string FileNumber,string language)
        {
            ObservableCollection<ItemDeliveredModel> fileItemDel = new ObservableCollection<ItemDeliveredModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetItemsDelivery", Method.GET);
                    request.AddQueryParameter("file_number", FileNumber);
                    request.AddQueryParameter("lang", language);
                    var result = await client.Execute<ObservableCollection<ItemDeliveredModel>>(request).ConfigureAwait(false);

                    fileItemDel = result.Data;
                }
            }
            catch(Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }

            return fileItemDel;
        }

        /// <summary>
        /// Post the items for delivery asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PostItemsDelivery(List<ItemDeliveredModel> ItemDelivered)
        {
            bool retval = false;

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostItemsDelivery", Method.POST);
                    request.AddJsonBody(ItemDelivered); 
                    var response = await client.Execute<bool>(request);

                    if (response != null)
                    {
                        retval = response.IsSuccess;
                    }
                   
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }
            return retval;
        }

        /// <summary>
        /// Post the photo items for delivery asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PostItemsPhotos(List<ItemPhotoModel> ItemPhoto)
        {
            bool retval = false;

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostItemsPhotos", Method.POST);
                    request.AddJsonBody(ItemPhoto);
                    var response = await client.Execute<bool>(request);

                    if (response != null)
                    {
                        retval = response.IsSuccess;
                    }
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }

            return retval;
        }

        /// <summary>
        /// Post the EndControls Delivery asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<DeliveryEndControls>GetDeliveryEndControl(string FileNumber)
        {
            DeliveryEndControls endControl = new DeliveryEndControls();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetDeliveryEndControls", Method.GET);
                    request.AddQueryParameter("file_number", FileNumber);
                    var result = await client.Execute<DeliveryEndControls>(request).ConfigureAwait(false);

                    endControl = result.Data;
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }
            return endControl;
        }

        /// <summary>
        /// Post the signature for delivery asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> PostSignature(SignatureModel Sign)
        {
            bool retval = false;

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostSignature", Method.POST);
                    request.AddJsonBody(Sign);
                    var response = await client.Execute<bool>(request);

                    if (response != null)
                    {
                        retval = response.IsSuccess;
                    }
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }

            return retval;
        }

        //Get Loading Picture from API
        public async Task<ObservableCollection<DownloadedPhotoModel>> GetItemsPhotos(string filenum)
        {
            ObservableCollection<DownloadedPhotoModel> photoItemDel = new ObservableCollection<DownloadedPhotoModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetItemsPhotos", Method.GET);
                    request.AddQueryParameter("file_number", filenum);
                    client.Timeout =TimeSpan.FromMinutes(10);
                    var result = await client.Execute<ObservableCollection<DownloadedPhotoModel>>(request).ConfigureAwait(false); ;
                    photoItemDel = result.Data;
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }

            return photoItemDel;
        }

                
    }
}
