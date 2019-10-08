using PackingCygest.Interface;
using System;
using System.Threading.Tasks;
using PackingCygest.Model;
using System.Collections.ObjectModel;
using RestSharp.Portable.HttpClient;
using RestSharp.Portable.Authenticators;
using PackingCygest.Data;
using RestSharp.Portable;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;

namespace PackingCygest.Service
{
    /// <summary>
    /// New loading 
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.INewLoadingService" />
    public class NewLoadingService : INewLoadingService
    {
        #region Get
        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<ItemModel>> GetItemAsync()
        {
            ObservableCollection<ItemModel> itemDetails = new ObservableCollection<ItemModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetItems", Method.GET);
                    var result = await client.Execute<ObservableCollection<ItemModel>>(request).ConfigureAwait(false);
                    itemDetails = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return itemDetails;
        }

        /// <summary>
        /// Gets the rooms asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<RoomsModel>> GetRoomsAsync()
        {
            ObservableCollection<RoomsModel> roomModel = new ObservableCollection<RoomsModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetRooms", Method.GET);                    
                    var result = await client.Execute<ObservableCollection<RoomsModel>>(request).ConfigureAwait(false);
                    roomModel = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return roomModel;
        }

        /// <summary>
        /// Gets the rubric item asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<ObservableCollection<RubricsModel>> GetRubricItemAsync()
        {
            ObservableCollection<RubricsModel> rubrics = new ObservableCollection<RubricsModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetRubrics", Method.GET);
                    var result = await client.Execute<ObservableCollection<RubricsModel>>(request).ConfigureAwait(false);
                    rubrics = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return rubrics;
        }

        /// <summary>
        /// Gets the rooms file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<RoomFileModel>> GetRoomsFileAsync( string file)
        {
            ObservableCollection<RoomFileModel > roomModel = new ObservableCollection<RoomFileModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetRoomsFile", Method.GET);
                    request.AddQueryParameter("file_number", file);
                    var result = await client.Execute<ObservableCollection<RoomFileModel>>(request).ConfigureAwait(false);
                    roomModel = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return roomModel;
        }


        /// <summary>
        /// Gets the item file asynchronous.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public async Task<ObservableCollection<ItemFileModel>> GetItemFileAsync(string file)
        {
            ObservableCollection<ItemFileModel> itemDetails = new ObservableCollection<ItemFileModel>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetItemsFile", Method.GET);
                    request.AddQueryParameter("file_number", file);
                    var result = await client.Execute<ObservableCollection<ItemFileModel>>(request).ConfigureAwait(false);
                    itemDetails = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return itemDetails;
        }


        /// <summary>
        /// Gets the loading end controls.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public async Task<EndControlsLoadingModel> GetLoadingEndControls(string file)
        {
           EndControlsLoadingModel endControls = new EndControlsLoadingModel();
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetLoadingEndControls", Method.GET);
                    request.AddQueryParameter("file_number", file);
                    var result = await client.Execute<EndControlsLoadingModel>(request).ConfigureAwait(false);
                    endControls = result.Data;
                }
            }

            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });
            }

            return endControls;
        }


        #endregion

        #region Post


        //public async Task<bool> PostPackingCygestNewLoaddingItems(string idFIleItem,string idFileRoom,string idItem,string idRoom,string file_Number,string barecode,string name,string cub_valued,string cub_dismount, string cub_reassembly,string cub_crate,string comments_loading,string comments_delivery,string sent,string packed_by,string Dismounting_type,string mechanical_statement,string imageSVG,string telephone,string branche)
        /// <summary>
        /// Posts the PackingCygest new loadding items.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> PostPackingCygestNewLoaddingItems(List<FileItemModel> item)
        {
            bool retval = false;
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostItemsLoading", Method.POST);
                    request.AddJsonBody(item);


                    var result = await client.Execute<bool>(request);

                    if (result != null)
                    {
                        retval = result.IsSuccess;
                    }

                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                { Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source });


            }
            
           
            return retval;


        }


        /// <summary>
        /// PackingCygests the post items photos.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> PackingCygestPostItemsPhotos(List<ItemPhotoModel> item)
        {
          
            bool retval = false;
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostItemsPhotos", Method.POST);
                    request.AddJsonBody(item);            

                    var result = await client.Execute<bool>(request);

                    if (result != null)
                    {
                        retval = result.IsSuccess;
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
        /// PackingCygests the post signature.
        /// </summary>
        /// <param name="signature">The signature.</param>
        /// <returns></returns>
        public async Task<bool> PackingCygestPostSignature(SignatureModel signature)
        {

            bool retval = false;
            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingPostSignature", Method.POST);
                    request.AddJsonBody(signature);

                    var result = await client.Execute<bool>(request);

                    if (result != null)
                    {
                        retval = result.IsSuccess;
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

        //Get Missing Barcode from API
        public async Task<ObservableCollection<string>> GetMissingBarocde(string filenum)
        {
            ObservableCollection<string> lstMissingBarcode = new ObservableCollection<string>();

            try
            {
                using (var client = new RestClient(new Uri("https://api-tray.intragest.info/api/")))
                {
                    client.Authenticator = new HttpBasicAuthenticator("astek.tracker", "3B]U/2Z8w7fDce=(");
                    var request = new RestRequest("PackingGetItemsBCMissing", Method.GET);
                    request.AddQueryParameter("file_number", filenum);
                    var result = await client.Execute<ObservableCollection<string>>(request).ConfigureAwait(false); ;
                    lstMissingBarcode = result.Data;
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

            return lstMissingBarcode;
        }











        #endregion
    }
}
