using Packing.Interface;
using Packing.Model;
using Packing.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Packing.ViewModel
{

    public class RoomsPageViewModel :BaseViewModel
    {
        private IPageService pageSercice;
        public ICommand Back { get; set; }
        public ObservableCollection<Rooms> ListRm { get; set; }

        public IPageService PageSercice
        {
            get => pageSercice;
            set => pageSercice = value;
        }

        public RoomsPageViewModel()
        {
            PopulateView();
        }
        public RoomsPageViewModel(IPageService pageService)
        {
            PopulateView();
            this.PageSercice = pageService; 

            //Registration of button.
            Back = new Command(GoBack);
        }

        public void PopulateView()
        {
            ///get rooms from Populaterooms
            ///set colum and row positions
            ///set the maximum column.
            ///loop through the list and create a list of room with respective colums and row numbers

            var tempList = PopulateRooms();
            int col = -1;
            int row = 0;
            int maxColumn = 3;
            List<Rooms> tempRm = new List<Rooms>();

       
            for (int i = 0; i < tempList.Count; i++)
            {
                
                if (col != maxColumn)
                {

                    col += 1;
                    Rooms rm = new Rooms
                    {
                        Column = col.ToString(),
                        Row = row.ToString(),
                        RoomName = tempList[i].RoomName,
                        

                    };
                    tempRm.Add(rm);
                   

                }
                else
                {
                    col = 0;
                    row++;
                    Rooms rm = new Rooms
                    {
                        Column = col.ToString(),
                        Row = row.ToString(),
                        RoomName = tempList[i].RoomName,


                    };
                    tempRm.Add(rm);
                }
            }
            ListRm = new ObservableCollection<Rooms>(tempRm);

        }

        private List<Rooms> PopulateRooms()
        {
            List<Rooms> listRm = new List<Rooms>();
            for (int i = 0; i < 40; i++)
            {
                Rooms rm = new Rooms
                {
                    RoomName = "Button " + i,
                };
                listRm.Add(rm);

            }
            return listRm;

        }

        private void GoBack()
        {
            PageSercice.PopAsync();
        }
    }
}
