using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// This class contains all  methods to obtaine data from webservice
    /// when the application if configurer / or launched in WIFI zone
    /// </summary>
    public class LoadingDetails
    {
        #region Private Declaration
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private DatabaseAccessAsync _dataAccess;
        private DatabaseAccess _data;
        private ObservableCollection<RubricsModel> _rubricsModel;
        private ObservableCollection<ItemModel> _itemModel;
        private ObservableCollection<RoomsModel> _roomModel;
        private ObservableCollection<ItemFileModel> _itemFileModel;
        private ObservableCollection<RoomFileModel> _roomFileModel;
        private readonly NewLoadingService   _newLoadingService = new NewLoadingService();

        #endregion

        #region public Declaration
        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public ObservableCollection<RubricsModel> RubricsModel { get => _rubricsModel; set => _rubricsModel = value; }
        /// <summary>
        /// Gets or sets the data access.
        /// </summary>
        /// <value>
        /// The data access.
        /// </value>
        public DatabaseAccessAsync DataAccess { get => _dataAccess; set => _dataAccess = value; }

        public DatabaseAccess Data { get => _data; set => _data = value; }

        /// <summary>
        /// Gets or sets the item model.
        /// </summary>
        /// <value>
        /// The item model.
        /// </value>
        public ObservableCollection<ItemModel> ItemModel { get => _itemModel; set => _itemModel = value; }

        /// <summary>
        /// Gets or sets the item file model.
        /// </summary>
        /// <value>
        /// The item file model.
        /// </value>
        public ObservableCollection<ItemFileModel> ItemFileModel { get => _itemFileModel; set => _itemFileModel = value;   }
        /// <summary>
        /// Gets or sets the room model.
        /// </summary>
        /// <value>
        /// The room model.
        /// </value>
        public ObservableCollection<RoomsModel> RoomModel { get => _roomModel; set => _roomModel = value; }

        /// <summary>
        /// Gets or sets the room file model.
        /// </summary>
        /// <value>
        /// The room file model.
        /// </value>
        public ObservableCollection<RoomFileModel> RoomFileModel { get => _roomFileModel; set => _roomFileModel = value; }

        #endregion

        #region Public Methods
        public LoadingDetails()
        {
            DataAccess = new DatabaseAccessAsync();
            _data = new DatabaseAccess();
        }
        /// <summary>
        /// Gets the rooms.
        ///foreach item in room model
        //save to sd > ret path
        //create a room> populate room with item + path
        //insert room in db 
        /// </summary>
        /// <returns></returns>
        public async Task   GetRooms()
        {
            List<RoomsModel> room = new List<RoomsModel>();
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    RoomModel = await _newLoadingService.GetRoomsAsync();
                
                    if(RoomModel != null)
                    {
                          DropTableRoom();
                     
                        foreach (var rooms in RoomModel)
                        {
                            var path = DependencyService.Get<ISavePhoto>().SavetoSd(rooms.ImageSvg, rooms.Name_EN, "RoomSVG");
                            var roomModel = new RoomsModel
                            {
                                IdRoom = rooms.IdRoom,
                                Name_EN = rooms.Name_EN,
                                Name_FR = rooms.Name_FR,
                                ImageSvg = rooms.ImageSvg,
                                SvgImagePath = path,
                                
                            };
                            room.Add(roomModel);
                          
                        }
                        InsertRooms(room);
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
            
        }
      
        /// <summary>
        /// Gets the rooms.
        /// </summary>
        /// <param name="filenumber">The filenumber.</param>
        public async Task<List<RoomFileModel>> GetRoomsFile(string filenumber)
        {
            List<RoomFileModel> roomFile = new List<RoomFileModel>();
            try
            {              
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    RoomFileModel = await _newLoadingService.GetRoomsFileAsync(filenumber);
                    //foreach item in room model
                    //save to sd > ret path
                    //create a room> populate room with item + path
                    //insert room in db 
                    if (RoomFileModel != null)
                    {
                        DropTableRoomFile();

                        foreach (var rooms in RoomFileModel)
                        {
                            var path= DependencyService.Get<ISavePhoto>().SavetoSd(rooms.ImageSVG, rooms.Name, "RoomSVGFile");
                            var roomFileModel = new RoomFileModel
                            {
                                IdFileRoom = rooms.IdFileRoom,
                                Name = rooms.Name,
                                File_Number = rooms.File_Number,
                                ImageSVG = rooms.ImageSVG,
                                SvgImagePath = path
                            };
                            roomFile.Add(roomFileModel);
                            
                        }
                        InsertRoomsFile(roomFile);
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
            return roomFile;
        }

        
        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <returns></returns>
        public async Task GetItemDetails()
        {
            List<ItemModel> itemModel = new List<ItemModel>();
            try
            {
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    ItemModel = await _newLoadingService.GetItemAsync();
                    if(ItemModel !=null)
                    {
                     
                        DropTableItem();
                        foreach (var item in ItemModel)
                        {
                            var path = DependencyService.Get<ISavePhoto>().SavetoSd(item.ImageSVG, item.Name_en, "ItemSVG");
                            
                            var items = new ItemModel
                            {
                                IdItem = item.IdItem,
                                IdRubric = item.IdRubric,
                                ImageSVG = item.ImageSVG,                                
                                Name_en = item.Name_en,
                                Name_fr = item.Name_fr,
                                SvgImagePath = path,
                                mechanical_statement = item.mechanical_statement,
                                Dismounting_statement=item.mounting_dismounting,
                                Reassembling_statement=item.mounting_dismounting
                            };
                            itemModel.Add(items);
                           
                        }
                        InsertItemDetails(itemModel);
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
            
        }
        /// <summary>
        /// Gets the item details.
        /// </summary>
        public async Task<List<ItemFileModel>> GetItemDetailsFile(string fileNumber,bool cub)
        {
            List<ItemFileModel> itemFile = new List<ItemFileModel>();
            string svgPathPackingCygest = _data.GetPackingCygestSvgPath();
            try
            {
               
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {                            
                    ItemFileModel = await _newLoadingService.GetItemFileAsync(fileNumber);
                    if (ItemFileModel != null)
                    {
                        DropTableItemFile();
                        foreach (var item in ItemFileModel)
                        {
                           var  path = DependencyService.Get<ISavePhoto>().SavetoSd(item.ImageSVG, item.Name, "ItemSVGFile");
                            var itemsFileDetails = new ItemFileModel
                            {
                                File_Number = item.File_Number,
                                Name = item.Name,
                                IdFileRoom = item.IdFileRoom,
                                comments_delivery = item.comments_delivery,
                                comments_loading = item.comments_loading,
                                cub_crate = item.cub_crate,
                                cub_dismount = item.cub_dismount,
                                cub_reassembly = item.cub_reassembly,
                                cub_valued = item.cub_valued,
                                IdFIleItem = item.IdFIleItem,
                                mechanical_statement = item.mechanical_statement,
                                mounting_dismounting = item.mounting_dismounting,
                                mounted_type = item.mounted_type,
                                packed_by = item.packed_by,
                                sent = item.sent,
                                SvgImagePath = path,
                                ImageSVG = item.ImageSVG,
                                Idrubric = item.Idrubric,
                                IdRoom = item.IdRoom                                

                            };
                            itemFile.Add(itemsFileDetails);
                           
                        }
                        if(cub)
                        {
                            string itemName = "";
                            if(_appViewModel.DefaultedCultureInfo.Equals("en-US"))
                            {
                                itemName = "PackingCygest";
                            }
                            else
                            {
                                itemName = "Carton";
                            }

                            var itemCubCarton = new ItemFileModel
                            {
                                File_Number = fileNumber,
                                Name = itemName,
                                IdFileRoom = "0",
                                comments_delivery = "",
                                comments_loading = "",
                                cub_crate = "",
                                cub_dismount = "",
                                cub_reassembly = "",
                                cub_valued = "",
                                IdFIleItem = "0",
                                mechanical_statement = "",
                                mounting_dismounting ="",
                                mounted_type = "",
                                packed_by = "",
                                sent = "",
                                SvgImagePath = svgPathPackingCygest,
                                ImageSVG = "",
                                Idrubric = "10",
                                IdRoom = "0"

                            };
                            itemFile.Add(itemCubCarton);
                        }
                        
                        InsertItemFileDetails(itemFile);
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
            return itemFile;
        }

        /// <summary>
        /// Gets the rubrics.
        /// </summary>
        /// <returns></returns>
        public async Task GetRubrics()
        {
            try
            {              
                var connected = CrossConnectivity.Current.IsConnected;

                if (connected)
                {
                    RubricsModel = await _newLoadingService.GetRubricItemAsync();
                    List<RubricsModel> rubricsDetails = new List<RubricsModel>();
                    if (RubricsModel != null)
                    {
                        DropTableRubric();
                        foreach (var rub in RubricsModel)
                        {
                            var path = DependencyService.Get<ISavePhoto>().SavetoSd(rub.ImageSvg, rub.Name_en, "RubricSVG");
                            var rubrics = new RubricsModel
                            {
                                IdRubric = rub.IdRubric,
                                ImageSvg = rub.ImageSvg,
                                ImageSVGPath = path,
                                Name_en = rub.Name_en,
                                Name_fr = rub.Name_fr,
                            };
                            rubricsDetails.Add(rubrics);
                          
                        }
                        InsertRubrics(rubricsDetails);
                    }
                
                }
            }
            catch (Exception e)
            {            
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(System.Globalization.CultureInfo.CurrentCulture), MethodName = e.Source
                });

            }
        }

       
        #endregion

        #region private Methods
        
        /// <summary>
        /// Inserts the item details.
        /// </summary>
        /// <param name="itemDetails">The item details.</param>
        private void InsertItemDetails( List<ItemModel> itemDetails)
        {

            _data.InsertItem(itemDetails);
            
        }
        /// <summary>
        /// Inserts the item file details.
        /// </summary>
        /// <param name="itemFileDetails">The item file details.</param>
        private void InsertItemFileDetails(List<ItemFileModel> itemFileDetails)
        {

            _data.InsertItemFileDetails(itemFileDetails);

        }

        /// <summary>
        /// Inserts the room single details.
        /// </summary>
        /// <param name="roomModel">The roomModel.</param>
        public void  InsertRooms(List<RoomsModel> roomModel)
        {
            _data.InsertRoomModel(roomModel);
        }

        /// <summary>
        /// Inserts the single room.
        /// </summary>
        /// <param name="roomFileModel">The room file model.</param>
        public void InsertRoomsFile(List<RoomFileModel> roomFileModel)
        {
            _data.InsertRoomFileModel(roomFileModel);
        }

        /// <summary>
        /// Inserts the single rubric.
        /// </summary>
        /// <param name="rub">The rub.</param>
        public void InsertRubrics(List <RubricsModel> rub)
        {
            _data.InsertRubricData(rub);
        }
        public void DropTableItemFile()
        {
            _data.DropTableItemFileDetails();
        }
        /// <summary>
        /// Drops the table room file.
        /// </summary>
        public void DropTableRoomFile()
        {
            _data.DropTableRoomFileDetails();
        }
        /// <summary>
        /// Drops the table room.
        /// </summary>
        public void DropTableRoom()
        {
            _data.DropTableRoom();
        }
        /// <summary>
        /// Drops the table rubric.
        /// </summary>
        public void DropTableRubric()
        {
            _data.DropTableRubrics();
        }
        /// <summary>
        /// Drops the table item.
        /// </summary>
        public void DropTableItem()
        {
            _data.DropTableItems();
        }

        #endregion
    }
}
