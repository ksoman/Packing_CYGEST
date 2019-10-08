namespace PackingCygest.Model
{
    using SQLite;
    /// <summary>
    /// Rubrics Model to handle the list of rubrics 
    /// </summary>
    public class RubricsModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        ///  
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
        /// Gets or sets the image SVG.
        /// </summary>
        /// <value>
        /// The image SVG.
        /// </value>
        public string ImageSvg { get; set; }
        /// <summary>
        /// Gets or sets the image SVG path.
        /// </summary>
        /// <value>
        /// The image SVG path.
        /// </value>
        public string ImageSVGPath { get; set; }
        /// <summary>
        /// Gets or sets the row and column 
        /// Purpose for Display 
        /// </summary>
        /// <value>
        /// The Row and Column values are used to display Items in Grid 
        /// </value>
        public int Row { get; set; }

        public int Columns { get; set; }
    }
}
