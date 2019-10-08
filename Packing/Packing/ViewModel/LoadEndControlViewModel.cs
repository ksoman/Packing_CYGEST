using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    public class LoadEndControlViewModel : BaseViewModel
    {

        private string _goToSignatureTxt;

        private DatabaseAccess _db;

        private  NewLoadingModel _newLoading;

        private FileInfoModel _fileInfo; 

        private string _numberOfItem;

        private string _missingBarcode;

        private string _doubleBarcode;

        private string _lblNumberOfItem;

        private string _lblMissingBarcode;

        private string _lblDoubleBarcode;

        private string _languageSelected;

        private string _lblLoadEndControlTitle;

        private ObservableCollection <EndControlTotals> _itemList;

        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        private string _lblEndControlNext;

        public string LblNumberOfItem
        {
            get { return _lblNumberOfItem; }
            set { SetValue(ref _lblNumberOfItem, value); }
        }

        public string LblMissingBarcode
        {
            get { return _lblMissingBarcode; }
            set { SetValue(ref _lblMissingBarcode, value); }
        }

        public string LblDoubleBarcode
        {
            get { return _lblDoubleBarcode; }
            set { SetValue(ref _lblDoubleBarcode, value); }
        }


        /// <summary>
        /// Gets or sets the number of item.
        /// </summary>
        /// <value>
        /// The number of item.
        /// </value>
        public string NumberOfItem
        {
            get {return _numberOfItem;       }
            set {SetValue(ref _numberOfItem, value);  }          
        }

        /// <summary>
        /// Gets or sets the missing barcode.
        /// </summary>
        /// <value>
        /// The missing barcode.
        /// </value>
        public string MissingBarcode
        {
            get { return _missingBarcode; }
            set { SetValue(ref _missingBarcode, value); }         
        }
        /// <summary>
        /// Gets or sets the double barcode.
        /// </summary>
        /// <value>
        /// The double barcode.
        /// </value>
        public string DoubleBarcode
        {
            get { return _doubleBarcode; }
            set { SetValue(ref _doubleBarcode, value); }          
        }

    
        /// <summary>
        /// Gets or sets the go to signature text.
        /// </summary>
        /// <value>
        /// The go to signature text.
        /// </value>
        public string GoToSignatureTxt
        {
            get { return _goToSignatureTxt; }
            set { SetValue(ref _goToSignatureTxt, value); }
         
        }
        public ObservableCollection<EndControlTotals> ItemList
        {
            get { return _itemList; }
            set { SetValue(ref _itemList, value); }           
        }

        public string LblEndControlNext
        {
            get { return _lblEndControlNext; }
            set { SetValue(ref _lblEndControlNext, value); }
        }

        public string LblLoadEndControlTitle
        {
            get { return _lblLoadEndControlTitle; }
            set { SetValue(ref _lblLoadEndControlTitle, value); }        
        }

        /// <summary>
        /// Gets or sets the go to signature.
        /// </summary>
        /// <value>
        /// The go to signature.
        /// </value>
        public ICommand GoToSignature { get; set; }

        public IPageService PageService { get; set; }
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string SvgPathCouch => "PackingCygest.Image.Couch.svg";
        public string SvgPathWashing => "PackingCygest.Image.Washing_machine.svg";
        public string SvgPathSmallArrow => "PackingCygest.Image.Small_arrow.svg";

        public NewLoadingModel NewLoading
        {
            get { return _newLoading; }
            set { _newLoading = value; }
        }

        private ObservableCollection<LoadingEndControlModel> _controlItem;
        public ObservableCollection<LoadingEndControlModel> ControlItem
        {
            get { return _controlItem; }
            set { SetValue(ref _controlItem, value); }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="LoadEndControlViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        public LoadEndControlViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo)
        {
            _languageSelected = _appViewModel.DefaultedCultureInfo;
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _db = new DatabaseAccess();
            PageService = pageService;
            NewLoading = loading;
            _fileInfo = fileInfo;
            GetEndControl(NewLoading);
            GoToSignatureTxt = "Next";
            GoToSignature  = new Command(GoToSignatureScreenAsync);
            _db. EmptyDatabaseForLoadSynchro();
        }
        public void GoBack()
        {
           // PageService.Remove();
            PageService.PopAsync();
        }
        /// <summary>
        /// Goes to signature.
        /// </summary>
        private async void GoToSignatureScreenAsync()
        {

            await PageService.PushAsync(new Signature(_fileInfo,"Loading"));

        }
        /// <summary>
        /// Gets the end control.
        /// </summary>
        /// <param name="newLoading">The new loading.</param>
        private void GetEndControl(NewLoadingModel newLoading)
        {
            if (newLoading != null && newLoading.FileItemModel != null && newLoading.FileItemModel.File_Number != null)
            {
                List<EndControlsLoadingModel> endControl ;

                endControl =  _db.GetEndControlLoading(newLoading.FileItemModel.File_Number);
                foreach (var controlIten in endControl)
                {
                    NumberOfItem = controlIten.Qt_items.ToString();
                    MissingBarcode = controlIten.Qt_manque.ToString();
                    DoubleBarcode = controlIten.Qt_double.ToString();
                }

                //Populate Listview of Controls information
                ControlItem = new ObservableCollection<LoadingEndControlModel>();

                LoadingEndControlModel numOfItem = new LoadingEndControlModel()
                {
                   Index=0,
                   ControlName= LblNumberOfItem,
                   ControlItemCount= NumberOfItem
                };
                ControlItem.Add(numOfItem);

                LoadingEndControlModel missingBarcode = new LoadingEndControlModel()
                {
                    Index = 1,
                    ControlName = LblMissingBarcode,
                    ControlItemCount = MissingBarcode
                };
                ControlItem.Add(missingBarcode);


                LoadingEndControlModel doubleBarcode = new LoadingEndControlModel()
                {
                    Index = 2,
                    ControlName = LblDoubleBarcode,
                    ControlItemCount = DoubleBarcode
                };
                ControlItem.Add(doubleBarcode);

                GetLoadingSummary(newLoading.FileItemModel.File_Number); 
            }

        }
        /// <summary>
        /// Gets the loading summary.
        /// </summary>
        private void GetLoadingSummary(string fileNumber)
        {        
            
         
            ItemList = new ObservableCollection<EndControlTotals>();
                     
            var rubrics =  _db.GetCountLoadingItem(fileNumber);

            foreach(var item in rubrics)
            {
                var itemlist = new EndControlTotals();
                itemlist.Id = item.Idrubric;
              
                var rubricsDetails= _db.GetSVGPathRubrics(item.Idrubric);
                if (rubricsDetails.Count > 0)
                {
                    if (rubricsDetails[0].ImageSVGPath != null)
                    {
                        itemlist.SvgImage = rubricsDetails[0].ImageSVGPath;
                    }
                    if (_languageSelected.Equals(Constants.FrenchCulture))
                    {
                        itemlist.Name = rubricsDetails[0].Name_fr;
                    }
                    else
                    {
                        itemlist.Name = rubricsDetails[0].Name_en;
                    }
                }
                itemlist.Total = _db.TotalItemPerRubric(item.Idrubric, fileNumber);
                ItemList.Add(itemlist);
            }
            
           

        }

        public void HandleTranslation(string cultureInfo)
        {
            LblNumberOfItem = Localize.GetString("LblNumberOfItems", cultureInfo);
            LblMissingBarcode = Localize.GetString("LblBarcodeMissing", cultureInfo);
            LblDoubleBarcode = Localize.GetString("LblDoubleBarcodes", cultureInfo);
            LblEndControlNext = Localize.GetString("LblEndControlNext", cultureInfo);
            LblLoadEndControlTitle = Localize.GetString("LblLoadEndControlTitle", cultureInfo);
        }


    }
    
}
