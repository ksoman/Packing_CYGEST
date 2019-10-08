using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    /// <summary>
    /// Model for Room details specific to file barcode selected 
    /// </summary>
    public class RoomFileModel
    {
        /// <summary>
        /// The identifier file room
        /// </summary>
        public string IdFileRoom { get; set; }

        /// <summary>
        /// The file number
        /// </summary>
        public string File_Number { get; set; }

        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The image SVG
        /// </summary>
        public string ImageSVG { get; set; }

        /// <summary>
        /// Gets or sets the row and column 
        /// Purpose for Display 
        /// </summary>
        /// <value>
        /// The Row and Column values are used to display Items in Grid 
        /// </value>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns { get; set; }

        /// <summary>
        /// Gets or sets the image SVG path from mobile
        /// </summary>
        /// <value>
        /// The image path
        /// </value>
        public string SvgImagePath { get; set; }
    }
}
