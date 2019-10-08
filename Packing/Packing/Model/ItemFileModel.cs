using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    /// <summary>
    /// The model will mapaped data for file specific items details according to barcode 
    /// </summary>
    public class ItemFileModel 
    {
        /// <summary>
        /// Gets or sets the identifier f ile item.
        /// </summary>
        /// <value>
        /// The identifier f ile item.
        /// </value>
        public string IdFIleItem { get; set; }

        /// <summary>
        /// Gets or sets the identifier file room.
        /// </summary>
        /// <value>
        /// The identifier file room.
        /// </value>
        public string IdFileRoom { get; set; }

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
        public string cub_valued { get; set; }

        /// <summary>
        /// Gets or sets the cub dismount.
        /// </summary>
        /// <value>
        /// The cub dismount.
        /// </value>
        public string cub_dismount { get; set; }

        /// <summary>
        /// Gets or sets the cub reassembly.
        /// </summary>
        /// <value>
        /// The cub reassembly.
        /// </value>
        public string cub_reassembly { get; set; }

        /// <summary>
        /// Gets or sets the cub crate.
        /// </summary>
        /// <value>
        /// The cub crate.
        /// </value>
        public string cub_crate { get; set; }

        /// <summary>
        /// Gets or sets the comments loading.
        /// </summary>
        /// <value>
        /// The comments loading.
        /// </value>
        public string comments_loading { get; set; }

        /// <summary>
        /// Gets or sets the comments delivery.
        /// </summary>
        /// <value>
        /// The comments delivery.
        /// </value>
        public string comments_delivery { get; set; }

        /// <summary>
        /// Gets or sets the sent.
        /// </summary>
        /// <value>
        /// The sent.
        /// </value>
        public string sent { get; set; }

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
        public string  dismounting_type { get; set; }

        /// <summary>
        /// Gets or sets the type of the reassembling.
        /// </summary>
        /// <value>
        /// The type of the reassembling.
        /// </value>
        public string  reassembling_type { get; set; }

        /// <summary>
        /// Gets or sets the type of the mounted.
        /// </summary>
        /// <value>
        /// The type of the mounted.
        /// </value>
        public string mounted_type { get; set; }

        /// <summary>
        /// Gets or sets the mechanical statement.
        /// </summary>
        /// <value>
        /// The mechanical statement.
        /// </value>
        public string mechanical_statement { get; set; }

        public string mounting_dismounting { get; set; }

        /// <summary>
        /// Gets or sets the image SVG.
        /// </summary>
        /// <value>
        /// The image SVG.
        /// </value>
        public string ImageSVG { get; set; }

        /// <summary>
        /// Gets or sets the SvgImagePath from mobile
        /// </summary>
        public string SvgImagePath { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>
        /// The row.
        /// </value>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns { get; set; }
    }
}
