using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace PackingCygest.Data
{
    public class DatabaseAccess
    {
        //private readonly SQLiteConnection _con ;

  

        public DatabaseAccess()
        {
            //_con = DependencyService.Get<ISqLiteDb>().GetConnection();
            // _con.CreateTable<ConfigurationModel>();
            using ( var _connect = DependencyService.Get<ISqLiteDb>().GetConnection())
            {
                _connect.CreateTable<ItemPhotoModel>();
                _connect.CreateTable<RubricsModel>();
                _connect.CreateTable<RoomFileModel>();
                _connect.CreateTable<RoomsModel>();
                _connect.CreateTable<ItemModel>();
                _connect.CreateTable<ItemFileModel>();
                _connect.CreateTable<EndControlsLoadingModel>();
                _connect.CreateTable<DeliveryPhotoModel>();
            } 
        }

        /// <summary>
        /// Opens the connect.
        /// </summary>
        /// <returns></returns>
        private SQLiteConnection OpenConnect()
        {
            SQLiteConnection _con = DatabaseConnection.DbConnection();

            return _con;
        }

        /// <summary>
        /// Closes the conenct.
        /// </summary>
        /// <param name="_con">The con.</param>
        private void CloseConenct(SQLiteConnection _con)
        {
            _con. Close();
        }

        /// <summary>
        /// Inserts the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        public void InsertPhoto(PhotoShotModel photo)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {               
                _con.CreateTable<PhotoShotModel>();
                System.Diagnostics.Debug.WriteLine("Saving the Photo");
                _con.Insert(photo);               
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
            CloseConenct(_con);

        }

        /// <summary>
        /// Gets the comments.
        /// </summary>
        public string GetComments(string itemBarcode)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<FileItemModel>(); 

            var comments = _con.ExecuteScalar<string>("Select Comments_loading from FileItemModel where barecode =?", itemBarcode);
            CloseConenct(_con);
            return comments;             
        }
        /// <summary>
        /// Inserts the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void InsertConfig(ConfigurationModel config)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();
            _con.Insert(config);
            CloseConenct(_con);
        }
        /// <summary>
        /// Gets all configuration data.
        /// </summary>
        /// <returns></returns>
        public List<ConfigurationModel> GetAllConfigData()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();

            var conf = _con.Query<ConfigurationModel>("Select * from ConfigurationModel");
            CloseConenct(_con);
            return conf;
        }

        /// <summary>
        /// Gets the loading photo.
        /// </summary>
        /// <returns></returns>
        public List<ItemPhotoModel> GetLoadingPhoto()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ItemPhotoModel>();
            var items = _con.Query<ItemPhotoModel>("Select * from ItemPhotoModel");
            CloseConenct(_con);

            return items;

        }
     
        /// <summary>
        /// Determines whether [has configuration data].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has configuration data]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasConfigData()
        {
            SQLiteConnection _con = OpenConnect();

            _con.CreateTable<ConfigurationModel>();
            bool hasConfiData = true;

            int count = _con.ExecuteScalar<int>("SELECT Count(*) FROM ConfigurationModel");

            if (count == 0)
            {
                hasConfiData = false;

            }
            CloseConenct(_con);
            return hasConfiData;

        }
        /// <summary>
        /// Gets the end control.
        /// </summary>
        /// <param name="fileBarcode">The file barcode.</param>
        /// <returns></returns>
        public List<EndControlsLoadingModel> GetEndControlLoading(string fileBarcode)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<EndControlsLoadingModel>();
            var control = _con.Query<EndControlsLoadingModel>("Select * from EndControlsLoadingModel where File_Number = ?", fileBarcode);
            CloseConenct(_con);
            return control;

        }
        /// <summary>
        /// Gets the branchCode.
        /// </summary>
        /// <returns></returns>
        public string GetBranchCode()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();
            string branchCode = "";
            int count = _con.ExecuteScalar<int>("SELECT Count(*) FROM ConfigurationModel");

            if (count > 0)
            {
                branchCode = _con.ExecuteScalar<string>("select BranchCode from ConfigurationModel");
            }
            CloseConenct(_con);
            return branchCode;

        }
        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <returns></returns>
        public string GetPhoneNumber()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();
            string phoneNumber = "";
            List<ConfigurationModel> PhoneNum = new List<ConfigurationModel>();

            int count = _con.ExecuteScalar<int>("SELECT Count(*) FROM ConfigurationModel");

            if (count > 0)
            {
                PhoneNum = _con.Query<ConfigurationModel>("select TelCode, MobileNumber from ConfigurationModel");

                foreach(var i in PhoneNum)
                {
                    phoneNumber = i.TelCode + i.MobileNumber;
                }
            }
            CloseConenct(_con);
            return phoneNumber;
        }
        /// <summary>
        /// Gets the item barcode.
        /// </summary>
        /// <param name="fileNumber">The file number.</param>
        /// <returns></returns>
        public List<FileItemModel> GetItemBarcode(string fileNumber)
        {
            SQLiteConnection _con = OpenConnect();
            var Items = _con.Query<FileItemModel>("select Barecode from FileItemModel where File_Number=?", fileNumber);
            CloseConenct(_con);
            return Items;
        }


        /// <summary>
        /// Drops the table room file details.
        /// </summary>
        public void DropTableRoomFileDetails()
        {
            SQLiteConnection _con = OpenConnect();
            _con.DeleteAll<RoomFileModel>();
            CloseConenct(_con);
        }

        /// <summary>
        /// Drops the table item file details.
        /// </summary>
        public void DropTableItemFileDetails()
        {
            SQLiteConnection _con = OpenConnect();
            _con.DeleteAll<ItemFileModel>();
            CloseConenct(_con);

        }
        /// <summary>
        /// Drops the table room.
        /// </summary>
        public void DropTableRoom()
        {
            SQLiteConnection _con = OpenConnect();
            _con.DeleteAll<RoomsModel>();
            CloseConenct(_con);
        }
        /// <summary>
        /// Drops the table rubrics.
        /// </summary>
        public void DropTableRubrics()
        {
            SQLiteConnection _con = OpenConnect();
            _con.DeleteAll<RubricsModel>();
            CloseConenct(_con);
        }
        /// <summary>
        /// Drops the table items.
        /// </summary>
        public void DropTableItems()
        {
            SQLiteConnection _con = OpenConnect();
            _con.DeleteAll<ItemModel>();
            CloseConenct(_con);

        }
        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <param name="scannedText">The scanned text.</param>
        /// <returns></returns>
        public List<ItemDeliveredModel> GetItemDetails(string scannedText)
        {
            SQLiteConnection _con = OpenConnect();
            List<ItemDeliveredModel> scanDEtails;

            scanDEtails = _con.Query<ItemDeliveredModel>("Select * from ItemDeliveredModel where Barecode=?", scannedText);
            CloseConenct(_con);
            return scanDEtails;
        }
        /// <summary>
        /// Gets the selected item details.
        /// </summary>
        /// <param name="scannedText">The scanned text.</param>
        /// <returns></returns>
        public FileItemModel GetSelectedItemDetails(string scannedText)
        {
            SQLiteConnection _con = OpenConnect();
            var selectedItem = _con.Table<FileItemModel>().SingleOrDefault(x => x.Barecode == scannedText);
            CloseConenct(_con);
            return selectedItem;
        }
        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        public void DeleteItem(string barcode)
        {
            SQLiteConnection _con = OpenConnect();
            _con.Execute("DELETE FROM  FileItemModel where Barecode =?", barcode);
            CloseConenct(_con);
        }

        /// <summary>
        /// Inserts the file information.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        public void InsertFileInfo(FileInfoModel fileInfo)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<FileInfoModel>();
            _con.InsertOrReplace(fileInfo);
            CloseConenct(_con);

        }
        /// <summary>
        /// Gets the count loading item.
        /// </summary>
        /// <returns></returns>
        public List<FileItemModel> GetCountLoadingItem(string fileNumber)
        {
            SQLiteConnection _con = OpenConnect();
            var countItem = _con.Query<FileItemModel>("Select distinct  Idrubric, RubricsName from FileItemModel where File_Number =?", fileNumber);
            CloseConenct(_con);
            return countItem;

        }
        public string GetSVGPathRoomFile(string IdFileRoom)
        {
            SQLiteConnection _con = OpenConnect();
            var svgPath = _con.ExecuteScalar<string>("Select SvgImagePath  from RoomFileModel ");
            CloseConenct(_con);
            return svgPath;
        }
        /// <summary>
        /// Gets the SVG path rubrics.
        /// </summary>
        /// <param name="IdRubric">The identifier rubric.</param>
        /// <returns></returns>
        public List<RubricsModel> GetSVGPathRubrics(string IdRubric)
        {
            SQLiteConnection _con = OpenConnect();
            var svgPath = _con.Query<RubricsModel>("Select *  from RubricsModel where IdRubric = ? ", IdRubric);
            CloseConenct(_con);
            return svgPath;
        }
        /// <summary>
        /// Gets the count loading file item.
        /// </summary>
        /// <returns></returns>
        public List<FileItemModel> GetCountLoadingFileItem()
        {
            SQLiteConnection _con = OpenConnect();
            var countLoading = _con.Query<FileItemModel>("Select distinct  IdFileRoom, RoomName from FileItemModel where Sent_Loading = 1");
            CloseConenct(_con);
            return countLoading;
        }
        /// <summary>
        /// Totals the item per rubric.
        /// </summary>
        /// <param name="rubricId">The rubric identifier.</param>
        /// <returns></returns>
        public int TotalItemPerRubric(string rubricId, string fileNumber)
        {
            SQLiteConnection _con = OpenConnect();
            int count = _con.ExecuteScalar<int>("SELECT Count(*) FROM FileItemModel where Idrubric = ? and File_Number=?", rubricId, fileNumber);
            CloseConenct(_con);
            return count;
        }

        /// <summary>
        /// Totals the item per room.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns></returns>
        public int TotalItemPerRoom(string roomId)
        {
            SQLiteConnection _con = OpenConnect();
            int count = _con.ExecuteScalar<int>("SELECT Count(*) FROM FileItemModel where IdFileRoom = ?", roomId);
            CloseConenct(_con);
            return count;
        }

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        public FileItemModel GetItem(string barcode)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                FileItemModel item = new FileItemModel();
                List<FileItemModel> allItems;
                allItems = _con.Query<FileItemModel>("Select * from FileItemModel where Barecode=?", barcode);
                for (int i = 0; i < allItems.Count; i++)
                {
                    item = new FileItemModel
                    {
                        Barecode = allItems[i].Barecode,
                        File_Number =allItems[i].File_Number,
                        Dismounting_type = allItems[i].Dismounting_type,
                        Reassembling_type = allItems[i].Reassembling_type,
                        Mechanical_statement = allItems[i].Mechanical_statement,
                        Comments_loading = allItems[i].Comments_loading
                    };
                }
                CloseConenct(_con);
                return item;

            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;
            }
            


        }
        /// <summary>
        /// Updates the rubric data.
        /// </summary>
        /// <param name="rub">The rub.</param>
        public void UpdateRubricData(RubricsModel rub)
        {
            SQLiteConnection _con = OpenConnect();
            _con.Update(rub);
            CloseConenct(_con);
        }
        /// <summary>
        /// Updates all room data.
        /// </summary>
        /// <param name="room">The room.</param>
        public void UpdateAllRoomData(List<RoomsModel> room)
        {
            SQLiteConnection _con = OpenConnect();
            _con.UpdateAll(room);
            CloseConenct(_con);
        }
        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="fileItem">The file item.</param>
        public void InsertItem(List<ItemModel> items)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ItemModel>();            
            _con.InsertAll(items);
            CloseConenct(_con);
        }
        /// <summary>
        /// Inserts the rubric data.
        /// to recreate table to ensure there is no duplication of data       
        /// </summary>
        /// <param name="rubrics">The rubrics.</param>
        /// //TODO: remove existing data prior to insert
        public void InsertRubricData(List<RubricsModel> rubrics)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<RubricsModel>();
            _con.InsertAll(rubrics);
            CloseConenct(_con);
        }

        /// <summary>
        /// Updates the configuration data.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public void UpdateConfigurationData(ConfigurationModel config)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();
            _con.Update(config);
            CloseConenct(_con);
        }

        /// <summary>
        /// Inserts the room model.
        /// </summary>
        /// <param name="rooms">The rooms.</param>
        public void InsertRoomModel(List<RoomsModel> rooms)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<RoomsModel>();
            _con.InsertAll(rooms);
            CloseConenct(_con);
        }

        /// <summary>
        /// Gets the items details.
        /// </summary>
        /// <returns></returns>
        public List<ItemModel> GetItemsDetails()
        {
            SQLiteConnection _con = OpenConnect();
            var itemDetails = _con.Query<ItemModel>("Select * from ItemModel ");
            CloseConenct(_con);
            return itemDetails;
        }
        /// <summary>
        /// Gets all rooms.
        /// </summary>
        /// <returns></returns>
        public List<RoomsModel> GetAllRooms()
        {
            SQLiteConnection _con = OpenConnect();
            var room = _con.Query<RoomsModel>("Select * from RoomsModel");
            CloseConenct(_con);
            return room;
        }
        /// <summary>
        /// Gets all rubrics.
        /// </summary>
        /// <returns></returns>
        public List<RubricsModel> GetAllRubrics()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<RubricsModel>();
            List<RubricsModel> rubricList;
            try
            {
                rubricList = _con.Query<RubricsModel>("Select * from RubricsModel");
                CloseConenct(_con);
            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;
            }
            return rubricList;
        }

        /// <summary>
        /// Gets all items by identifier rub.
        /// </summary>
        /// <param name="IdRubric">The identifier rubric.</param>
        /// <returns></returns>
        public List<ItemModel> GetAllItemsByIdRub(string IdRubric)
        {
            SQLiteConnection _con = OpenConnect();
            var item = _con.Query<ItemModel>("Select * from ItemModel where IdRubric = ?", IdRubric);
            CloseConenct(_con);
            return item;
        }
        /// <summary>
        /// Gets all items by identifier rooms.
        /// </summary>
        /// <param name="IdRubric">The identifier rubric.</param>
        /// <returns></returns>
        public List<ItemModel> GetAllItemsByIdRooms(string IdRubric)
        {
            SQLiteConnection _con = OpenConnect();
            var item = _con.Query<ItemModel>("Select * from ItemModel where IdRubric = ?", IdRubric);
            CloseConenct(_con);
            return item;
        }

        /// <summary>
        /// Gets the photo path.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        public List<DeliveryPhotoModel> GetPhotoPathForDelivery(string barcode)
        {
            SQLiteConnection _con = OpenConnect();
            var path = _con.Query<DeliveryPhotoModel>("select PhotoPath from DeliveryPhotoModel WHERE Barecode = ?", barcode);
            CloseConenct(_con);
            return path;

        }

        /// <summary>
        /// Gets the photo path.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        public string GetPhotoPath(string barcode)
        {
            SQLiteConnection _con = OpenConnect();
            var path = _con.ExecuteScalar<string>("select PhotoPath from ItemDeliveredModel WHERE Barecode = ?", barcode);
            CloseConenct(_con);
            return path;

        }
        /// <summary>
        /// Gets the photo path loading.
        /// </summary>
        /// <param name="IdItem">The identifier item.</param>
        /// <returns></returns>
        public string GetPhotoPathLoading(string IdItem)
        {
            // var allItems = _con.Query<FileItemModel>("Select * from FileItemModel");
            SQLiteConnection _con = OpenConnect();
            var path = _con.ExecuteScalar<string>("Select PhotoPath from FileItemModel where Barecode = ? AND Sent_Loading = 0", IdItem);
            CloseConenct(_con);
            return path;
        }

        /// <summary>
        /// Inserts the file information delete.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        public void InsertFileInfoDel(FileInfoDeliveryModel fileInfo)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<FileInfoDeliveryModel>(); 
             _con.InsertOrReplace(fileInfo);
            CloseConenct(_con);
        }


        /// <summary>
        /// Insert new loading details
        /// </summary>
        /// <param name="fileItem"></param>
        public void InsertLoading(FileItemModel fileItem)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {

                _con.Insert(fileItem);

                CloseConenct(_con);
            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                //throw ex;
            }

        }
        /// <summary>
        /// Update existing records
        /// </summary>
        /// <param name="fileItem"></param>

        public void UpdateLoadingData(FileItemModel fileItem)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                _con.Update(fileItem);
                CloseConenct(_con);
            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;

            }
        }
        /// <summary>
        /// Languages the selected.
        /// </summary>
        /// <returns></returns>
        public string LanguageSelected()
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<ConfigurationModel>();
            string language = _con.ExecuteScalar<string>("Select CultureInfo from ConfigurationModel");
            CloseConenct(_con);
            return language;

        }
        /// <summary>
        /// Gets the loading photo list detail.
        /// </summary>
        /// <returns></returns>
        public List<FileItemModel> GetLoadingPhotoListDetail()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                var photoItem = _con.Query<FileItemModel>("Select Barecode,PhotoPath,Comments_loading from FileItemModel WHERE Sent_Loading = 0;");
                CloseConenct(_con);
                return photoItem;

            }
            catch (Exception e)
            {
                CloseConenct(_con);
                throw e;
            }
        }
        /// <summary>
        /// Updates the loading to true.
        /// </summary>
        public void UpdateLoadingToTrue()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
               
                _con.Execute("UPDATE FileItemModel SET Sent_Loading = 1 WHERE Sent_Loading = 0");
                CloseConenct(_con);

            }
            catch (Exception e)
            {
                CloseConenct(_con);
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Gets the file item details.
        /// </summary>
        /// <returns></returns>
        public List<FileItemModel> GetAllDetails()
        {
            SQLiteConnection _con = OpenConnect();
            List<FileItemModel> fileModel = new List<FileItemModel>();
            try
            {
                fileModel = _con.Query<FileItemModel>("Select * from FileItemModel WHERE Sent_Loading = 0");
                CloseConenct(_con);
            }
            catch (Exception e)
            {
                CloseConenct(_con);
                throw e;
            }
            return fileModel;
        }

        /// <summary>
        /// Gets the file details.
        /// </summary>
        /// <param name="fileNumber">The file number.</param>
        /// <returns></returns>
        public List<FileItemModel> GetFileDetails(string fileNumber)
        {
            SQLiteConnection _con = OpenConnect();
            var fileDetails = _con.Query<FileItemModel>("Select * from FileItemModel where File_Number = ? and Sent_Loading = 0", fileNumber);
            CloseConenct(_con);
            return fileDetails;
        }
        /// <summary>
        /// Gets the file details list.
        /// </summary>
        /// <param name="Cubage">if set to <c>true</c> [cubage].</param>
        /// <param name="fileNumber">The file number.</param>
        /// <param name="IdItemList">The identifier item list.</param>
        /// <returns></returns>
        public List<ItemsSearchModel> GetFileDetailsList(bool Cubage, string fileNumber, List<FileItemModel> IdItemList)
        {
            SQLiteConnection _con = OpenConnect();
            List<ItemsSearchModel> allIt;
            var newList = new List<ItemsSearchModel>();
            if (Cubage)
            {
                for (int i = 0; i < IdItemList.Count; i++)
                {
                    if (IdItemList[i].IdFileItem!="0" && IdItemList[i].IdFileItem !=null )
                    {
                        allIt = _con.Query<ItemsSearchModel>
                            ("Select * from FileItemModel Fm inner join ItemFileModel Im  on Fm.IdFileItem = Im.IdFileItem and  Fm.Idrubric = Im.Idrubric   Where Im.IdFileItem = ? and Fm.File_Number = ? and Fm.Sent_Loading =0  and  Fm.Barecode = ? ", IdItemList[i].IdFileItem, fileNumber , IdItemList[i].Barecode);
                        foreach (var list in allIt)
                        {
                            newList.Add(list);
                        }
                    }
                    else if (IdItemList[i].IdItem !="0"&& IdItemList[i].IdItem != null)
                    {
                        allIt = _con.Query<ItemsSearchModel>
                            ("Select * from FileItemModel Fm inner join ItemModel Im  on Fm.IdItem = Im. IdItem and  Fm.Idrubric = Im.Idrubric Where Im.IdItem = ? and Fm.File_Number =? and Fm.Sent_Loading = 0 and  Fm.Barecode = ?", IdItemList[i].IdItem, fileNumber, IdItemList[i].Barecode);
                        foreach (var list in allIt)
                        {
                            newList.Add(list);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < IdItemList.Count; i++)
                {
                    allIt = _con.Query<ItemsSearchModel>
                        ("Select * from FileItemModel Fm inner join ItemModel Im  on Fm.IdItem = Im.IdItem and  Fm.Idrubric = Im.Idrubric Where Im.IdItem = ? and Fm.File_Number = ? and Fm.Sent_Loading =0 and  Fm.Barecode = ?", IdItemList[i].IdItem, fileNumber, IdItemList[i].Barecode);
                    foreach (var list in allIt)
                    {
                        newList.Add(list);
                    }
                }
            }

            CloseConenct(_con);
            return newList;
        }


        public void InsertDeliveryData(ItemDeliveredModel deliveryModelData)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                _con.CreateTable<ItemDeliveredModel>();
                
                if (!string.IsNullOrEmpty(deliveryModelData.Barecode))
                {
                    _con.Insert(deliveryModelData);
                    CloseConenct(_con);

                }


            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;
            }

        }

        public void InsertDeliveryListData(List<ItemDeliveredModel> deliveryModelData)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                _con.CreateTable<ItemDeliveredModel>();
                for(int i =0; i<deliveryModelData.Count;i++)
                {
                    int count = _con.ExecuteScalar<int>("Select count(*) from ItemDeliveredModel where Barecode =?", deliveryModelData[i].Barecode);
                    if (count <= 0)
                    {
                        //if barecode do not exist ==> insert .
                        _con.Insert(deliveryModelData[i]);

                    }
                    else
                    {
                        // if barecode already exist do not insert .
                    }

                }
                CloseConenct(_con);
            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;
            }

        }

        /// <summary>
        /// Updates the delivery data.
        /// </summary>
        /// <param name="deliveryModelData">The delivery model data.</param>
        public void UpdateDeliveryData(ItemDeliveredModel deliveryModelData)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                _con.Update(deliveryModelData);
                CloseConenct(_con);
            }
            catch (Exception e)
            {
                CloseConenct(_con);
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// Gets the count of photo path.
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        /// <returns></returns>
        public int GetCountOfPhotoPath(string barcode)
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                int count = _con.ExecuteScalar<int>("SELECT COUNT(Barecode) FROM ItemDeliveredModel WHERE Barecode= ?", barcode);
                CloseConenct(_con);
                return count;

            }
            catch (Exception e)
            {
                CloseConenct(_con);
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Gets all item name image quantity.
        /// </summary>
        /// <returns></returns>
        public List<ItemDeliveredModel> GetAllItemNameImageQuantity()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                var item = _con.Query<ItemDeliveredModel>("Select Name,ImageSVGPath from ItemDeliveredModel;");
                CloseConenct(_con);
                return item;
            }
            catch (Exception e)
            {
                CloseConenct(_con);
                throw e;
            }
        }

        /// <summary>
        /// Gets the photo list detail.
        /// </summary>
        /// <returns></returns>
        public List<ItemDeliveredModel> GetPhotoListDetail()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                var photoItem = _con.Query<ItemDeliveredModel>("Select Barecode,PhotoPath from ItemDeliveredModel where sent_delivery='False';");
                CloseConenct(_con);
                return photoItem;

            }
            catch (Exception e)
            {
                CloseConenct(_con);
                throw e;
            }
        }

        /// <summary>
        /// Gets the item delivery detail.
        /// </summary>
        /// <returns></returns>
        public List<ItemDeliveredModel> GetItemDeliveryDetail()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                var item = _con.Query<ItemDeliveredModel>("Select IdFIleItem,IdFileRoom,IdItem,IdRoom,File_Number,client_number,Barecode,Name,cub_valued,cub_dismount,cub_reassembly,cub_crate,comments_loading,comments_delivery,sent_loading,packed_by,Dismounting_type,mechanical_statement,Telephone_loading,Branche_loading,Telephone_delivery,Branche_delivery,sent_delivery from ItemDeliveredModel where sent_delivery='False';");
                CloseConenct(_con);
                return item;

            }
            catch (Exception e)
            {
                CloseConenct(_con);
                throw e;
            }
        }

        /// <summary>
        /// Gets all item by room file.
        /// </summary>
        /// <param name="idRoomFile">The identifier room file.</param>
        /// <returns></returns>
        public List<ItemFileModel> GetAllItemByRoomFile(string idRoomFile)
        {
            SQLiteConnection _con = OpenConnect();
            var room = _con.Query<ItemFileModel>("Select * from ItemFileModel where  IdFileRoom =?", idRoomFile);
            CloseConenct(_con);
            return room;
        }

        public List<ItemFileModel> GetAllItemByRoomFileCub(string idRoomFile)
        {
            SQLiteConnection _con = OpenConnect();

            var room = _con.Query<ItemFileModel>("select i.IdFIleItem,i.IdFileRoom,i.Idrubric,i.IdRoom,i.File_Number,i.Name,i.mounting_dismounting,i.cub_valued,i.mounting_dismounting,i.cub_dismount,i.cub_reassembly,i.cub_crate,i.comments_loading,i.comments_delivery,i.sent,i.packed_by,i. dismounting_type,i. reassembling_type,i.mounted_type,i.mechanical_statement,i.ImageSVG,i.SvgImagePath,i.Row,i.Columns from ItemFileModel i left join FileItemModel f on i.IdFIleItem=f.IdFIleItem AND i.IdFileRoom=f.IdFileRoom AND i.Idrubric=f.Idrubric where f.IdFIleItem is null AND (i.IdFileRoom =? OR i.IdRoom='0')", idRoomFile);
            CloseConenct(_con);
            return room;
        }


        public void SentDataToServer()
        {
            SQLiteConnection _con = OpenConnect();
            try
            {
                _con.Execute("UPDATE ItemDeliveredModel SET sent_delivery = 'True' WHERE sent_delivery = 'False'");
                CloseConenct(_con);
            }
            catch (Exception e)
            {
                CloseConenct(_con);
                System.Diagnostics.Debug.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Empties the database for load synchro.
        /// </summary>
        public void EmptyDatabaseForLoadSynchro()
        {
            SQLiteConnection _con = OpenConnect();
            var allSentItem = _con.Query<FileItemModel>("Select File_Number from FileItemModel where Sent_Loading = ?", 1);
            var allSentItemComma = string.Join("','", allSentItem.Select(i => i.File_Number));
            var allSentItemBarcode = _con.Query<FileItemModel>("Select Barecode from FileItemModel where Sent_Loading = ?", 1);
            var allSentItemBarcodeComma = string.Join("','", allSentItemBarcode.Select(i => i.Barecode));

            _con.Execute("DELETE FROM ItemPhotoModel WHERE Barecode IN ('" + allSentItemBarcodeComma + "');");
            _con.Execute("DELETE FROM FileItemModel WHERE Sent_Loading = ?", 1);
            _con.Execute("DELETE FROM FileInfoModel WHERE File_number IN ('" + allSentItemComma + "');");
            CloseConenct(_con);

        }


        /// <summary>
        /// Empties the database for delete synchro.
        /// </summary>
        public void EmptyDatabaseForDelSynchro()
        {
            SQLiteConnection _con = OpenConnect();
            var allSentItem = _con.Query<ItemDeliveredModel>("Select File_Number from ItemDeliveredModel where sent_delivery = ?", "True");
            var allSentItemComma = string.Join("','", allSentItem.Select(i => i.File_Number));
            var allSentItemBarcode = _con.Query<ItemDeliveredModel>("Select Barecode from ItemDeliveredModel where sent_delivery = ?", "True");           
            _con.Execute("DELETE FROM ItemDeliveredModel WHERE sent_delivery = ?", "True");
            _con.Execute("DELETE FROM FileInfoDeliveryModel WHERE File_number IN ('" + allSentItemComma + "');");
            CloseConenct(_con);
        }


        /// <summary>
        /// Gets all rubric delivery details.
        /// </summary>
        /// <returns></returns>
        public List<ListViewRubricModel> GetAllRubricDeliveryDetails(string fileNumber)
        {
            SQLiteConnection _con = OpenConnect();
            List<ListViewRubricModel> myList;
            try
            {
                myList= _con.Query<ListViewRubricModel>("Select idm.Idrubric, Count(idm.Idrubric) as CountRubric,Count(case AlreadyScanned when 'True' then 1 else null end) as CountAlreadyScannedRubric, rm.Name_fr, rm.Name_en, rm.ImageSVGPath from ItemDeliveredModel idm Inner Join RubricsModel rm on idm.Idrubric = rm.Idrubric Where File_Number = ? Group by idm.Idrubric", fileNumber);

            }
            catch (Exception)
            {
                myList = null;

            }
            CloseConenct(_con);
            return myList;

        }


        /// <summary>
        /// Gets all rubric delivery summary details.
        /// </summary>
        /// <returns></returns>
        public List<ListViewRubricModel> GetAllRubricDeliverySummaryDetails(string filenumber)
        {
            SQLiteConnection _con = OpenConnect();
            List<ListViewRubricModel> myList = new List<ListViewRubricModel>();
            try
            {
                myList = _con.Query<ListViewRubricModel>("Select idm.Idrubric, Count(idm.Idrubric) as CountRubric, rm.Name_fr, rm.Name_en, rm.ImageSVGPath from ItemDeliveredModel idm Inner Join RubricsModel rm on idm.Idrubric = rm.Idrubric  WHERE sent_delivery = '"+ "True" + "'  AND File_Number = ? Group by idm.Idrubric; ",filenumber);

            }
            catch (Exception)
            {
                myList = null;
            }
            CloseConenct(_con);
            return myList;

        }


        /// <summary>
        /// Gets the count all rubric delivery details.
        /// </summary>
        /// <returns></returns>   
        public int GetCountAllRubricDeliveryDetails(string filenumber)
        {
            SQLiteConnection _con = OpenConnect();
            int count;
            try
            {
                count = _con.ExecuteScalar<int>(
                    "Select  Sum(Tb.CountNumber) from (Select  Count(idm.Idrubric) as CountNumber from ItemDeliveredModel idm Inner Join RubricsModel rm on idm.Idrubric = rm.Idrubric  Where File_Number = ? Group by idm.Idrubric) Tb; ",filenumber);


            }
            catch (Exception e)
            {               
                count = 0;
            }
            CloseConenct(_con);
            return count;
        }
        /// <summary>
        /// Inserts the item file details.
        /// </summary>
        /// <param name="itemsFile">The items file.</param>
        public void InsertItemFileDetails(List<ItemFileModel> itemsFile)
        {
            SQLiteConnection _con = OpenConnect();
            _con.InsertAll(itemsFile);
            CloseConenct(_con);
        }

        /// <summary>
        /// Inserts the room model.
        /// </summary>
        /// <param name="roomsFileModel">The rooms file model.</param>
        public void InsertRoomFileModel(List<RoomFileModel> roomsFileModel)
        {
            SQLiteConnection _con = OpenConnect();
            _con.CreateTable<RoomFileModel>();
            _con.InsertAll(roomsFileModel);
            CloseConenct(_con);
        }

        public void InsertPhotoDetails(List<DeliveryPhotoModel> photo)
        {
         
            SQLiteConnection _con = OpenConnect();
            try
            {
                List<DeliveryPhotoModel> path = new List<DeliveryPhotoModel>();
                path = _con.Query<DeliveryPhotoModel>("select PhotoPath from DeliveryPhotoModel");
                if (path != null)
                {
                    if (path.Count > 0)
                    {
                        for (int i = 0; i < path.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(path[i].PhotoPath))
                            {
                                DependencyService.Get<IFileMgr>().DeleteDownloadedPhoto(path[i].PhotoPath);
                            }
                        }
                        _con.DeleteAll<DeliveryPhotoModel>();
                    }
                }
                _con.InsertAll(photo);
                CloseConenct(_con);
            }
            catch (Exception ex)
            {
                CloseConenct(_con);
                throw ex;
            }

        }

        public void InsertOnePhoto(DeliveryPhotoModel model)
        {
            SQLiteConnection _con = OpenConnect();
            _con.Insert(model);
            CloseConenct(_con);
        }

        /// <summary>
        /// Gets the room name en.
        /// </summary>
        /// <param name="IdRoom">The identifier room.</param>
        /// <returns></returns>
        public string GetRoomNameEN(string IdRoom)
        {
            SQLiteConnection _con = OpenConnect();
            string roomNameEN;
            try
            {
                roomNameEN = _con.ExecuteScalar<string>("Select Name_EN from RoomsModel Where IdRoom = ?; ", IdRoom);
            }
            catch (Exception e)
            {
                roomNameEN = "";
            }
            CloseConenct(_con);
            return roomNameEN;
        }

        /// <summary>
        /// Gets the room name fr.
        /// </summary>
        /// <param name="IdRoom">The identifier room.</param>
        /// <returns></returns>
        public string GetRoomNameFR(string IdRoom)
        {
            SQLiteConnection _con = OpenConnect();
            string roomNameFR;
            try
            {
                roomNameFR = _con.ExecuteScalar<string>("Select Name_FR from RoomsModel Where IdRoom = ?; ", IdRoom);
            }
            catch (Exception e)
            {
                roomNameFR = "";
            }
            CloseConenct(_con);
            return roomNameFR;
        }

        /// <summary>
        /// Gets the file room.
        /// </summary>
        /// <param name="IdFileRoom">The identifier file room.</param>
        /// <returns></returns>
        public string GetFileRoom(string IdFileRoom)
        {
            SQLiteConnection _con = OpenConnect();
            string roomFile;
            try
            {
                roomFile = _con.ExecuteScalar<string>("Select Name from RoomFileModel Where IdFileRoom = ?; ", IdFileRoom);
            }
            catch (Exception e)
            {
                roomFile = "";
            }
            CloseConenct(_con);
            return roomFile;
        }


        /// <summary>
        /// Gets the photo quality.
        /// </summary>
        /// <returns></returns>
        public string GetPhotoQuality()
        {
            SQLiteConnection _con = OpenConnect();
            string quality;
            try
            {
                quality = _con.ExecuteScalar<string>("select PhotoQuality from ConfigurationModel");
            }
            catch (Exception e)
            {
                quality = "";
            }
            CloseConenct(_con);
            return quality;
        }

        /// <summary>
        /// Gets the count of photo path loading.
        /// </summary>
        /// <param name="IdItem">The identifier item.</param>
        /// <returns></returns>
        public int GetCountOfPhotoPathLoading(string IdItem)
        {
            SQLiteConnection _con = OpenConnect();
            int count;
            try
            {
                 count = _con.ExecuteScalar<int>("SELECT COUNT(Barecode) from FileItemModel Where Barecode =?", IdItem);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return count;

        }

        /// <summary>
        /// Gets the PackingCygest id rubric 10 SVG path.
        /// </summary>
        /// <returns></returns>
        public string GetPackingCygestSvgPath()
        {
            SQLiteConnection _con = OpenConnect();
            string path;
            try
            {
                path = _con.ExecuteScalar<string>("SELECT ImageSVGPath FROM RubricsModel WHERE IdRubric='10'");
            }
            catch (Exception ex)
            {

                path="";
            }
            return path;
        }



    }
}
