using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// ItemSelectionViewModel
    /// </summary>
    public class ItemSelectionViewModel : BaseViewModel
    {

        private IPageService _pageSercice;
        public NewLoadingModel Loading { get; set; }
        public FileInfoModel FileInfo { get; set; }
        private string _lblItem;
        private static ObservableCollection<ItemModel> _listItems;
        private static ObservableCollection<ItemFileModel> _listItemsFile;
        private readonly DatabaseAccess _dataAccess = new DatabaseAccess();
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        public static bool ClickedAdd;


        public string LblItem
        {
            get { return _lblItem; }
            set { SetValue(ref _lblItem, value); }
        }
        /// <summary>
        /// Get or Set the list of items
        /// </summary>
        public ObservableCollection<ItemModel> ListItems
        {
            get { return _listItems; }
            set { SetValue(ref _listItems, value); }
        }
        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public ICommand Back { get; set; }

        /// <summary>
        /// Gets or sets the BTN carton.
        /// </summary>
        /// <value>
        /// The BTN carton.
        /// </value>
        /// <summary>
        /// Navigate to Rubrics Page
        /// </summary>
        /// <value>
        /// The Add
        /// </value>
        public ICommand AddBtn { get; set; }
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
        /// Initializes a new instance of the <see cref="ItemSelectionViewModel"/> class.
        /// </summary>
        public ItemSelectionViewModel()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemSelectionViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public ItemSelectionViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
          
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageSercice = pageService;
            Loading = loading;          
            FileInfo = fileInfo;
            AddBtn = new Command(AddButton);
            Back = new Command(GoBack);

        }


        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblItem = Localize.GetString("LblItem", cultureInfo);

        }      

        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {         
            if (FileInfo.Info_Cubage)
            {
                ClickedAdd = false;
                Loading.FileItemModel.IdRoom = null; //loading normal flow for item page
                PageSercice.PushAsync(new RoomSelection(Loading, FileInfo));
            }
            else
            {
                PageSercice.PushAsync(new RubricSelection(Loading, FileInfo));
            }
        }


        public void AddButton()
        {
            if (FileInfo.Info_Cubage)
            {
                ClickedAdd = true;
                Loading.FileItemModel.IdFileItem = "0";
                PageSercice.PushAsync(new RubricSelection(Loading, FileInfo));              
            }

        }
        /// <summary>
        /// Populates the view.
        /// </summary>
        /// <param name="idRubric">The identifier rubric.</param>
        /// <returns></returns>
        public ObservableCollection<ItemModel> PopulateView(string idRubric)
        {
            var templist = _dataAccess.GetAllItemsByIdRub(idRubric);
            int col = -1;
            int row = 0;
            int maxColumn = 2;
            List<ItemModel> tempItems = new List<ItemModel>();
            
            for(int i = 0; i < templist.Count; i++)
            {
                if (col != maxColumn)
                {
                    col += 1;
                    ItemModel im = new ItemModel
                    {
                        Columns = col,
                        Row = row,
                        Id = templist[i].Id,
                        IdItem = templist[i].IdItem,
                        IdRubric = templist[i].IdRubric,
                        ImageSVG = templist[i].ImageSVG,
                        Name_en = templist[i].Name_en,
                        Name_fr = templist[i].Name_fr,
                        SvgImagePath = templist[i].SvgImagePath,
                        mechanical_statement = templist[i].mechanical_statement,
                    };
                    tempItems.Add(im);
                }
                else
                {
                    col = 0;
                    row++;
                    ItemModel im = new ItemModel
                    {
                        Columns = col,
                        Row = row,
                        Id = templist[i].Id,
                        IdItem = templist[i].IdItem,
                        IdRubric = templist[i].IdRubric,
                        ImageSVG = templist[i].ImageSVG,
                        Name_en = templist[i].Name_en,
                        Name_fr = templist[i].Name_fr,
                        SvgImagePath = templist[i].SvgImagePath,
                        mechanical_statement = templist[i].mechanical_statement,
                        mounting_dismounting = templist[i].mounting_dismounting,
                    };
                    tempItems.Add(im);


                }

            }
            _listItems = new ObservableCollection<ItemModel>(tempItems);
            return _listItems;
        }

        /// <summary>
        /// Populates the item file view.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ItemFileModel> PopulateItemFileView(string idFileRoom,bool cub)
        {
            List<ItemFileModel> templist = new List<ItemFileModel>();
            if (cub)
            {
               // remove item already selection from list in select item page for CUB only --> done in query
                 templist = _dataAccess.GetAllItemByRoomFileCub(idFileRoom);

            }
            else
            {
                 templist = _dataAccess.GetAllItemByRoomFile(idFileRoom);

            }
            int col = -1;
            int row = 0;
            int maxColumn = 2;
            List<ItemFileModel> tempItems = new List<ItemFileModel>();

            for (int i = 0; i < templist.Count; i++)
            {
                if (col != maxColumn)
                {
                    col += 1;
                    ItemFileModel im = new ItemFileModel
                    {
                        Columns = col,
                        Row = row,
                        IdFIleItem = templist[i].IdFIleItem,                        
                        IdFileRoom = templist[i].IdFileRoom,
                        ImageSVG = templist[i].ImageSVG,
                        Name = templist[i].Name,
                        Idrubric = templist[i].Idrubric,
                        IdRoom = templist[i].IdRoom,
                        SvgImagePath = templist[i].SvgImagePath,
                        mechanical_statement = templist[i].mechanical_statement,
                         mounting_dismounting = templist[i].mounting_dismounting,
                    };
                    tempItems.Add(im);
                }
                else
                {
                    col = 0;
                    row++;
                    ItemFileModel im = new ItemFileModel
                    {
                        Columns = col,
                        Row = row,
                        IdFIleItem = templist[i].IdFIleItem,
                        IdFileRoom = templist[i].IdFileRoom,
                        ImageSVG = templist[i].ImageSVG,
                        Name = templist[i].Name,
                        Idrubric = templist[i].Idrubric,
                        IdRoom = templist[i].IdRoom,
                        SvgImagePath = templist[i].SvgImagePath,
                        mechanical_statement = templist[i].mechanical_statement,
                        mounting_dismounting = templist[i].mounting_dismounting,
                    };
                    tempItems.Add(im);


                }

            }
            _listItemsFile = new ObservableCollection<ItemFileModel>(tempItems);
            return _listItemsFile;
        }
    }
}
