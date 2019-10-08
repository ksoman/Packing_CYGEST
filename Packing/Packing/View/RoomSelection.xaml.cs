using PackingCygest.Model;
using PackingCygest.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Load Rooms
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomSelection : ContentPage
    {
        private readonly RoomSelectionViewModel _roomViewModel = new RoomSelectionViewModel();
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;
        private readonly string _languageSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomSelection"/> class.
        /// </summary>
        public RoomSelection()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomSelection"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public RoomSelection(NewLoadingModel loading, FileInfoModel fileInfo)
        {
            try
            {
                _languageSelected = _appViewModel.DefaultedCultureInfo;
                BindingContext = new RoomSelectionViewModel(new PageService(), loading, fileInfo, _languageSelected);
                _loading = loading;
                _fileInfo = fileInfo;
                InitializeComponent();
                Task.Delay(TimeSpan.FromSeconds(10));
                PopulateButtons(loading);
            }
            catch (Exception e)
            {

                throw e;
            } 

        }
        /// <summary>
        /// Populates the buttons.
        /// </summary>
        /// <param name="loading">The loading.</param>
        private void PopulateButtons(NewLoadingModel loading)
        {
            if( loading.RoomsFiles !=null)
            {

                PopulateRoomFileButton();
            }
            else
            {
                PopulateRoomButton();
            }
        }
        /// <summary>
        /// Populates the button.
        /// </summary>
        public void PopulateRoomButton()
        {
            var myList = _roomViewModel.ListRm;
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 0,
                ColumnSpacing = 0,

            };
            foreach (var roomName in myList)
            {
                string itemName;
                if (_languageSelected.Equals("fr-FR"))
                {
                    itemName = roomName.Name_FR;
                }
                else
                {
                    itemName = roomName.Name_EN;
                }
                //var source = FFImageLoading.Svg.Forms.SvgImageSource.FromFile(roomName.SvgImagePath);
                //FFImageLoading.Svg.Forms.SvgCachedImage svg = new FFImageLoading.Svg.Forms.SvgCachedImage
                //{
                //    Source = source,
                //    HeightRequest = 30,
                //    WidthRequest = 30,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    BackgroundColor = Color.White,
                //};

                var ressid = roomName.SvgImagePath;
                Controls.SvgIcon svg1 = new Controls.SvgIcon
                {
                    ResourceId = ressid,
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
                    Margin = new Thickness(0, 0, 0, 10)

                };
                StackLayout stacklay = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 105,
                    HeightRequest = 90,
                    Spacing = 0,
                    Margin = new Thickness(10, 0, 10, 10),

                };
                stacklay.Children.Add(svg1);
                stacklay.Children.Add(lbl);
                grid.Children.Add(stacklay, Convert.ToInt16(roomName.Column), Convert.ToInt16(roomName.Row));

                TapGestureRecognizer tap = new TapGestureRecognizer();
                stacklay.GestureRecognizers.Add(tap);
                tap.Tapped += delegate {
                    var rName = roomName.Name_EN;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ Button Text " + rName);
                    var list = new List<RoomsModel>(_roomViewModel.ListRm);
                    var room = list.Find(x => x.Name_EN == rName);
                    _loading.Room = room;
                    _loading.FileItemModel.IdRoom = roomName.IdRoom;
                    _loading.FileItemModel.RoomName= roomName.Name_EN;
                    _loading.SelectedIdRoom = roomName.IdRoom;
                    _loading.SelectedData = roomName.Name_EN;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ roomName.IdRoom " + roomName.IdRoom + " +++++++++++++++++++++ roomName.IdRoom " + roomName.Id);

                    Navigation.PushAsync(new RubricSelection(_loading, _fileInfo));
                };
            }
            StackLayout stack = new StackLayout()
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            stack.Children.Add(grid);
            ScrollView scroll = new ScrollView
            {
                Content = stack,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            MainLayout.Children.Add(scroll);
            Content = MainLayout;

        }
        /// <summary>
        /// Populates the room file button.
        /// </summary>
        public void PopulateRoomFileButton()
        {
            var myList = _roomViewModel.ListRoomFile;
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 0,
                ColumnSpacing = 0,

            };

            foreach (var roomDetails in myList)
            {
                string itemName = roomDetails.Name;
                //var source = FFImageLoading.Svg.Forms.SvgImageSource.FromFile(roomDetails.SvgImagePath);
                //FFImageLoading.Svg.Forms.SvgCachedImage svg = new FFImageLoading.Svg.Forms.SvgCachedImage
                //{
                //    Source = source,
                //    HeightRequest = 30,
                //    WidthRequest = 30,
                //    VerticalOptions = LayoutOptions.CenterAndExpand,
                //    HorizontalOptions = LayoutOptions.CenterAndExpand,
                //    BackgroundColor = Color.White,
                //};
                var ressid = roomDetails.SvgImagePath;
                Controls.SvgIcon svg = new Controls.SvgIcon
                {
                    ResourceId = ressid,
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
                    Margin = new Thickness(0, 0, 0, 10)

                };
                StackLayout stacklay = new StackLayout()
                {
                    BackgroundColor = Color.White,
                    WidthRequest = 105,
                    HeightRequest = 90,
                    Spacing = 0,
                    Margin = new Thickness(10, 0, 10, 10),

                };
                stacklay.Children.Add(svg);
                stacklay.Children.Add(lbl);
                grid.Children.Add(stacklay, Convert.ToInt16(roomDetails.Columns), Convert.ToInt16(roomDetails.Row));
                TapGestureRecognizer tap = new TapGestureRecognizer();
                stacklay.GestureRecognizers.Add(tap);
                tap.Tapped += delegate
                {
                    var rName = roomDetails.Name;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ Button Text " + rName);
                    var list = new List<RoomFileModel>(_roomViewModel.ListRoomFile);
                    var room = list.Find(x => x.Name == rName);
                    _loading.RoomFileMapper = room;
                    _loading.FileItemModel.IdFileRoom = roomDetails.IdFileRoom;
                    _loading.FileItemModel.RoomName = roomDetails.Name;
                    _loading.SelectedIdRoom = roomDetails.IdFileRoom;
                    _loading.SelectedData = roomDetails.Name;
                    ItemSelectionViewModel.ClickedAdd = false;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ roomDetails.IdFileRoom " + roomDetails.IdFileRoom);

                    Navigation.PushAsync(new ItemSelection(_loading, _fileInfo));
                };
            }
            StackLayout stack = new StackLayout()
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            stack.Children.Add(grid);
            ScrollView scroll = new ScrollView
            {
                Content = stack,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            MainLayout.Children.Add(scroll);
            Content = MainLayout;

        }
    }


}
