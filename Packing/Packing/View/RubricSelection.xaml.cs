using PackingCygest.Model;
using PackingCygest.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PackingCygest.Utils;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Load Selection Item Page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RubricSelection : ContentPage
    {
        #region Private
        private RoomSelectionViewModel _roomViewModel;
        private readonly RubricViewModel _rubView = new RubricViewModel();
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;
        private RoomSelectionViewModel _room ;
        private ObservableCollection<RoomsModel> _roomList;
        private ObservableCollection<RoomFileModel> _roomFileList;
        private StackLayout _stack;
        private BorderlessPicker _picker;
        private StackLayout _stacklay;
        private StackLayout _pickerStack;
        private ScrollView _scroll;
        #endregion

        private string _languageSelected;
        private string _roomName;
        private string _index;
        public string Index { get => _index; set => _index = value; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RubricSelection"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public RubricSelection(NewLoadingModel loading, FileInfoModel fileInfo)
        {     
            _languageSelected  = _appViewModel.DefaultedCultureInfo;
            BindingContext = new RubricViewModel(new PageService(),loading, fileInfo);
            _loading = loading;
            _fileInfo = fileInfo;
            InitializeComponent();
            
            if(_loading.Search && _loading.SelectedData != null)
            {
                _roomName = _loading.SelectedData;
            }
            else
            {
                if (_languageSelected.Equals("fr-FR"))
                {
                    if(_loading.Room != null)
                    {
                        _roomName = _loading.Room.Name_FR;
                    }
                    else
                    {
                        _roomName = _loading.RoomFileMapper.Name;
                    }
                    
                    
                }
                else
                {
                    if (_loading.Room != null)
                    {
                        _roomName = _loading.Room.Name_EN;
                    }
                    else
                    {
                        _roomName = _loading.RoomFileMapper.Name;
                    }
                }
                

            }
            _room = new RoomSelectionViewModel(_loading);
            HasCubageData(fileInfo);
         
           

        }

        /// <summary>
        /// Determines whether [has cubage data] [the specified file information].
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        private void HasCubageData(FileInfoModel fileInfo)
        {
            if( fileInfo !=null && fileInfo.Info_Cubage)
            {

                PopulateButtonRoomsFile(_roomName, _loading, _fileInfo);
            }
            else
            {
                
                _loading.RubricFlow = true; //show picker on item selection page
                PopulateButtonRooms(_roomName, _loading, _fileInfo);
            }
        }


        /// <summary>
        /// Populates the button rooms file.
        /// </summary>
        /// <param name="val">The value.</param>
        /// <param name="load">The load.</param>
        /// <param name="fileInfo">The file information.</param>
        private void PopulateButtonRoomsFile(string val, NewLoadingModel load, FileInfoModel fileInfo)
        {
            var myList = _rubView.ListRub;
            _roomViewModel = new RoomSelectionViewModel();
            List<string> allRoomsFile = new List<string>();
            if (_roomViewModel.ListRoomFile != null)
            {         
                _roomFileList = new ObservableCollection<RoomFileModel>(_roomViewModel.ListRoomFile);

                foreach (var value in _roomFileList)
                {
                    allRoomsFile.Add(value.Name);                  
                }

                var list = new List<RoomFileModel>(_roomFileList);
                var listOfRoomFile = load.RoomsFiles;

                var index = _roomFileList.IndexOf(list.Find(x => x.Name == val));
                if (val != null)
                {
                    /*_picker = new BorderlessPicker
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        ItemsSource = allRoomsFile,
                        SelectedItem = val,
                        SelectedIndex = index,
                        HorizontalOptions = LayoutOptions.Center     
                    };
                    _pickerStack = new StackLayout();
                    _pickerStack.VerticalOptions = LayoutOptions.Center;
                    _pickerStack.HorizontalOptions = LayoutOptions.Center;
                    _pickerStack.Spacing = 0;
                    _pickerStack.BackgroundColor = Color.White;
                    _pickerStack.WidthRequest = 325;
                    _pickerStack.HeightRequest = 50;
                    _pickerStack.Children.Add(_picker);

                    _picker.SelectedIndexChanged += (sender, args) =>
                    {
                        if (_picker.SelectedIndex != -1)
                        {
                            System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Index of " + _picker.SelectedIndex);
                            Index = _picker.SelectedIndex.ToString();
                            var idRoom = listOfRoomFile[_picker.SelectedIndex].IdFileRoom;
                            load.FileItemModel.IdFileRoom = idRoom;
                            load.FileItemModel.RoomName = listOfRoomFile[_picker.SelectedIndex].Name;
                            if (_loading.Search)
                            {
                                _loading.SelectedData = listOfRoomFile[_picker.SelectedIndex].Name;
                                _loading.SelectedIdRoom = idRoom;
                            }
                            System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Id Rooms File " + idRoom);

                        }
                    };
                        MainLayout.Children.Add(_pickerStack);*/
                        PopulateGrid(myList, load, fileInfo);
                }
            }

        }

        /// <summary>
        /// Populates the button.
        /// </summary>
        private void PopulateButtonRooms(string val , NewLoadingModel load, FileInfoModel fileInfo)
        {

            var myList = _rubView.ListRub;

            int index = 0;
            
            _roomList = new ObservableCollection<RoomsModel>(_room.ListRm);

            List<string> allRooms = new List<string>();

            var listOfRooms = _loading.Rooms;
      
            var list = new List<RoomsModel>(_roomList);
            if (_loading.Rooms != null)
            {
                
                foreach (var value in listOfRooms)
                {
                    if (_languageSelected.Equals("fr-FR"))
                    {
                        allRooms.Add(value.Name_FR);
                    }
                    else
                    {
                        allRooms.Add(value.Name_EN);
                    //index = allRooms.IndexOf(val);
                }
            }
            if (_loading.Search)
            {
                index = _roomList.IndexOf(list.Find(x => x.Name_EN == val));

            }
            else
            {
                if (_languageSelected.Equals("fr-FR"))
                {
                    index = _roomList.IndexOf(list.Find(x => x.Name_FR == val));
                }
                else
                {
                    index = _roomList.IndexOf(list.Find(x => x.Name_EN == val));
                    }
                }

                if (val != null)
                {
                    _picker = new BorderlessPicker
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        ItemsSource = allRooms,
                        SelectedItem = val,
                        SelectedIndex = index,
                        HorizontalOptions = LayoutOptions.Center,

                    };
                    _pickerStack = new StackLayout();
                    _pickerStack.VerticalOptions = LayoutOptions.Center;
                    _pickerStack.HorizontalOptions = LayoutOptions.Center;
                    _pickerStack.Spacing = 0;
                    _pickerStack.BackgroundColor = Color.White;
                    _pickerStack.WidthRequest = 325;
                    _pickerStack.HeightRequest = 50;
                    _pickerStack.Children.Add(_picker);
                    _picker.SelectedIndexChanged += (sender, args) =>
                    {
                    if (_picker.SelectedIndex != -1)
                    {
                        System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Index of " + _picker.SelectedIndex);
                        Index = _picker.SelectedIndex.ToString();
                        var idRoom = listOfRooms[_picker.SelectedIndex].IdRoom;
                        load.FileItemModel.IdRoom = idRoom;
                        load.FileItemModel.RoomName = listOfRooms[_picker.SelectedIndex].Name_EN;
                        if(_loading.Search)
                        {
                            _loading.SelectedData = listOfRooms[_picker.SelectedIndex].Name_EN;
                            _loading.SelectedIdRoom = idRoom;
                        }
                        System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Id Rooms of " + idRoom);

                    }


                };

                PopulateGrid(myList,load, fileInfo);
                }
            }
        }

        /// <summary>
        /// Populates the grid.
        /// </summary>
        /// <param name="rubs">The rubs.</param>
        /// <param name="load">The load.</param>
        /// <param name="file">The file.</param>
        public void PopulateGrid(ObservableCollection<RubricsModel> rubs, NewLoadingModel load, FileInfoModel file)
        {
            var rubrics = new ObservableCollection<RubricsModel>(rubs);

            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 0,
                ColumnSpacing = 0,

            };

            foreach (var item in rubrics)
            {
                //var source = FFImageLoading.Svg.Forms.SvgImageSource.FromFile(item.ImageSVGPath);
                var itemName = _languageSelected.Equals("fr-FR") ? item.Name_fr : item.Name_en;
                //var svg = new FFImageLoading.Svg.Forms.SvgCachedImage
                //{
                //    Source = source,
                //    HeightRequest = 30,
                //    WidthRequest = 30,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    BackgroundColor = Color.White,
                //};
                var svg = item.ImageSVGPath;
                Controls.SvgIcon svg1 = new Controls.SvgIcon
                {
                    ResourceId = svg,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Color.White,
                };
                var lbl = new Label
                {
                    BackgroundColor = Color.White,
                    Text = itemName,
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 20)

                };

                _stacklay = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 105,
                    HeightRequest = 90,
                    Spacing = 0,
                    Margin = new Thickness(10, 0, 10, 10),

                };

                _stacklay.Children.Add(svg1);
                _stacklay.Children.Add(lbl);
                grid.Children.Add(_stacklay, item.Columns, item.Row);

                TapGestureRecognizer tap = new TapGestureRecognizer();
                _stacklay.GestureRecognizers.Add(tap);
                tap.Tapped += delegate {
                    var rName = item.Name_en;
                    var idRubrics = item.IdRubric;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ Button Text " + rName);
                    var lr = new List<RubricsModel>(rubrics);
                    var rub = lr.Find(x => x.Name_en == rName);
                    load.Item = new ItemModel();
                    load.Rubric = new RubricsModel();
                    if (idRubrics != null && rName != null)
                    {
                        load.Item.IdRubric = idRubrics;
                        load.Rubric.Name_en = rName;
                    }
                    load.Rubric = rub;
                    load.FileItemModel.Idrubric = idRubrics;
                    load.FileItemModel.RubricsName = rName;
                    Navigation.PushAsync(new ItemSelection(load, file));

                };


            } // end foreach

            _stack = new StackLayout()
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            _stack.Children.Add(grid);

            _scroll = new ScrollView
            {
                Content = _stack,
                VerticalOptions = LayoutOptions.FillAndExpand,

            };

            MainLayout.Children.Add(_scroll);
            // MainLayout.Children.Insert(1, stack);

            Content = MainLayout;
        }
    }
        
}
