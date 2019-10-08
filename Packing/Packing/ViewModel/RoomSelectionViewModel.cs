using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{

    /// <summary>
    /// RoomSelectionViewModel
    /// </summary>
    public class RoomSelectionViewModel :BaseViewModel
    {
        private IPageService _pageSercice;        
        private List<RoomsModel> _rooms;
        private readonly FileInfoModel _fileInfoModel;
        private readonly NewLoadingModel _newLoading;
        private string _lblRoom;
        private static ObservableCollection<RoomsModel> _listRm;
        private static ObservableCollection<RoomFileModel> _listRoomFile;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        /// <summary>
        /// Gets or sets the label room.
        /// </summary>
        /// <value>
        /// The label room.
        /// </value>
        public string LblRoom
        {
            get { return _lblRoom; }
            set { SetValue(ref _lblRoom, value); }
        }
        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public List<RoomsModel> Rooms
        {
            get => _rooms;
            set => _rooms = value;
        }
        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public ICommand Back
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list rm.
        /// </summary>
        /// <value>
        /// The list rm.
        /// </value>
        public ObservableCollection<RoomsModel> ListRm
        {
            get { return _listRm; }
            set { SetValue(ref _listRm, value); }
        }

        /// <summary>
        /// Gets or sets the page sercice.
        /// </summary>
        /// <value>
        /// The page sercice.
        /// </value>
        public IPageService PageSercice
        {
            get => _pageSercice;
            set => _pageSercice = value;
        }
        /// <summary>
        /// Gets or sets the list room file.
        /// </summary>
        /// <value>
        /// The list room file.
        /// </value>
        public  ObservableCollection<RoomFileModel> ListRoomFile
        {
            get { return _listRoomFile; }
            set { SetValue(ref _listRoomFile, value); }
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomSelectionViewModel"/> class.
        /// </summary>
        /// 
        public RoomSelectionViewModel()
        {

        }
        public RoomSelectionViewModel(NewLoadingModel loading)
        {
            _newLoading = loading;
            PopulateView(loading);
            
          
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomSelectionViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        public RoomSelectionViewModel(IPageService pageService,NewLoadingModel loading, FileInfoModel fileInfo, string language)
        {
            
            HandleTranslation(_appViewModel.DefaultedCultureInfo);     
            _newLoading = loading;
            PageSercice = pageService;
            PopulateView(loading);
            _fileInfoModel = fileInfo;
            //Registration of button.
            Back = new Command(GoBack);
          
        }
        /// <summary>
        /// Populates the view.
        /// </summary>
        /// <param name="loading">The loading.</param>
        private void PopulateView(NewLoadingModel loading)
        {
            if (loading.RoomsFiles != null)
            {
                PopulateRoomFileView();
            }
            else
            {
                PopulateRoomView();
            }
        }

        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblRoom = Localize.GetString("LblRoom", cultureInfo);

        }

            /// <summary>
            /// Populates the view.
            /// </summary>
            public ObservableCollection<RoomsModel> PopulateRoomView()
            {
            //get rooms from Populaterooms
            //set colum and row positions
            //set the maximum column.
            //loop through the list and create a list of room with respective colums and row numbers
            //wait to execute
           
            List<RoomsModel> tempRm = new List<RoomsModel>();
            
                var tempList = _newLoading.Rooms;
                int col = -1;
                int row = 0;
                int maxColumn = 2;
                


                for (int i = 0; i < tempList.Count; i++)
                {
               
                if (col != maxColumn)
                    {

                        col += 1;
                        RoomsModel rm = new RoomsModel
                        {
                            Column = col.ToString(),
                            Row = row.ToString(),
                            IdRoom = tempList[i].IdRoom,
                            Name_EN = tempList[i].Name_EN,
                            Name_FR = tempList[i].Name_FR,
                            ImageSvg = tempList[i].ImageSvg,
                            SvgImagePath = tempList[i].SvgImagePath,


                        };
                        tempRm.Add(rm);


                    }
                    else
                    {
                        col = 0;
                        row++;
                        RoomsModel rm = new RoomsModel
                        {
                            Column = col.ToString(),
                            Row = row.ToString(),
                            IdRoom = tempList[i].IdRoom,
                            Name_EN = tempList[i].Name_EN,
                            Name_FR = tempList[i].Name_FR,
                            ImageSvg = tempList[i].ImageSvg,
                            SvgImagePath = tempList[i].SvgImagePath,


                        };
                        tempRm.Add(rm);
                    }
                }

            _listRm = new ObservableCollection<RoomsModel>(tempRm as List<RoomsModel>);
            
            return _listRm;
            
        }
        /// <summary>
        /// Populates the room file view.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<RoomFileModel> PopulateRoomFileView()
        {
            List<RoomFileModel> tempRmFile = new List<RoomFileModel>();

            var tempList = _newLoading.RoomsFiles;
            int col = -1;
            int row = 0;
            int maxColumn = 2;

            for (int i = 0; i < tempList.Count; i++)
            {
                if (col != maxColumn)
                {
                    col += 1;
                    RoomFileModel rm = new RoomFileModel
                    {
                        Columns = col,
                        Row = row,
                        IdFileRoom = tempList[i].IdFileRoom,
                        Name = tempList[i].Name,                     
                        ImageSVG = tempList[i].ImageSVG,
                        SvgImagePath = tempList[i].SvgImagePath,
                        File_Number =tempList[i].File_Number                       
                        
                    };
                    tempRmFile.Add(rm);


                }
                else
                {
                    col = 0;
                    row++;
                    RoomFileModel rm = new RoomFileModel
                    {
                        Columns = col,
                        Row = row,
                        IdFileRoom = tempList[i].IdFileRoom,
                        Name = tempList[i].Name,
                        ImageSVG = tempList[i].ImageSVG,
                        SvgImagePath = tempList[i].SvgImagePath,
                        File_Number = tempList[i].File_Number

                    };
                    tempRmFile.Add(rm);
                }
            }

            _listRoomFile = new ObservableCollection<RoomFileModel>(tempRmFile as List<RoomFileModel>);

            return _listRoomFile;

        }
       
        /// <summary>
        /// Goes the back.
        /// </summary>
        private void GoBack()
        {
             PageSercice.PushAsync(new LoadMenu(_newLoading, _fileInfoModel));
        }
    }
}
