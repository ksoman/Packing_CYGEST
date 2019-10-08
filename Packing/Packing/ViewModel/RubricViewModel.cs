
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
    /// Rubrics VeiwModel
    /// </summary>
    ///  />
    public class RubricViewModel : BaseViewModel
    {

        #region Private Variables Declaration
        private IPageService _pageSercice;       
        private List<RubricsModel> _rubrics;
        private readonly FileInfoModel _fileInfoModel;
       // private FFImageLoading.Svg.Forms.SvgImageSource _ffImage;
        private NewLoadingModel _newloading;
        private string _lblCategory;     
        private static ObservableCollection<RubricsModel> _listRub;
        private readonly ApplicationViewModel _appViewModel;
        #endregion

        #region Properties
        public string LblCategory
        {
            get { return _lblCategory; }
            set { SetValue(ref _lblCategory, value); }
        }
        public List<RubricsModel> Rubrics { get => _rubrics; set => _rubrics = value; }
        /// <summary>
        /// Gets or sets the back.
        /// </summary>
        /// <value>
        /// The back.
        /// </value>
        public ICommand Back { get; set; }

        /// <summary>
        /// Gets or sets the list rm.
        /// </summary>
        /// <value>
        /// The list rm.
        /// </value>
        public ObservableCollection<RubricsModel> ListRub
        {
            get { return _listRub; }
            set { SetValue(ref _listRub, value); }
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

   
        //public FFImageLoading.Svg.Forms.SvgImageSource FfImage
        //{
        //    get { return _ffImage; }
        //    set { SetValue(ref _ffImage, value); }

        //}
        #endregion

        #region Construstor

        /// <summary>
        /// Initializes a new instance of the <see cref="RubricViewModel"/> class.
        /// </summary>
        public RubricViewModel()
        {
            _appViewModel = new ApplicationViewModel();
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="RubricViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public RubricViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
            _appViewModel = new ApplicationViewModel();
            HandleTranslation(_appViewModel.DefaultedCultureInfo);         
            _newloading = loading;
            PopulateView();
            PageSercice = pageService;
            _fileInfoModel = fileInfo;
            Back = new Command(GoBack);          

        }
        #endregion


        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblCategory = Localize.GetString("LblCategory", cultureInfo);

        }   


        /// <summary>
        /// Populates the view.
        /// </summary>
        public ObservableCollection<RubricsModel>  PopulateView()
        {
            //get rooms from Populaterooms
            //set colum and row positions
            //set the maximum column.
            //loop through the list and create a list of room with respective colums and row numbers

            var tempList = _newloading.Rubrics;
            int col = -1;
            int row = 0;
            int maxColumn = 2;
            List<RubricsModel> tempRm = new List<RubricsModel>();


            for (int i = 0; i < tempList.Count; i++)
            {

                if (col != maxColumn)
                {

                    col += 1;
                    RubricsModel rm = new RubricsModel
                    {
                        Columns = col,
                        Row = row,
                        IdRubric = tempList[i].IdRubric,
                        Name_en = tempList[i].Name_en,
                        Name_fr = tempList[i].Name_fr,
                        ImageSvg = tempList[i].ImageSvg,
                        ImageSVGPath = tempList[i].ImageSVGPath,
                    };
                    tempRm.Add(rm);


                }
                else
                {
                    col = 0;
                    row++;
                    RubricsModel rm = new RubricsModel
                    {
                        Columns = col,
                        Row = row,
                        IdRubric = tempList[i].IdRubric,
                        Name_en = tempList[i].Name_en,
                        Name_fr = tempList[i].Name_fr,
                        ImageSvg = tempList[i].ImageSvg,
                        ImageSVGPath = tempList[i].ImageSVGPath,
                    };
                    tempRm.Add(rm);
                }
            }
            _listRub = new ObservableCollection<RubricsModel>(tempRm);
            return _listRub;

        }


        /// <summary>
        /// Populates the rooms.
        /// </summary>
        /// <returns></returns>
       

        /// <summary>
        /// Goes the back.
        /// </summary>
        private void GoBack()
        {
            ItemSelectionViewModel.ClickedAdd = false;
            PageSercice.PushAsync(new RoomSelection(_newloading,_fileInfoModel));
        }
    }
}

