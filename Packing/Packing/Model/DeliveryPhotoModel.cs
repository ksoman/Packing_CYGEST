using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    public class DeliveryPhotoModel
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
        /// Gets or sets the barecode.
        /// </summary>
        /// <value>
        /// The barecode.
        /// </value>
        public string Barecode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is sent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sent; otherwise, <c>false</c>.
        /// </value>
        public bool IsSent { get; set; }
        /// <summary>
        /// Gets or sets the photo path.
        /// </summary>
        /// <value>
        /// The photo path.
        /// </value>
        public string PhotoPath { get; set; }
    }
}
