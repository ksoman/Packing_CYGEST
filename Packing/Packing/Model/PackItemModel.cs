using System.Reflection;

namespace PackingCygest.Model
{
    /// <summary>
    /// Pack Item Model
    /// </summary>
    public class PackItemModel
    {
        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        /// <value>
        /// The name of the room.
        /// </value>
        public string RoomName { get; set; }

        public string IdRoom { get; set; }

        public string IdRoomFile { get; set; }

        public string IdRubrics { get; set; }

        public string RubricsName { get; set; }

         public Assembly SvgAssembly => typeof(App).GetTypeInfo().Assembly;
         public string AddSvg => "PackingCygest.Image.Add.svg";
         public string EndSvg => "PackingCygest.Image.end.svg";
         public string BoxSvg => "PackingCygest.Image.box.svg";
         public string FillSvg => "PackingCygest.Image.fill.svg";
         public string BoatSvg => "PackingCygest.Image.boat.svg";
         public string AddUnSelected => "PackingCygest.Image.AddUnselected.svg";
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public string Row { get; set; }
        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        /// <value>
        /// The column.
        /// </value>
        public string Column { get; set; }
        /// <summary>
        /// Gets or sets the bar code.
        /// </summary>
        /// <value>
        /// The bar code.
        /// </value>
        public string BarCode { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        public string IdFileItem { get; set; }

        public string IdItem { get; set; }

        public string Comments_loading { get; set; }
    }
}
