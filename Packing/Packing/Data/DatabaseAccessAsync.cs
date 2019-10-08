

using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PackingCygest.Data
{
    /// <summary>
    /// This class reference to Database access
    /// </summary>
    public class DatabaseAccessAsync
    {
        
        private static SQLiteAsyncConnection _con = DatabaseConnection.DbConnectionAsync();

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseAccessAsync"/> class.
        /// </summary>
        public DatabaseAccessAsync()
        {            
            _con.CreateTableAsync<PackingCygestExceptionModel>();       
            _con.CreateTableAsync<FileItemModel>();          
            _con.CreateTableAsync<PhotoShotModel>();
            _con.CreateTableAsync<ItemDeliveredModel>();    
            _con.CreateTableAsync<EndControlsLoadingModel>();
            _con.CreateTableAsync<ItemPhotoModel>();
            _con.CreateTableAsync<FileInfoDeliveryModel>();
        }
            
        /// <summary>
        /// Inserts the item details.
        /// </summary>
        /// <param name="items">The items.</param>
        public async Task InsertItemDetails(List<ItemModel> items)
        {
            await _con.CreateTableAsync<ItemModel>();
            await _con.InsertAllAsync(items);
        }
             
        /// <summary>
        /// Inserts the end controls.
        /// </summary>
        /// <param name="endControls">The end controls.</param>
        public async Task InsertEndControlsLoading(EndControlsLoadingModel endControls)
        {
            await _con.CreateTableAsync<EndControlsLoadingModel>();
            await  _con.InsertAsync(endControls);
        }

        
        /// <summary>
        /// Inserts the exception.
        /// </summary>
        /// <param name="PackingCygestException">The PackingCygest exception.</param>
        public void InsertException(PackingCygestExceptionModel PackingCygestException)
        {
            _con.CreateTableAsync<PackingCygestExceptionModel>();
            _con.InsertAsync(PackingCygestException);
        }
    
        /// <summary>
        /// Inserts the loading photo.
        /// </summary>
        /// <param name="itemPhoto">The item photo.</param>
        public async Task InsertLoadingPhoto(ItemPhotoModel itemPhoto)
        {
            await _con.CreateTableAsync<ItemPhotoModel>();
            await _con.InsertAsync(itemPhoto);
        }
   

        /// <summary>
        /// Gets the photo quality asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetPhotoQualityAsync()
        {
            try
            {
                string val = await _con.ExecuteScalarAsync<string>("select PhotoQuality from ConfigurationModel");
                return val;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

     
        /// <summary>
        /// Check for existing IdItems for loading
        /// </summary>
        /// <param name="IdItem"></param>
        /// <returns> Count of Existing IdItems</returns>
        public async Task<int> GetCountOfPhotoPathLoading (string IdItem)
        {
            try
            {
                int count = await _con.ExecuteScalarAsync<int>("SELECT COUNT(Barecode) from FileItemModel Where Barecode =?", IdItem);
                return count;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        /// <summary>
        /// Sents the data to server.
        /// </summary>
        public void SentDataToServer()
        {
            try
            {
                _con.ExecuteAsync("UPDATE ItemDeliveredModel SET Sent = 1 WHERE Sent = 0");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

     
    }
}
