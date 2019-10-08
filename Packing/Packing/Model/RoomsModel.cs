using SQLite;

namespace PackingCygest.Model
{
    /// <summary>
    /// Class for Room Model
    /// </summary>
    public class RoomsModel
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string IdRoom { get; set; }
        /// <summary>
        /// Gets or sets the name of the room in English.
        /// </summary>
        /// <value>
        /// The name of the room in English.
        /// </value>
        public string Name_EN { get; set; }
        /// <summary>
        /// Gets or sets the name of the room in French.
        /// </summary>
        /// <value>
        /// The name of the room in French.
        /// </value>
        public string Name_FR { get; set; }
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
        /// Gets or sets the image SVG.
        /// </summary>
        /// <value>
        /// The image SVG.
        /// </value>
        public string ImageSvg { get; set; }
        /// <summary>
        /// Gets or sets the image SVG path from mobile
        /// </summary>
        /// <value>
        /// The image path
        /// </value>
        public string SvgImagePath { get; set; }

    }
}
