using SQLite;
using System.Reflection;

namespace PackingCygest.Model
{
    /// <summary>
    /// Item Model calss
    /// </summary>
    public class ItemModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public  string IdItem { get; set; }

        /// <summary>
        /// Gets or sets the identifier rubric.
        /// </summary>
        /// <value>
        /// The identifier rubric.
        /// </value>
        public string IdRubric { get; set; }

        /// <summary>
        /// Gets or sets the name fr.
        /// </summary>
        /// <value>
        /// The name fr.
        /// </value>
        public string Name_fr { get; set; }
        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>
        /// The name en.
        /// </value>
        public string Name_en { get; set; }

        /// <summary>
        /// Gets or sets the image SVG path.
        /// </summary>
        /// <value>
        /// The image SVG path.
        /// </value>
        public string ImageSVG { get; set; }

        /// <summary>
        /// Gets or sets the SvgImagePath from mobile
        /// </summary>
        public string SvgImagePath { get; set; }

        /// <summary>
        /// Gets or sets the mechanical statement.
        /// </summary>
        /// <value>
        /// The mechanical statement.
        /// </value>
        public string mechanical_statement { get; set; }

        public string mounting_dismounting { get; set; }

        /// <summary>
        /// Gets or sets the reassembling statement.
        /// </summary>
        /// <value>
        /// The reassembling statement.
        /// </value>
        public string Reassembling_statement { get; set; }

        /// <summary>
        /// Gets or sets the dismounting statement.
        /// </summary>
        /// <value>
        /// The dismounting statement.
        /// </value>
        public string Dismounting_statement { get; set; }
       // public string mounting_dismounting { get; set; }

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
