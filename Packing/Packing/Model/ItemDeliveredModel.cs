using System;
using System.Collections.ObjectModel;
using SQLite;

namespace PackingCygest.Model
{
    /// <summary>
    /// This class is the mapper for the New Delivery Process 
    /// </summary>
    public class ItemDeliveredModel
    {
        /// <summary>
        /// Gets or sets the identifier file item.
        /// </summary>
        /// <value>
        /// The identifier file item.
        /// </value>
        public string IdFileItem { get; set; }
        // public string IdFIleItem { get; internal set; }
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
        /// Gets or sets the identifier room.
        /// </summary>
        /// <value>
        /// The identifier room.
        /// </value>
        public string IdRoom { get; set; }
        /// <summary>
        /// Gets or sets the idrubric.
        /// </summary>
        /// <value>
        /// The idrubric.
        /// </value>
        public string Idrubric { get; set; }
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
        public string cub_Valued { get; set; }
        /// <summary>
        /// Gets or sets the cub dismount.
        /// </summary>
        /// <value>
        /// The cub dismount.
        /// </value>
        public string cub_Dismount { get; set; }
        /// <summary>
        /// Gets or sets the cub reassembly.
        /// </summary>
        /// <value>
        /// The cub reassembly.
        /// </value>
        public string cub_Reassembly { get; set; }
        /// <summary>
        /// Gets or sets the cub crate.
        /// </summary>
        /// <value>
        /// The cub crate.
        /// </value>
        public string cub_Crate { get; set; }
        /// <summary>
        /// Gets or sets the comments loading.
        /// </summary>
        /// <value>
        /// The comments loading.
        /// </value>
        public string comments_Loading { get; set; }
        /// <summary>
        /// Gets or sets the comments delivery.
        /// </summary>
        /// <value>
        /// The comments delivery.
        /// </value>
        public string comments_Delivery { get; set; }
        /// <summary>
        /// Gets or sets the sent loading.
        /// </summary>
        /// <value>
        /// The sent loading.
        /// </value>
        public string sent_loading { get; set; }
        /// <summary>
        /// Gets or sets the packed by.
        /// </summary>
        /// <value>
        /// The packed by.
        /// </value>
        public string packed_by { get; set; }
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
        /// Gets or sets the mechanical statement.
        /// </summary>
        /// <value>
        /// The mechanical statement.
        /// </value>
        public string mechanical_statement { get; set; }
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
        public string Branche_delivery { get; set; }
        /// <summary>
        /// Gets or sets the sent delivery.
        /// </summary>
        /// <value>
        /// The sent delivery.
        /// </value>
        public string sent_delivery { get; set; }
        /// <summary>
        /// Gets or sets the image SVG path.
        /// </summary>
        /// <value>
        /// The image SVG path.
        /// </value>
        public string ImageSVGPath { get; set; }
        /*
         * Other properties used
         */
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the item pb.
        /// </summary>
        /// <value>
        /// The item pb.
        /// </value>
        public string ItemPb { get; set; }
        /// <summary>
        /// Gets or sets the item ms.
        /// </summary>
        /// <value>
        /// The item ms.
        /// </value>
        public string ItemMs { get; set; }
        /// <summary>
        /// Gets or sets the item dismounting.
        /// </summary>
        /// <value>
        /// The item dismounting.
        /// </value>
        public string ItemDismounting { get; set; }
        /// <summary>
        /// Gets or sets the item reassembling.
        /// </summary>
        /// <value>
        /// The item reassembling.
        /// </value>
        public string ItemReassembling { get; set; }
        /// <summary>
        /// Gets or sets the item comments.
        /// </summary>
        /// <value>
        /// The item comments.
        /// </value>
        public string ItemComments { get; set; }
        /// <summary>
        /// Gets or sets the osds.
        /// </summary>
        /// <value>
        /// The osds.
        /// </value>
        public string Osds { get; set; }
        /// <summary>
        /// Gets or sets the PackingCygest.
        /// </summary>
        /// <value>
        /// The PackingCygest.
        /// </value>
        public string PackingCygest { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ItemDeliveredModel"/> is sent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sent; otherwise, <c>false</c>.
        /// </value>
        public bool Sent { get; set; }
        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        /// <value>
        /// The photo path.
        /// </value>
        public string PhotoPath { get; set; }
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }
        /// <summary>
        /// Gets or sets the already scanned.
        /// </summary>
        /// <value>
        /// The already scanned.
        /// </value>
        public string AlreadyScanned { get; set; }

     /*   public static implicit operator ObservableCollection<object>(ItemDeliveredModel v)
        {
            throw new NotImplementedException();
        }*/
    }
}
