using PackingCygest.Model;
using System.Collections.ObjectModel;

namespace PackingCygest.Data
{
    /// <summary>
    /// This class will be removed once the web service is up and running from client side
    /// The class contains data to mimic the flow from web serevice to ensure all bindings and data are passed correctly .
    /// </summary>
    public class DeliveryData
    {

        ObservableCollection<FileItemModel> _fileItemModel;
      
        public ObservableCollection<FileItemModel> PopulateFileItem()
        {
            _fileItemModel = new ObservableCollection<FileItemModel>()
            {
                  new FileItemModel
                  {
                      Barecode = "Carton001",
                
                      IdFileRoom = "Carton",
                      IdRoom = "Kitchen",
                      //Id = 001,
                      IdFileItem = "4545",
                      File_Number = "Books"
                  }
                 
            };
            return _fileItemModel;

        }

    }
}
