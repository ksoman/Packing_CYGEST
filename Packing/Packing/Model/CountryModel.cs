

namespace PackingCygest.Model
{
    /// <summary>
    /// Country model to pull details from web service 
    /// </summary>
    public class CountryModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the iso.
        /// </summary>
        /// <value>
        /// The iso.
        /// </value>
        public string Iso { get; set; }

        /// <summary>
        /// Gets or sets the tel code.
        /// </summary>
        /// <value>
        /// The tel code.
        /// </value>
        public string TelCode { get; set; }
    }
}
