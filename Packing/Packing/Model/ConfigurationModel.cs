using SQLite;

namespace PackingCygest.Model
{
    /// <summary>
    /// ConfigurationModel
    /// </summary>
    public class ConfigurationModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the branch.
        /// </summary>
        /// <value>
        /// The branch.
        /// </value>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets the branchcode.
        /// </summary>
        /// <value>
        /// The branchcode.
        /// </value>
        public string BranchCode { get; set; }


        /// <summary>
        /// Gets or sets the Tel Code for each country.
        /// </summary>
        /// <value>
        /// The Tel Code.
        /// </value>
        public string TelCode { get; set; }

        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>     
        public string Language { get; set; }
        /// <summary>
        /// Gets or sets the photo quality.
        /// </summary>
        /// <value>
        /// The photo quality.
        /// </value>
        public string PhotoQuality { get; set; }

        /// <summary>
        /// Gets or sets the culture information.
        /// </summary>
        /// <value>
        /// The culture information.
        /// </value>
        public string CultureInfo { get; set; }

    }
}
