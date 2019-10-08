


using Xamarin.Forms;

namespace PackingCygest.Model
{
   public class ListViewItemModel
    {
        //for DelControl

        /// <summary>
        /// Gets or sets the SVG path.
        /// </summary>
        /// <value>
        /// The SVG path.
        /// </value>
        public string SvgPath { get; set; }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public ImageSource x { get; set; }
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets or sets the already scan.
        /// </summary>
        /// <value>
        /// The already scan.
        /// </value>
        public int AlreadyScan { get; set; }

        // for DelItem ListView 
        /// <summary>
        /// Gets or sets the item detail.
        /// </summary>
        /// <value>
        /// The item detail.
        /// </value>
        public string ItemDetail { get; set; }
        /// <summary>
        /// Gets or sets the item detail value.
        /// </summary>
        /// <value>
        /// The item detail value.
        /// </value>
        public string ItemDetailValue { get; set; }
    }
}
