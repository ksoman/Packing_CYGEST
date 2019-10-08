using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Service;
using PackingCygest.Utils;
using PackingCygest.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    class DelItemViewModel : BaseViewModel
    {
        public IPageService PageService { get; set; }
        public ICommand SaveItemAndGoBackToDeliveryControl { get; set; }
        public ICommand Back { get; set; }
        public ICommand TakePhoto { get; set; }
        readonly DatabaseAccess _db;
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string SvgPathTakePhoto => "PackingCygest.Image.Add.svg";
        private readonly ItemDeliveredModel _delivery;
        private readonly FileInfoDeliveryModel _fileInfo;
        private readonly DeliveryService _deliveryService = new DeliveryService();
        private List<string> _photoPathList;
        private bool _hasActivtyLoad;
        private string _imagePaths;
        private string _lblReassembled;
        private string _lblPackingCygest;
        private string _lblReassemble;
        private string _lblHeading;
        private string _ImageClickToTakePic;
        private string _lblMechanicalState;

        public string TxtBarcode { get; set; }
        public string LoadingComment { get; set;}
        public string TxtName { get; set; }
        private string _txtRoom;

        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();

        public string ImageClickToTakePic
        {
            get { return _ImageClickToTakePic; }
            set { SetValue(ref _ImageClickToTakePic, value); }
        }

        public string TxtRoom
        {
            get { return _txtRoom; }
            set { SetValue(ref _txtRoom, value); }

        }

        public string LblHeading
        {
            get { return _lblHeading; }
            set { SetValue(ref _lblHeading, value); }
        }

        public string LblDismounting
        {
            get { return _lblReassembled; }
            set { SetValue(ref _lblReassembled, value); }
        }

        public string LblPackingCygest
        {
            get { return _lblPackingCygest; }
            set { SetValue(ref _lblPackingCygest, value); }
        }

        public string LblReassemble
        {
            get { return _lblReassemble; }
            set { SetValue(ref _lblReassemble, value); }
        }

        public bool HasActivtyLoad
        {
            get { return _hasActivtyLoad; }
            set { SetValue(ref _hasActivtyLoad, value); }
        }

        private int _position;
        public int Position
        {

            get { return _position; }
            set { SetValue(ref _position, value); }
        }

        private ObservableCollection<string>_photoList;

        private ObservableCollection<string> _myCarousel;
        public ObservableCollection<string> MyCarousel
        {
            get { return _myCarousel; }
            set { SetValue(ref _myCarousel, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DelItemViewModel"/> class.
        /// </summary>
        private ObservableCollection<ListViewItemModel> _itemsInformation;
        public ObservableCollection<ListViewItemModel> ItemsInformation
        {
            get { return _itemsInformation; }
            set { SetValue(ref _itemsInformation, value); }
        }

        public string LblMechanicalState
        {
            get { return _lblMechanicalState; }
            set { SetValue(ref _lblMechanicalState, value); }           
        }
        public string TxtComment { get; set; }

        public DelItemViewModel(IPageService pageService, FileInfoDeliveryModel fileInfo, ItemDeliveredModel deliveryItem)
        {
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageService = pageService;
            _delivery = deliveryItem;
            _fileInfo = fileInfo;
            _db = new DatabaseAccess();
            _photoPathList = new List<string>();
            SetValueByBinding(deliveryItem);
            SaveItemAndGoBackToDeliveryControl = new Command(SaveItemAndGoBack);
            Back = new Command(GoBack);
            TakePhoto = new Command(TakeAndSavePhoto);
            GetPhotoPathFromDbForDelivery(_delivery.Barecode);

        }

        /// <summary>
        /// SetValueByBinding field UI with value from paramater of constructor .
        /// </summary>
        /// <param name="deliveryItem">The delivery item.</param>
        private void SetValueByBinding(ItemDeliveredModel deliveryItem)
        {
            TxtBarcode = deliveryItem.Barecode.Substring(deliveryItem.Barecode.Length - 3, 3);
            TxtName = deliveryItem.Name;
            ItemsInformation = new ObservableCollection<ListViewItemModel>();

            var packBy = deliveryItem.packed_by;
            var mountedType = deliveryItem.Dismounting_type;
            var cubReassembly = deliveryItem.Reassembling_type;
            var mechanicalState = deliveryItem.mechanical_statement;
            //var cubReassembly = deliveryItem.cub_Reassembly;

            if (!string.IsNullOrEmpty(mountedType))
            {
                ListViewItemModel mountedTypeModel = new ListViewItemModel()
                {
                    //ItemDetail = "Reassembled"
                    ItemDetail = LblDismounting,
                    ItemDetailValue = mountedType,
                    SvgPath = "",
                    ItemName = "",
                    Quantity = 0,

                };
                ItemsInformation.Add(mountedTypeModel);

            }

            if (!string.IsNullOrEmpty(mechanicalState))
            {
                ListViewItemModel mechanicalModel = new ListViewItemModel()
                {
                    ItemDetail = LblMechanicalState,
                    ItemDetailValue = mechanicalState,
                    SvgPath = "",
                    ItemName = "",
                    Quantity = 0,

                };
                ItemsInformation.Add(mechanicalModel);

            }

            if (!string.IsNullOrEmpty(packBy))
            {
                ListViewItemModel packByTypeModel = new ListViewItemModel()
                {
                    //ItemDetail = "PackingCygest"
                    ItemDetail = LblPackingCygest,
                    ItemDetailValue = packBy,
                    SvgPath = "",
                    ItemName = "",
                    Quantity = 0,

                };
                ItemsInformation.Add(packByTypeModel);

            }

            if (!string.IsNullOrEmpty(cubReassembly))
            {
                ListViewItemModel cubReassemblyModel = new ListViewItemModel()
                {
                    //ItemDetail = "Reassemble"
                    ItemDetail = LblReassemble,
                    ItemDetailValue = cubReassembly,
                    SvgPath = "",
                    ItemName = "",
                    Quantity = 0,

                };
                ItemsInformation.Add(cubReassemblyModel);

            }

            //Display loading photo comment if any
            if (!string.IsNullOrEmpty(deliveryItem.comments_Loading))
            {
                LoadingComment = TxtComment +": "+ deliveryItem.comments_Loading;
            }
            else
            {
                LoadingComment = TxtComment+": ";
            }

            //display if FileRoom
            if (!string.IsNullOrEmpty(deliveryItem.IdFileRoom))
            {
                if (!deliveryItem.IdFileRoom.Equals("0"))
                {
                    TxtRoom = _db.GetFileRoom(deliveryItem.IdFileRoom);

                }

            }

            //display if Room

            if (!string.IsNullOrEmpty(deliveryItem.IdRoom))
            {
                if (!deliveryItem.IdRoom.Equals("0"))
                {
                    if (_appViewModel.DefaultedCultureInfo.Equals("en-US"))
                    {
                        TxtRoom = _db.GetRoomNameEN(deliveryItem.IdRoom);
                    }
                    else
                    {
                        TxtRoom = _db.GetRoomNameFR(deliveryItem.IdRoom);

                    }
                }
               
            }

        }

     

        /// <summary>
        /// DisplayImageInCarousel if string retrieve frrom db is not empty 
        /// picture will be dipslayed in carousel
        /// else generic pic will be shown specifying no pic avaliable
        /// </summary>
        private void DisplayImageInCarousel()
        {
            if (_photoPathList.Count > 0)
            {

                _photoList = new ObservableCollection<string>(_photoPathList);
            }
            else
            {
                var tempList = new List<string>();

                Image image = new Image {
                    //Source = "Click_to_take_pic.png"
                    Source = ImageClickToTakePic
                };

                var source = image.Source as FileImageSource;

                if (source != null)
                {
                    tempList.Add(source.File);

                }
                else
                {
                    //the image wasn't loaded with a FileImageSource, and thus do not have a filename
                }

                _photoList = new ObservableCollection<string>(tempList);

            }

            MyCarousel = new ObservableCollection<string>(_photoList);
            SetActivityIndicator(false);
        }




        /// <summary>
        /// Sets the activity indicator.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void SetActivityIndicator(bool status)
        {
            HasActivtyLoad = status;

        }

        /// <summary>
        /// Takes the and save photo. go to next page photoandcomment to take pic and put comment
        /// </summary>
        public async void TakeAndSavePhoto()
        {
            System.Diagnostics.Debug.WriteLine("TakeAndSavePhoto<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            await PageService.PushAsync(new PhotoAndCommentPage(_fileInfo, _delivery));

        }

        public async  void SaveItemAndGoBack()
        {
            _delivery.sent_delivery = "False";
            _delivery.AlreadyScanned = "True";
            _db.UpdateDeliveryData(_delivery);
            await PageService.PushAsync(new DelControls(_fileInfo));

        }

        public async void GoBack()
        {
            // PageService.PopAsync();
            await PageService.PushAsync(new View.DelControls(_fileInfo));

        }


        /// <summary>
        /// Gets the photo path from database for delivery form 2 table 
        /// 1 from api/ 1 take form users mobile
        /// </summary>
        /// <param name="barcode">The barcode.</param>
        private void GetPhotoPathFromDbForDelivery(string barcode)
        {
            List<DeliveryPhotoModel> deliveryPhotoModels = new List<DeliveryPhotoModel>();
            deliveryPhotoModels = _db.GetPhotoPathForDelivery(barcode);
            _photoPathList.Clear();
            for (int i = 0; i < deliveryPhotoModels.Count; i++)
            {
                _photoPathList.Add(deliveryPhotoModels[i].PhotoPath);

            }

            string paths = _db.GetPhotoPath(barcode);
            if(!string.IsNullOrEmpty(paths))
            {
                List<string> lst = new List<string>();
                lst = paths.Split(',').ToList();
                for (int k = 0; k < lst.Count; k++)
                {
                    _photoPathList.Add(lst[k]);

                }
            }
            DisplayImageInCarousel();
        }


        /// <summary>
        /// Handles the translation.
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            LblDismounting = Localize.GetString("LblDismounting", cultureInfo);
            LblPackingCygest = Localize.GetString("LblPackingCygest", cultureInfo);
            LblReassemble = Localize.GetString("LblReassembling", cultureInfo);
            LblHeading = Localize.GetString("LblHeading", cultureInfo);
            LblMechanicalState = Localize.GetString("LblMechanicalState", cultureInfo);
            ImageClickToTakePic = Localize.GetString("ImageClickToTakePic", cultureInfo);
            TxtComment= Localize.GetString("TxtComment", cultureInfo);

        }



    }
}
