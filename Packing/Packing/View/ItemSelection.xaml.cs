using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PackingCygest.View
{
    /// <summary>
    /// Class Load New item
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemSelection : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSelection"/> class.
        /// </summary>
        /// 
        private readonly ItemSelectionViewModel _loadNew = new ItemSelectionViewModel();
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfo;
        private ObservableCollection<RubricsModel> _rubricList;
        public string BtnAdd { get; private set; }
        private string _index;
        private ObservableCollection<ItemModel> _itemsList;
        private ObservableCollection<ItemFileModel> _itemsFileList;
        private  StackLayout _stack;
        private Button _addBtn;
        private  BorderlessPicker _picker;
        private StackLayout _stacklay;
        private StackLayout _pickerStack;
        private ScrollView _scroll;
        private string _languageSelected;
        
        public string Index { get => _index; set => _index = value; }

        public ObservableCollection<RubricsModel> RubricList
        {
            get { return _rubricList; }
            set { _rubricList = value; }
        }

        public ObservableCollection<ItemFileModel> ItemsFileList
         {
            get { return _itemsFileList; }
            set { _itemsFileList = value; }
        }


        public ItemSelection()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSelection"/> class.
        /// </summary>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public ItemSelection(NewLoadingModel loading, FileInfoModel fileInfo)
        {
            InitializeComponent();        
            _languageSelected  = _appViewModel.DefaultedCultureInfo;
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _loading = loading;
            BindingContext = new ItemSelectionViewModel(new PageService(), loading, fileInfo);
            _fileInfo = fileInfo;
            PopulateButtons(loading);         
        }
        /// <summary>
        /// Populates the buttons.
        /// </summary>
        /// <param name="loading">The loading.</param>
        private void PopulateButtons(NewLoadingModel loading)
        {
            if (loading.ItemsFiles != null && _fileInfo.Info_Cubage && !ItemSelectionViewModel.ClickedAdd)           
            {
                if(!string.IsNullOrEmpty(_loading.FileItemModel.Idrubric) && !string.IsNullOrEmpty(_loading.FileItemModel.IdRoom) && !string.IsNullOrEmpty(_loading.FileItemModel.IdFileItem))
                {
                    if (_loading.FileItemModel.Idrubric.Equals("10") && _loading.FileItemModel.IdRoom.Equals("0") && _loading.FileItemModel.IdFileItem.Equals("0") && !_loading.RubricFlow)
                    {
                        if(_loading.Rubric == null)
                        {
                            _loading.Rubric = new RubricsModel();
                            _loading.Rubric.IdRubric = "10";
                            _loading.Rubric.Name_en = "PackingCygests";

                        }

                        PopulateItemButtons();
                        //load page with rubric 10 PackingCygest items
                    }
                    else
                    {
                        PopulateItemFileButtons();
                        //normal flow

                    }
                }
                else
                {
                    PopulateItemFileButtons();
                    

                }

            }
          
            else
            {
                 PopulateItemButtons();
                //from rubric page load rubric items
            }
        }
        /// <summary>
        /// Populates the item buttons.
        /// </summary>
        /// 

        public void HandleTranslation(string cultureInfo)
        {
            BtnAdd = Localize.GetString("BtnAdd", cultureInfo);
           
        }

        /// <summary>
        /// Populates the item buttons.
        /// </summary>
        public void PopulateItemButtons()
        {

            List<string> allRubrics = new List<string>();
      //      RubricViewModel rbv = new RubricViewModel();

         //   RubricList = new ObservableCollection<RubricsModel>(rbv.ListRub);
          
            var listOfRubrics = _loading.Rubrics;

            foreach (var value in listOfRubrics)
            {
                if (_languageSelected.Equals("fr-FR"))
                {
                    allRubrics.Add(value.Name_fr);
                }
                else
                {
                    allRubrics.Add(value.Name_en);
                }
                
            }
            var val = _loading.Rubric;
            int ii;
           
            if (val!=null)

            {
                Index = (val.IdRubric);

                if (_languageSelected.Equals("fr-FR"))
                {
                    ii = allRubrics.IndexOf(val.Name_fr);
                }
                else
                {
                    ii = allRubrics.IndexOf(val.Name_en);
                }

                _itemsList = new ObservableCollection<ItemModel>(_loadNew.PopulateView(Index));

                if (val != null && !ItemSelectionViewModel.ClickedAdd &&_loading.RubricFlow)  //show picker for only flow from Rubric page where cub is false
                {
                    _picker = new BorderlessPicker
                    {
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        ItemsSource = allRubrics,
                        SelectedItem = val,
                        SelectedIndex = ii,
                        HorizontalOptions = LayoutOptions.Center,
                        BackgroundColor = Color.White
                    };

                    _pickerStack = new StackLayout();
                    _pickerStack.VerticalOptions = LayoutOptions.Center;
                    _pickerStack.HorizontalOptions = LayoutOptions.Center;
                    _pickerStack.Spacing = 0;
                    _pickerStack.BackgroundColor = Color.White;
                    _pickerStack.WidthRequest = 325;
                    _pickerStack.HeightRequest = 50;
                    _pickerStack.Children.Add(_picker);

                    MainLayout.Children.Add(_pickerStack);


                    _picker.SelectedIndexChanged += (sender, args) =>
                    {
                        if (_picker.SelectedIndex != -1)
                        {
                            _stack.Children.Clear();
                            MainLayout.Children.Remove(_scroll);
                            System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Index of " + _picker.SelectedIndex);
                            Index = _picker.SelectedIndex.ToString();
                            var idRub = listOfRubrics[_picker.SelectedIndex].IdRubric;
                            _loading.FileItemModel.Idrubric = idRub;
                            _loading.FileItemModel.RubricsName = listOfRubrics[_picker.SelectedIndex].Name_en;
                            System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>> Id Rubrics of " + idRub);
                            _itemsList = new ObservableCollection<ItemModel>(_loadNew.PopulateView(idRub));
                            PopulateGrid(_itemsList);
                        }
                        
                    };
                   
                }
                PopulateGrid(_itemsList);

            }      
        }

        /// <summary>
        /// Populates the item file buttons.
        /// _loading.Search is used to handle the editing of an scanned item 
        /// </summary>
        public void PopulateItemFileButtons()
        {
            //if loading.search is true hence it is a modification --> load every item in select item page
            //if loading.search is false hence it is a addition of a new item -->remove item already selection from list in select item page for CUB only

            if (_loading.Search)
            {
                if (_fileInfo.Info_Cubage)
                {
                    ItemsFileList = new ObservableCollection<ItemFileModel>(_loadNew.PopulateItemFileView(_loading.SelectedIdRoom,false));
                }
                else
                {
                    ItemsFileList = new ObservableCollection<ItemFileModel>(_loadNew.PopulateItemFileView(_loading.SelectedIdRoom,false));
                }             
            }
            else
            {
                if (_fileInfo.Info_Cubage)
                {
                    ItemsFileList = new ObservableCollection<ItemFileModel>(_loadNew.PopulateItemFileView(_loading.FileItemModel.IdFileRoom, true));
                }
                else
                {
                    ItemsFileList = new ObservableCollection<ItemFileModel>(_loadNew.PopulateItemFileView(_loading.FileItemModel.IdFileRoom, false));

                }

            }

            PopulateItemFileGrid(ItemsFileList);

        }
        /// <summary>
        /// Populate Grid With Rubrics 
        /// </summary>
        /// <param name="itms"></param>
        /// ScrollView => StackLayout => Grid 
        public void PopulateGrid(ObservableCollection<ItemModel> itms)
        {
            var items = new ObservableCollection<ItemModel>(itms);

            Grid grid = new Grid
            {
                BackgroundColor = (Color) Application.Current.Resources["GreyBackColour"],
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 0,
                ColumnSpacing = 0,

            };

            foreach (var item in items)
            {
                string source;

                if (item.SvgImagePath != null)
                {
                    source = item.SvgImagePath;
                }
                else
                {
                    source = "resource://PackingCygest.Image.box.svg";
                }

                string itemName;
                if (_languageSelected.Equals("fr-FR"))
                {
                    itemName = item.Name_fr;
                }
                else
                {
                    itemName = item.Name_en;
                }
                var svg = new Controls.SvgIcon
                {
                    ResourceId = source,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
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

                _stacklay.Children.Add(svg);
                _stacklay.Children.Add(lbl);
                grid.Children.Add(_stacklay, item.Columns, item.Row);

                TapGestureRecognizer tap = new TapGestureRecognizer();
                _stacklay.GestureRecognizers.Add(tap);
                tap.Tapped += delegate {
                    var rName = item.Name_en;
                    System.Diagnostics.Debug.WriteLine(" +++++++++++++++++++++ Button Text " + rName);
                    if (_languageSelected.Equals("fr-FR"))
                    {
                        _loading.FileItemModel.Name = item.Name_fr;
                    }
                    else
                    {
                        _loading.FileItemModel.Name = item.Name_en;
                    }

                    _loading.FileItemModel.IdItem = item.IdItem;
                    _loading.FileItemModel.Idrubric = item.IdRubric;
                    _loading.FileItemModel.Item_MS = item.mechanical_statement;
                    _loading.FileItemModel.Reassembling_type = item.Reassembling_statement;
                    _loading.FileItemModel.Dismounting_type = item.Dismounting_statement;

                    Navigation.PushAsync(new PackingCygestType(_loading, _fileInfo));

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

        /// <summary>
        /// Populates the item file grid.
        /// </summary>
        /// <param name="itms">The itms.</param>
        public void PopulateItemFileGrid(ObservableCollection<ItemFileModel> itms)
        {
            var items = new ObservableCollection<ItemFileModel>(itms);

            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(10, 0, 10, 10),
                RowSpacing = 0,
                ColumnSpacing = 0,

            };

            foreach (var item in items)
            {
               string source;


                if (item.SvgImagePath != null)
                {
                     source = (item.SvgImagePath);
                }
                else
                {
                    source = ("resource://PackingCygest.Image.box.svg");
                }

                var svg = new Controls.SvgIcon
                {
                    ResourceId = source,
                    HeightRequest = 30,
                    WidthRequest = 30,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.Center,
                    BackgroundColor = Color.White,
                };

                var lbl = new Label
                {
                    BackgroundColor = Color.White,
                    Text = item.Name,
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

                _stacklay.Children.Add(svg);
                _stacklay.Children.Add(lbl);
                grid.Children.Add(_stacklay, item.Columns, item.Row);

                TapGestureRecognizer tap = new TapGestureRecognizer();
                _stacklay.GestureRecognizers.Add(tap);
                tap.Tapped += delegate {
                  
                  //  var pages = Application.Current.MainPage.Navigation.NavigationStack.ToList();

                  if(item.IdRoom.Equals("0") && item.IdFIleItem.Equals("0") && item.IdFileRoom.Equals("0"))
                    {
                        // go to rubric 10 PackingCygest page
                        _loading.FileItemModel.Idrubric = "10";
                        _loading.FileItemModel.IdRoom = "0";
                        _loading.FileItemModel.IdFileItem= "0";
                        _loading.RubricFlow = false; //hide picker
                        Navigation.PushAsync(new ItemSelection(_loading, _fileInfo));


                    }
                    else
                    {
                        //normal flow
                        _loading.FileItemModel.IdFileItem = item.IdFIleItem;
                        _loading.FileItemModel.Name = item.Name;
                        _loading.FileItemModel.Idrubric = item.Idrubric;
                        _loading.FileItemModel.Item_MS = item.mechanical_statement;
                        _loading.FileItemModel.Mounting_dismounting = item.mounting_dismounting;
                        _loading.FileItemModel.Dismounting_type = item.cub_dismount;
                        _loading.FileItemModel.Reassembling_type = item.cub_reassembly;

                        Navigation.PushAsync(new PackingCygestType(_loading, _fileInfo));

                    }

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

            _addBtn = new Button()
            {
                Text = BtnAdd,
                VerticalOptions = LayoutOptions.EndAndExpand
                
             };

            
            _addBtn.SetBinding(Button.CommandProperty, new Binding("AddBtn"));
            _addBtn.SetDynamicResource(Button.TextColorProperty, "ComponentBackgroundColor");
            _addBtn.SetDynamicResource(Button.BackgroundColorProperty, "RedTextColor");
            MainLayout.Children.Add(_scroll);
            MainLayout.Children.Add(_addBtn);
            // MainLayout.Children.Insert(1, stack);

            Content = MainLayout;
        }


    }


}
