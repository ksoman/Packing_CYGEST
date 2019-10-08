using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// DelDismountingReassemblingViewModel
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    class LoadingOptionViewModel : BaseViewModel
    {
        public IPageService PageService { get; set; }
        public ICommand KnownCommand { get; set; }
        public ICommand UnKnownCommand { get; set; }

        public ICommand DismountingKnownCommand { get; set; }
        public ICommand DismountingUnKnownCommand { get; set; }
        public ICommand ReAssemblingKnownCommand { get; set; }
        public ICommand ReAssemblingUnKnownCommand { get; set; }
        public ICommand NextCommand { get; set; }
        private readonly DatabaseAccess _db;
        private ImageSource _unKnownImageSource;
        private ImageSource _knownImageSource;
        private ImageSource _dismountingKnownImageSource;
        private ImageSource _dismountingUnKnownImageSource;
        private ImageSource _reAssemblingKnownImageSource;
        private ImageSource _reAssemblingUnKnownImageSource;
        private readonly NewLoadingModel _loading;
        private string _lblSelectedItem;
        private readonly FileInfoModel _fileinfo;
        public ICommand Back { get; set; }

        private string _lblMechanicalState;
        private string _lblDismounting;
        private string _lblReassembling;
        private string _lblKnown;
        private string _lblClient;
        private string _lblUnknown;
        private string _lblMover;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        private bool _visibilityMechanicalState;
        private bool _visibilityDismounting;
        private bool _visibilityReassembling;

        private string _nextButton;
        public string LblMechanicalState
        {
            get { return _lblMechanicalState; }
            set { SetValue(ref _lblMechanicalState, value); }
        }

        public string LblDismounting
        {
            get { return _lblDismounting; }
            set { SetValue(ref _lblDismounting, value); }
        }

        public string LblReassembling
        {
            get { return _lblReassembling; }
            set { SetValue(ref _lblReassembling, value); }
        }

        public string LblKnown
        {
            get { return _lblKnown; }
            set { SetValue(ref _lblKnown, value); }
        }

        public string LblClient
        {
            get { return _lblClient; }
            set { SetValue(ref _lblClient, value); }
        }

        public string LblUnknown
        {
            get { return _lblUnknown; }
            set { SetValue(ref _lblUnknown, value); }
        }

        public string LblMover
        {
            get { return _lblMover; }
            set { SetValue(ref _lblMover, value); }
        }

        /// <summary>
        /// Gets or sets the known image source.
        /// </summary>
        /// <value>
        /// The known image source.
        /// </value>
        public ImageSource KnownImageSource
        {
            get { return _knownImageSource; }
            set { SetValue(ref _knownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the un known image source.
        /// </summary>
        /// <value>
        /// The un known image source.
        /// </value>
        public ImageSource UnKnownImageSource
        {
            get { return _unKnownImageSource; }
            set { SetValue(ref _unKnownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the dismounting known image source.
        /// </summary>
        /// <value>
        /// The dismounting known image source.
        /// </value>
        public ImageSource DismountingKnownImageSource
        {
            get { return _dismountingKnownImageSource; }
            set { SetValue(ref _dismountingKnownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the dismounting un known image source.
        /// </summary>
        /// <value>
        /// The dismounting un known image source.
        /// </value>
        public ImageSource DismountingUnKnownImageSource
        {
            get { return _dismountingUnKnownImageSource; }
            set { SetValue(ref _dismountingUnKnownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the re assembling known image source.
        /// </summary>
        /// <value>
        /// The re assembling known image source.
        /// </value>
        public ImageSource ReAssemblingKnownImageSource
        {
            get { return _reAssemblingKnownImageSource; }
            set { SetValue(ref _reAssemblingKnownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the re assembling un known image source.
        /// </summary>
        /// <value>
        /// The re assembling un known image source.
        /// </value>
        public ImageSource ReAssemblingUnKnownImageSource
        {
            get { return _reAssemblingUnKnownImageSource; }
            set { SetValue(ref _reAssemblingUnKnownImageSource, value); }

        }

        /// <summary>
        /// Gets or sets the label selected item.
        /// </summary>
        /// <value>
        /// The label selected item.
        /// </value>
        public string LblSelectedItem
        {
            get { return _lblSelectedItem; }
            set { SetValue(ref _lblSelectedItem, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visibility mechanical state].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [visibility mechanical state]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibilityMechanicalState
        {
            get { return _visibilityMechanicalState; }
            set { SetValue(ref _visibilityMechanicalState, value); }           
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visibility dismounting].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [visibility dismounting]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibilityDismounting
        {
            get { return _visibilityDismounting; }
            set { SetValue(ref _visibilityDismounting, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether 
        /// [visibility reassembling].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [visibility reassembling]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibilityReassembling
        {
            get { return _visibilityReassembling; }
            set { SetValue(ref _visibilityReassembling, value); }
        }


        /// <summary>
        /// Gets or sets the next button.
        /// </summary>
        /// <value>
        /// The next button.
        /// </value>
        public string NextButton
        {
            get { return _nextButton; }
            set { SetValue(ref _nextButton, value); }
           
        }

      

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingOptionViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadingOptionViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageService = pageService;
            _loading = loading;
            LblSelectedItem = _loading.FileItemModel.Barecode;
            _fileinfo = fileInfo;
            Back = new Command(GoBack);
            _db = new DatabaseAccess();
            SetSelectedOption();
            NextCommand = new Command(GetValues);
            HasMechanicalState(loading);
            HasDismountingProperty(loading);
            HasReAssemblingProperty(loading);

            /* 

             if( !VisibilityReassembling && ! VisibilityMechanicalState && ! VisibilityDismounting  )
              {

                  GetValues();
              }*/
        }

        private void SetSelectedOption()
        {
            if (_loading.Search && _loading.SelectedBarcode != null)
            {
                var newList = (_db.GetItem(_loading.SelectedBarcode));

             /*   if(_loading.FileItemModel.Barecode.Equals(_loading.SelectedBarcode))
                {
                    //same barcode no change
                    _loading.FileItemModel.Comments_loading = newList.Comments_loading;

                }
                else
                {
                    //set comment to null ,not to show previous barcode comment
                    _loading.FileItemModel.Comments_loading = "";

                }
                */
                if (newList != null && newList.Barecode != null)
                {
                    if (newList.Mechanical_statement != null && newList.Mechanical_statement.Equals(Constants.MechanicalStateKnown))
                    {
                        Known();
                    }
                    else
                    {
                        UnKnown();
                    }
                    if (newList.Dismounting_type!=null && newList.Dismounting_type.Equals(Constants.DismountClient))
                    {
                        DismountingKnown();
                    }
                    else
                    {
                        DismountingUnKnown();
                    }
                    if (newList.Reassembling_type!=null && newList.Reassembling_type.Equals(Constants.ReassemblingClient))
                    {
                        ReAssemblingKnown();
                    }
                    else
                    {
                        ReAssemblingUnKnown();
                    }
                }
                else
                {
                    KnownImageSource = ImageSource.FromFile("UnSelected.png");
                    UnKnownImageSource = ImageSource.FromFile("UnSelected.png");
                    DismountingKnownImageSource = ImageSource.FromFile("UnSelected.png");
                    DismountingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
                    ReAssemblingKnownImageSource = ImageSource.FromFile("UnSelected.png");
                    ReAssemblingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
                }
            }
            else
            {
                KnownImageSource = ImageSource.FromFile("UnSelected.png");
                UnKnownImageSource = ImageSource.FromFile("UnSelected.png");
                DismountingKnownImageSource = ImageSource.FromFile("UnSelected.png");
                DismountingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
                ReAssemblingKnownImageSource = ImageSource.FromFile("UnSelected.png");
                ReAssemblingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
            }


            KnownCommand = new Command(Known);
            UnKnownCommand = new Command(UnKnown);

            DismountingKnownCommand = new Command(DismountingKnown);
            DismountingUnKnownCommand = new Command(DismountingUnKnown);

            ReAssemblingKnownCommand = new Command(ReAssemblingKnown);
            ReAssemblingUnKnownCommand = new Command(ReAssemblingUnKnown);

        }

        private bool HasMechanicalState(NewLoadingModel loading)
        {
            VisibilityMechanicalState = false;
            if (loading.FileItemModel != null
                && loading.FileItemModel.Item_MS != null
                && loading.FileItemModel.Item_MS.Equals("True"))
            {
                VisibilityMechanicalState =true;
            }
            else
            {
                VisibilityMechanicalState = false;
            }
            return VisibilityMechanicalState;
        }
        

        /// <summary>
        /// Determines whether [has dismounting property] [the specified loading].
        /// </summary>
        /// <param name="loading">The loading.</param>
        private bool HasDismountingProperty(NewLoadingModel loading)
        {
            if (loading.FileItemModel != null
                && loading.FileItemModel.Mounting_dismounting != null
                && loading.FileItemModel.Mounting_dismounting.Equals("True"))
            {
                VisibilityDismounting = true;
            }
            else
            {
                VisibilityDismounting = false;
            }
            return VisibilityDismounting;
        }
        /// <summary>
        /// Determines whether [has re assembling property] [the specified loading].
        /// </summary>
        /// <param name="loading">The loading.</param>
        private bool  HasReAssemblingProperty(NewLoadingModel loading)
        {
            if (loading.FileItemModel != null
                && loading.FileItemModel.Mounting_dismounting != null
                && loading.FileItemModel.Mounting_dismounting.Equals("True"))
            {
                VisibilityReassembling  = true;
            }
            else
            {
                VisibilityReassembling = false;
            }
            return VisibilityReassembling;
        }


        /// <summary>
        /// Knowns this instance.
        /// </summary>
        public void Known()
        {
            KnownImageSource = ImageSource.FromFile("Selected.png");
            UnKnownImageSource = ImageSource.FromFile("UnSelected.png");
            _loading.FileItemModel.Mechanical_statement = Constants.MechanicalStateKnown;
        }

        /// <summary>
        /// Uns the known.
        /// </summary>
        public void UnKnown()
        {
            KnownImageSource = ImageSource.FromFile("UnSelected.png");
            UnKnownImageSource = ImageSource.FromFile("Selected.png");
            _loading.FileItemModel.Mechanical_statement = Constants.MechanicalStateUnknown;
        }

        /// <summary>
        /// Dismountings the known.
        /// </summary>
        public void DismountingKnown()
        {
            DismountingKnownImageSource = ImageSource.FromFile("Selected.png");
            DismountingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
            _loading.FileItemModel.Dismounting_type = Constants.DismountClient;
            _loading.FileItemModel.Cub_dismount = Constants.DismountClient;
        }

        /// <summary>
        /// Dismountings the un known.
        /// </summary>
        public void DismountingUnKnown()
        {
            DismountingKnownImageSource = ImageSource.FromFile("UnSelected.png");
            DismountingUnKnownImageSource = ImageSource.FromFile("Selected.png");
            _loading.FileItemModel.Dismounting_type = Constants.DismountMover;
            _loading.FileItemModel.Cub_dismount = Constants.DismountMover;
            System.Diagnostics.Debug.WriteLine(">>>>>>  Dismounting State: " + Constants.DismountMover);

        }

        /// <summary>
        /// Res the assembling known.
        /// </summary>
        public void ReAssemblingKnown()
        {
            ReAssemblingKnownImageSource = ImageSource.FromFile("Selected.png");
            ReAssemblingUnKnownImageSource = ImageSource.FromFile("UnSelected.png");
            _loading.FileItemModel.Reassembling_type = Constants.ReassemblingClient;
            _loading.FileItemModel.Cub_reassembly = Constants.ReassemblingClient;
        }

        /// <summary>
        /// Res the assembling un known.
        /// </summary>
        public void ReAssemblingUnKnown()
        {
            ReAssemblingKnownImageSource = ImageSource.FromFile("UnSelected.png");
            ReAssemblingUnKnownImageSource = ImageSource.FromFile("Selected.png");
            _loading.FileItemModel.Reassembling_type = Constants.ReassemblingMover;
            _loading.FileItemModel.Cub_reassembly = Constants.ReassemblingMover;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        public void GetValues()
        {
            if(!string.IsNullOrEmpty(_loading.FileItemModel.Barecode))
            {
                PageService.PushAsync(new PhotoAndCommentPage(_loading, true, _fileinfo));
            }         
        }

        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {
          //  PageService.Remove();
            //  PageService.PopAsync();
            PageService.PushAsync(new PackingCygestType(_loading, _fileinfo));
        }

        public void HandleTranslation(string cultureInfo)
        {
            LblMechanicalState = Localize.GetString("LblMechanicalState", cultureInfo);
            LblDismounting = Localize.GetString("LblDismounting", cultureInfo);
            LblReassembling = Localize.GetString("LblReassembling", cultureInfo);
            LblKnown = Localize.GetString("LblKnown", cultureInfo);
            LblClient = Localize.GetString("LblClient", cultureInfo);
            LblUnknown = Localize.GetString("LblUnknown", cultureInfo);
            LblMover = Localize.GetString("LblMover", cultureInfo);
            NextButton = Localize.GetString("NextButton", cultureInfo);
        }

    }
}
