using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    class DelFinalSummaryViewModel : BaseViewModel
    {
        public IPageService PageService { get; set; }
        public ICommand GoToSignature { get; set; }
        public ICommand Back { get; set; }
        public FileInfoDeliveryModel InfoModel { get; }
        public string LanguageSelected;
        private ObservableCollection<ListViewItemModel> _items;
        private int _expectedValue;
        private int _recievedValue;
        private DeliveryEndControls _delEndControl;
        private string _lblDelivery;
       // private string _lblNumItem;
        private string _lblNumItemExpct;
        private List<ListViewRubricModel> _listRubricDetails;
        private DatabaseAccess _db;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        private string _lblNumItemReceived;

        public string LblDelivery
        {
            get { return _lblDelivery; }
            set { SetValue(ref _lblDelivery, value); }
        }
        /*
        public string LblNumItem
        {
            get { return _lblNumItem; }
            set { SetValue(ref _lblNumItem, value); }
        }*/
        public string LblNumItemExpct
        {
            get { return _lblNumItemExpct; }
            set { SetValue(ref _lblNumItemExpct, value); }
        }
        public string LblNumItemReceived
        {
            get { return _lblNumItemReceived; }
            set { SetValue(ref _lblNumItemReceived, value); }
        }

        public ObservableCollection<ListViewItemModel> Items
        {
            get { return _items; }
            set { SetValue(ref _items, value); }
        }

        public int ExpectedValue
        {
            get { return _expectedValue; }
            set { SetValue(ref _expectedValue, value); }
        }
        public int RecievedValue
        {
            get { return _recievedValue; }
            set { SetValue(ref _recievedValue, value); }
        }

        private bool _isSucess;

        private string _lblEndControlNext;

        public string LblEndControlNext
        {
            get { return _lblEndControlNext; }
            set { SetValue(ref _lblEndControlNext, value); }
        }
        

        /// <summary>
        /// Initializes a new instance of the <see cref="DelFinalSummaryViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="info">The information.</param>
        /// <param name="end">The end.</param>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        public DelFinalSummaryViewModel(IPageService pageService, FileInfoDeliveryModel info,DeliveryEndControls end,bool isSuccess)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            LanguageSelected = _appViewModel.DefaultedCultureInfo;
            PageService = pageService;
            InfoModel = info;
            _delEndControl = end;
            _isSucess = isSuccess;
            _db = new DatabaseAccess();
            GetDeliverySummaryDetail(end);
            GetDeliveryItem(InfoModel.File_number);
            GoToSignature = new Command(GoToSignaturePage);
            Back = new Command(BackToControlAsync);
        
        }
        public void HandleTranslation(string cultureInfo)
        {
            LblDelivery = Localize.GetString("LblDelivery", cultureInfo);
          //  LblNumItem = Localize.GetString("LblNumItem", cultureInfo);
            LblNumItemExpct = Localize.GetString("LblNumItemExpct", cultureInfo);
            LblNumItemReceived = Localize.GetString("LblNumItemReceived", cultureInfo);
            LblEndControlNext= Localize.GetString("LblEndControlNext", cultureInfo);
        }

        private async void BackToControlAsync()
        {
            await PageService.PushAsync(new DelControls(InfoModel));
        }

        private async void GoToSignaturePage()
        {
            if (_isSucess)
            {
                _db.EmptyDatabaseForDelSynchro();
            }
            await PageService.PushAsync(new Signature(InfoModel,"Delivery"));

        }

        private async void GetDeliverySummaryDetail(DeliveryEndControls endDel)
        {
            if(!string.IsNullOrEmpty(endDel.file_number))
            {
                    ExpectedValue = endDel.qt_items_attendus;
                    RecievedValue = endDel.qt_items_recus;
            }
            else
            {
                await PageService.DisplayAlert("Info", "No data recieved", "ok");
                ExpectedValue = 0;
                RecievedValue = 0;
            }
        }

        /// <summary>
        /// Gets the delivery item.
        /// </summary>
        private void GetDeliveryItem(string filenumber)
        {
            Items = new ObservableCollection<ListViewItemModel>();
            _listRubricDetails = new List<ListViewRubricModel>();
            _listRubricDetails = _db.GetAllRubricDeliverySummaryDetails(filenumber);
            string itemName;
            if (_listRubricDetails.Count > 0)
            {
                for (int i = 0; i < _listRubricDetails.Count; i++)
                {
                    itemName = LanguageSelected.Equals("fr-FR") ? itemName = _listRubricDetails[i].Name_fr : itemName = _listRubricDetails[i].Name_en;
                    ListViewItemModel temp = new ListViewItemModel()
                    {

                        SvgPath = _listRubricDetails[i].ImageSVGPath,
                        ItemName = itemName,
                        Quantity = _listRubricDetails[i].CountRubric
                    };
                    Items.Add(temp);
                }

            }
            else
            {
                Items = new ObservableCollection<ListViewItemModel>();
            }

        }

       
    }
}
