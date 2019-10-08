using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace PackingCygest.Model
{
    public class FileItemModel
    {

        //  [ForeignKey(typeof(ItemDeliveredModel))]
        /// <summary>
        /// Gets or sets the delivery identifier.
        /// </summary>
        /// <value>
        /// The delivery identifier.
        /// </value>
        public int DeliveryID { get; set; }

        /// <summary>
        /// Gets or sets the identifier file item.
        /// </summary>
        /// <value>
        /// The identifier file item.
        /// </value>
        public string IdFileItem { get; set; }
        /// <summary>
        /// Gets or sets the identifier file room.
        /// </summary>
        /// <value>
        /// The identifier file room.
        /// </value>
        public string IdFileRoom { get; set; }
        /// <summary>
        /// Gets or sets the identifier item.
        /// </summary>
        /// <value>
        /// The identifier item.
        /// </value>
        public string IdItem { get; set; }
        /// <summary>
        /// Gets or sets the idrubric.
        /// </summary>
        /// <value>
        /// The idrubric.
        /// </value>
        public string Idrubric { get; set; }
        /// <summary>
        /// Gets or sets the identifier room.
        /// </summary>
        /// <value>
        /// The identifier room.
        /// </value>
        public string IdRoom { get; set; }
        /// <summary>
        /// Gets or sets the file number.
        /// </summary>
        /// <value>
        /// The file number.
        /// </value>
        public string File_Number { get; set; }
        /// <summary>
        /// Gets or sets the client number.
        /// </summary>
        /// <value>
        /// The client number.
        /// </value>
        public string Client_Number { get; set; }
        /// <summary>
        /// Gets or sets the barecode.
        /// </summary>
        /// <value>
        /// The barecode.
        /// </value>
        [PrimaryKey]
        public string Barecode { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the cub valued.
        /// </summary>
        /// <value>
        /// The cub valued.
        /// </value>
        public string Cub_valued { get; set; }
        /// <summary>
        /// Gets or sets the cub dismount.
        /// </summary>
        /// <value>
        /// The cub dismount.
        /// </value>
        public string Cub_dismount { get; set; }
        /// <summary>
        /// Gets or sets the cub reassembly.
        /// </summary>
        /// <value>
        /// The cub reassembly.
        /// </value>
        public string Cub_reassembly { get; set; }
        /// <summary>
        /// Gets or sets the cub crate.
        /// </summary>
        /// <value>
        /// The cub crate.
        /// </value>
        public string Cub_crate { get; set; }
        /// <summary>
        /// Gets or sets the comments loading.
        /// </summary>
        /// <value>
        /// The comments loading.
        /// </value>
        public string Comments_loading { get; set; }
        /// <summary>
        /// Gets or sets the comments delivery.
        /// </summary>
        /// <value>
        /// The comments delivery.
        /// </value>
        public string Comments_delivery { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [sent loading].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent loading]; otherwise, <c>false</c>.
        /// </value>
        public bool Sent_Loading { get; set; }
        /// <summary>
        /// Gets or sets the packed by.
        /// </summary>
        /// <value>
        /// The packed by.
        /// </value>
        public string Packed_by { get; set; }
        /// <summary>
        /// Gets or sets the type of the dismounting.
        /// </summary>
        /// <value>
        /// The type of the dismounting.
        /// </value>
        public string Dismounting_type { get; set; }
        /// <summary>
        /// Gets or sets the type of the reassembling.
        /// </summary>
        /// <value>
        /// The type of the reassembling.
        /// </value>
        public string Reassembling_type { get; set; }
        /// <summary>
        /// Gets or sets the item ms.
        /// </summary>
        /// <value>
        /// The item ms.
        /// </value>
        public string Item_MS { get; set; }
        /// <summary>
        /// Gets or sets the image SVG.
        /// </summary>
        /// <value>
        /// The image SVG.
        /// </value>
        public string ImageSVG { get; set; }
        /// <summary>
        /// Gets or sets the telephone loading.
        /// </summary>
        /// <value>
        /// The telephone loading.
        /// </value>
        public string Telephone_loading { get; set; }
        /// <summary>
        /// Gets or sets the branche loading.
        /// </summary>
        /// <value>
        /// The branche loading.
        /// </value>
        public string Branche_loading { get; set; }
        /// <summary>
        /// Gets or sets the telephone delivery.
        /// </summary>
        /// <value>
        /// The telephone delivery.
        /// </value>
        public string Telephone_delivery { get; set; }
        /// <summary>
        /// Gets or sets the branche delivery.
        /// </summary>
        /// <value>
        /// The branche delivery.
        /// </value>
        public string  Branche_delivery { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [sent delivery].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [sent delivery]; otherwise, <c>false</c>.
        /// </value>
        public bool Sent_delivery { get; set; }


        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        /// <value>
        /// The photo path.
        /// </value>
        public string PhotoPath { get; set; }
        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        /// <value>
        /// The name of the room.
        /// </value>
        public string RoomName { get; set; }
        /// <summary>
        /// Gets or sets the name of the rubrics.
        /// </summary>
        /// <value>
        /// The name of the rubrics.
        /// </value>
        public string RubricsName { get; set; }
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ItemName { get; set; }
        /// <summary>
        /// Gets or sets the mechanical statement.
        /// </summary>
        /// <value>
        /// The mechanical statement.
        /// </value>
        public string Mechanical_statement { get; set; }
        public string Mounting_dismounting { get; set; }

    }
}
