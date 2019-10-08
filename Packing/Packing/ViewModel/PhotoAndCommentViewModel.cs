using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Model;
using PackingCygest.Utils;
using PackingCygest.View;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PackingCygest.ViewModel
{
    public class PhotoAndCommentViewModel : BaseViewModel
    {
        private int _qualityVal=92;
        private readonly ItemDeliveredModel _delivery;
        private readonly FileInfoDeliveryModel _fileInfo;
        public ICommand AddPhoto { get; set; }
        public ICommand SaveLoadingDetails { get; set; }
        public ICommand SaveAndGoBack { get; set; }
        public IPageService PageService { get; set; }
        private static readonly DatabaseAccessAsync _dbAccess = new DatabaseAccessAsync();
        private readonly DatabaseAccess _db;
        public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
        public string SvgPathAddComment => "PackingCygest.Image.Add.svg";
        public string PhotoComment { get; set; }
        private List<string> _photoPathList;
        private string _imagePaths;
        private ObservableCollection<string> PhotoList { get; set; } = new ObservableCollection<string>();
        private ObservableCollection<string> _myCarousel;
        private string _barcode, _fileNum, _photoPath;
        private bool _sent;
        private readonly NewLoadingModel _loading;
        private readonly FileInfoModel _fileInfoLoading;
        private string _status;
        private IPageService _pageService;
        private string _lblPhotoAndComment;
        public string LblPhotoAndComment { get; private set; }
        private string _ImageClickToTakePic;
        private readonly ApplicationViewModel _appViewModel = new ApplicationViewModel();
        public string BtnSave { get; set; }
        public string alertTitleSave { get; set; }
        public string alertMsgSave { get; set; }
        public string lblOK{ get; set; }
        
        public string ImageClickToTakePic
        {
            get { return _ImageClickToTakePic; }
            set { SetValue(ref _ImageClickToTakePic, value); }
        }

        /// <summary>
        /// Gets or sets my carousel.
        /// </summary>
        /// <value>
        /// My carousel.
        /// </value>
        public ObservableCollection<string> MyCarousel
        {

            get { return _myCarousel; }
            set { SetValue(ref _myCarousel, value); }
        }

        private int _position;
        public int Position
        {

            get { return _position; }
            set { SetValue(ref _position, value); }
        }

        private bool _hasActivtyLoad;

        public bool HasActivtyLoad
        {
            get { return _hasActivtyLoad; }
            set { SetValue(ref _hasActivtyLoad, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAndCommentViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="loading">The loading.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="status">The status.</param>
        public PhotoAndCommentViewModel(IPageService pageService, NewLoadingModel loading, FileInfoModel fileInfo, string status)
        {
            PageService = pageService;

            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            _db = new DatabaseAccess();
            _loading = loading;
            _fileInfoLoading = fileInfo;
            _status = status;
            _photoPathList = new List<string>();
            DetermineValues();
            GetPhotoPathFromDb(_status, _loading.FileItemModel.Barecode);
            GetPhotoQualityFromDb();
            AddPhoto = new Command(TakePhoto);
            SaveLoadingDetails = new Command(CallSaveLoadingAsync);
            SaveAndGoBack = new Command(SaveImageAndGoBack);
            CrossMedia.Current.Initialize();

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoAndCommentViewModel"/> class.
        /// </summary>
        /// <param name="pageService">The page service.</param>
        /// <param name="fileInfo">The file information.</param>
        /// <param name="deliveryItem">The delivery item.</param>
        /// <param name="status">The status.</param>
        public PhotoAndCommentViewModel(IPageService pageService, FileInfoDeliveryModel fileInfo, ItemDeliveredModel deliveryItem, string status)
        {
            /**  
             Get Image quality from database at initial stage to set compression ratio

             */
            CrossMedia.Current.Initialize();
            HandleTranslation(_appViewModel.DefaultedCultureInfo);
            PageService = pageService;
            _fileInfo = fileInfo;
            _status = status;
            _db = new DatabaseAccess();
            _photoPathList = new List<string>();
            _delivery = deliveryItem;
            GetPhotoPathFromDb(_status,_delivery.Barecode);
            GetPhotoQualityFromDb();
            DetermineValues();
            AddPhoto = new Command(TakePhoto);
            SaveLoadingDetails = new Command(CallSaveLoadingAsync);
            SaveAndGoBack = new Command(SaveImageAndGoBack);
            TakePhoto();
        }

        /**  
            SetActivityIndicator function has the functionality to enable activity indicator when saving a picture taken in photo Directory of Mobile App ,
            save path in database and retrieving path from db and displaying photo in the carousel

         */
        /// <summary>
        /// Sets the activity indicator.
        /// </summary>
        /// <param name="status">if set to <c>true</c> [status].</param>
        private void SetActivityIndicator(bool status)
        {
            HasActivtyLoad = status;

        }

        /// <summary>
        /// Determines the values.
        /// </summary>
        private void DetermineValues()
        {
            if (_status != null)
            {

                if (_status == "loading")
                {
                    _barcode = _loading.FileItemModel.Barecode;
                    _fileNum = _loading.FileItemModel.File_Number;
                    //PhotoPath = _loading.FileItemModel.PhotoPath;
                    //Sent = _loading.FileItemModel.Sent;

                }
                else if (_status == "delivery")
                {
                    _barcode = _delivery.Barecode;
                    _fileNum = _fileInfo.File_number;
                    //PhotoPath = _delivery.PhotoPath;
                    //Sent = _delivery.Sent;

                }

            }

        }
        private void GetPhotoPathFromDb(string status, string barcode)
        {
            SetActivityIndicator(true);
            if (status == "loading")
            {
                _imagePaths = _db.GetPhotoPathLoading(barcode);
                _loading.FileItemModel.PhotoPath = _imagePaths;
                DisplayImageInCarousel();
               // PhotoComment = _db.GetComments(barcode);
            }
            else
            {
                _photoPathList.Clear();
                string photoPath = _db.GetPhotoPath(barcode);
                _delivery.PhotoPath = photoPath;
                if (!string.IsNullOrEmpty(photoPath))
                {
                    List<string> listPhoto = new List<string>();
                    listPhoto = photoPath.Split(',').ToList();
                    for (int k = 0; k < listPhoto.Count; k++)
                    {
                        _photoPathList.Add(listPhoto[k]);

                    }
                }
                DisplayImageInCarouselForDelivery();
            }

        }

        /// <summary>
        /// Displays the image in carousel.
        /// </summary>
        private void DisplayImageInCarousel()
        {

            if (!string.IsNullOrEmpty(_imagePaths))
            {
                
                _photoPathList = _imagePaths.Split(',').ToList();
                PhotoList = new ObservableCollection<string>(_photoPathList);

            }
            else
            {
                var tempList = new List<string>();

                Image image = new Image
                {
                   // Source = "Click_to_take_pic.png"
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

                PhotoList = new ObservableCollection<string>(tempList);

            }

            MyCarousel = new ObservableCollection<string>(PhotoList);
            SetActivityIndicator(false);
        }


        private void DisplayImageInCarouselForDelivery()
        {

            if (_photoPathList.Count > 0)
            {
                PhotoList = new ObservableCollection<string>(_photoPathList);

            }
            else
            {
                var tempList = new List<string>();

                Image image = new Image
                {
                   // Source = "Click_to_take_pic.png"
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

                PhotoList = new ObservableCollection<string>(tempList);

            }

            MyCarousel = new ObservableCollection<string>(PhotoList);
            SetActivityIndicator(false);
        }



        /// <summary>
        /// Gets the comments.
        /// </summary>
        /// <param name="text">The text.</param>
        public void GetComments(string text)
        {
            PhotoComment = text;
            if (_status.Equals("loading"))
            {
                _loading.FileItemModel.Comments_loading = PhotoComment;
            }
            if (_status.Equals("delivery"))
            {
                _delivery.comments_Delivery = text;

            }
        }
        
            /// <summary>
            /// Takes the photo.
            /// </summary>
            public async void TakePhoto()
             {
            /**  
            Step 1 : Check if Camera is available in mobile phone
            Step 2 :Give a name to photo taken FileNumber + Barecode + timestamp . also remove any space and slash in photo name
            Step 3 :Take Photo And save Image in given Directory and its path in database
            Step 4 :Get Path from Db of corresponding File Number and Barecode to display them in carousel view
             */
            string barcodeNum = "", tempName = "";
            string bindingName = "";
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {

            
                if (_barcode != null && _fileNum != null)
                {
                    barcodeNum = _barcode;
                    var fileNum = _fileNum;
                    bindingName = fileNum + barcodeNum;


                    tempName = bindingName + $"{DateTime.UtcNow}.jpg".Replace(" ", "_");
                    System.Diagnostics.Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> File + Barocde :" + bindingName);

                    if (tempName.Contains("/"))
                    {
                        tempName = tempName.Replace("/", "_");
                    }

                }
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoDirectory",
                    Name = tempName,
                    PhotoSize = PhotoSize.Small,
                    CompressionQuality = _qualityVal

                };
                // Take a photo of the business receipt.

                try
                {
                 
                    var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (file != null)
                    {
                        _photoPathList.Add(file.Path);
                        string photoPathLongString = String.Join(",", _photoPathList);

                        if (_status.Equals("delivery"))
                        {
                            SetActivityIndicator(true);
                            _delivery.PhotoPath = photoPathLongString;
                            _delivery.comments_Delivery = PhotoComment;
                            _delivery.sent_delivery = "False";
                            _delivery.Osds = Constants.Delivery;
                            System.Diagnostics.Debug.WriteLine(file.Path + " >>path of picture taken");
                            SaveDelivery();
                            await Task.Delay(TimeSpan.FromSeconds(4));
                            GetPhotoPathFromDb(_status, _barcode);
                        }
                        else
                        {
                            _loading.FileItemModel.PhotoPath = photoPathLongString;
                            _loading.FileItemModel.Sent_Loading = false;
                            _loading.FileItemModel.Comments_loading = PhotoComment;
                        
                            System.Diagnostics.Debug.WriteLine(file.Path + " LOADING >>>>path of picture taken");
                            SaveLoading();
                            SetActivityIndicator(true);
                            await Task.Delay(TimeSpan.FromSeconds(5));
                            GetPhotoPathFromDb(_status, _barcode);
                            await UpdateLoadingPhotoTable(_loading.FileItemModel.PhotoPath, _loading);
                        }


                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("No picture was taken<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>");
                        return;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("catch" + e);

                }

            }
        }

        /// <summary>
        /// Updates the loading photo table.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <param name="newLoading">The new loading.</param>
        private async Task UpdateLoadingPhotoTable(string imagePath , NewLoadingModel newLoading)
        {
            ItemPhotoModel photoModel = new ItemPhotoModel();
           //tring imageBase64 = DependencyService.Get<IFileMgr>().GetBase64ImageString(imagePath);

            photoModel.Barecode = newLoading.FileItemModel.Barecode;
            photoModel.Comments = newLoading.FileItemModel.Comments_loading;
            photoModel.Photo = newLoading.FileItemModel.PhotoPath;
            photoModel.OSDS = Constants.Loading;
            photoModel.Telephone = newLoading.FileItemModel.Telephone_loading;

           await  _dbAccess.InsertLoadingPhoto(photoModel);

        }

        /// <summary>
        /// Calls the save loading asynchronous.
        /// </summary>
        public async void CallSaveLoadingAsync()
        {
        
            if (_status.Equals("delivery"))
            {
                SaveDelivery();
                await PageService.PushAsync(new DelItem(_fileInfo, _delivery));

            }
            else
            {
                SaveLoading();
                FinishAndSetFieldToNull();
                ItemSelectionViewModel.ClickedAdd = false;
               
                PageService.PushAsync(new ItemSelection(_loading, _fileInfoLoading));

              
            }
              

        }
        /// <summary>
        /// Saves the loading.
        /// </summary>
        public async void SaveLoading()
        {
            if (_status.Equals("loading"))
            {
                _loading.FileItemModel.Comments_loading = PhotoComment;

                if (_loading.Search == false)
                {
                    int count =  _db.GetCountOfPhotoPathLoading(_loading.FileItemModel.Barecode);
                    if (count != 0)
                    {
                        _db.UpdateLoadingData(_loading.FileItemModel);
                    }
                    else
                    {
                        _db.InsertLoading(_loading.FileItemModel);
                    }
                }
               
                if(_loading.Search == true)
                {
                    FileItemModel singleItem = _db.GetSelectedItemDetails(_loading.SelectedBarcode);

                    if (singleItem != null)
                    {
                        if ((_loading.FileItemModel.Barecode).Equals(_loading.SelectedBarcode))
                        {
                            singleItem.Idrubric = _loading.FileItemModel.Idrubric;
                            singleItem.RubricsName = _loading.FileItemModel.RubricsName;                          
                            singleItem.IdItem = _loading.FileItemModel.IdItem;
                            singleItem.ItemName = _loading.FileItemModel.ItemName;
                            singleItem.Name = _loading.FileItemModel.Name;
                            singleItem.Packed_by = _loading.FileItemModel.Packed_by;
                            singleItem.Dismounting_type = _loading.FileItemModel.Dismounting_type;
                            singleItem.Cub_dismount = _loading.FileItemModel.Cub_dismount;
                            singleItem.Mechanical_statement = _loading.FileItemModel.Mechanical_statement;                           
                            singleItem.Reassembling_type = _loading.FileItemModel.Reassembling_type;
                            singleItem.Cub_reassembly = _loading.FileItemModel.Cub_reassembly;
                            singleItem.PhotoPath = _loading.FileItemModel.PhotoPath;
                            singleItem.Comments_loading = _loading.FileItemModel.Comments_loading;
                            singleItem.Telephone_loading = _loading.FileItemModel.Telephone_loading;
                            singleItem.Branche_loading = _loading.FileItemModel.Branche_loading;
                            singleItem.Sent_Loading = _loading.FileItemModel.Sent_Loading;
                            singleItem.PhotoPath = _loading.FileItemModel.PhotoPath; 

                            if (_fileInfoLoading != null && _fileInfoLoading.Info_Cubage && _loading.ItemsFiles != null)
                            {
                                singleItem.IdFileRoom = _loading.SelectedIdRoom;
                                singleItem.RoomName = _loading.SelectedData;
                                singleItem.IdFileItem = _loading.FileItemModel.IdFileItem;
                            }
                            else
                            {
                                singleItem.IdRoom = _loading.SelectedIdRoom;
                                singleItem.RoomName = _loading.SelectedData;                            
                               
                            }
                            _db.UpdateLoadingData(singleItem);
                        }
                        else
                        {
                            _db.DeleteItem(_loading.SelectedBarcode);
                            if(_fileInfoLoading != null && _fileInfoLoading.Info_Cubage && _loading.ItemsFiles != null)
                            {
                                _loading.FileItemModel.IdFileRoom = _loading.SelectedIdRoom;
                                _loading.FileItemModel.RoomName = _loading.SelectedData;

                            }
                            else
                            {
                                _loading.FileItemModel.IdRoom = _loading.SelectedIdRoom;
                                _loading.FileItemModel.RoomName = _loading.SelectedData;
                            }
                            
                            _db.InsertLoading(_loading.FileItemModel);

                        }  

                    }
                    else
                    {   
                        //selected item barcode already deleted  .... NEW barcode scanned ! ... to perform insert ou update on new changed barcode
                        if(!_loading.FileItemModel.Barecode.Equals(_loading.SelectedBarcode))
                        {
                            int count = _db.GetCountOfPhotoPathLoading(_loading.FileItemModel.Barecode);
                            if (count != 0)
                            {
                                _db.UpdateLoadingData(_loading.FileItemModel);
                            }
                            else
                            {
                                _db.InsertLoading(_loading.FileItemModel);
                            }
                        }
                       
                    }
                }

            }
        }

        /// <summary>
        /// Saves the delivery.
        /// </summary>
        public void SaveDelivery()
        {
            _delivery.comments_Delivery = PhotoComment;
            int count =  _db.GetCountOfPhotoPath(_delivery.Barecode);
            if (count != 0)
            {
                _db.UpdateDeliveryData(_delivery);

            }
            else
            {
                _db.InsertDeliveryData(_delivery);

            }
        }

  


        /// <summary>
        /// Gets the photo quality from database.
        /// </summary>
        private  void GetPhotoQualityFromDb()
        {
            /* Getting imageQuality from DB
             Set the CompressionQuality, which is a value from 0 the most compressed all the way to 100, which is no compression. 
             A good setting from testing is around 92. This is only supported in Android & iOS

            */
            string imageQuality = _db.GetPhotoQuality();


            if (!string.IsNullOrEmpty(imageQuality))
            {
                if (imageQuality.Equals("Low"))
                {
                    _qualityVal = 30;
                }
                else if (imageQuality.Equals("Medium"))
                {
                    _qualityVal = 50;
                }
                else
                {
                    _qualityVal = 100;
                }

            }
            else
            {
                _qualityVal = 100;
            }

        }

        /// <summary>
        /// Saves the image and go back.
        /// </summary>
        public async void SaveImageAndGoBack()
        {
           

            if (_status == "delivery")
            {
                SaveDelivery();
                await PageService.PushAsync(new DelItem(_fileInfo, _delivery));

            }
            else
            {
                //set comment to null refreshing comment editor
                // _loading.FileItemModel.Comments_loading = "";
                if ((_loading.FileItemModel.Dismounting_type != null && _loading.FileItemModel.Dismounting_type.Equals("True"))
                || (_loading.FileItemModel.Item_MS != null && _loading.FileItemModel.Item_MS.Equals("True"))
                || (_loading.FileItemModel.Reassembling_type != null && _loading.FileItemModel.Reassembling_type.Equals("True")))
                {
                    await PageService.PushAsync(new LoadingOption(_loading, _fileInfoLoading));

                }
                else
                {
                    await PageService.PushAsync(new PackingCygestType(_loading,_fileInfoLoading));

                }
              //  await PageService.PushAsync(new LoadingOption(_loading, _fileInfoLoading));
            }
            

        }

        /// <summary>
        /// Handles the translation of text found on the configuration form
        /// </summary>
        /// <param name="cultureInfo">The culture information.</param>
        public void HandleTranslation(string cultureInfo)
        {
            
            LblPhotoAndComment = Localize.GetString("LblPhotoAndComment", cultureInfo);
            ImageClickToTakePic = Localize.GetString("ImageClickToTakePic", cultureInfo);
            BtnSave = Localize.GetString("lblSaveSuccessfully", cultureInfo);
            alertTitleSave = Localize.GetString("lblSaveSuccessfully", cultureInfo);
            alertMsgSave = Localize.GetString("PhotoCommentTitle", cultureInfo);
            lblOK = Localize.GetString ("lblOK", cultureInfo);
        }

        private void FinishAndSetFieldToNull()
        {
            _loading.FileItemModel.Barecode = null;
            _loading.FileItemModel.Idrubric = null;
            _loading.FileItemModel.Item_MS = null;
            _loading.FileItemModel.Mechanical_statement = null;
            _loading.FileItemModel.Name = null;
            _loading.FileItemModel.Packed_by = null;
            _loading.FileItemModel.IdFileItem = null;

            _loading.SelectedBarcode = null;
            _loading.SelectedData = null;
            _loading.SelectedIdFileItem = null;
            _loading.SelectedIdItem = null;
            // _loading.SelectedIdRoom = null;
            _loading.FileItemModel.Comments_loading = null;

        }
       
       

    }
}
