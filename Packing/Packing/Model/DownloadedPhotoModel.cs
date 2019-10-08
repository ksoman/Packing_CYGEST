using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    public class DownloadedPhotoModel
    {
        /// <summary>
        /// Gets or sets the barecode.
        /// </summary>
        /// <value>
        /// The barecode.
        /// </value>
        public string Barecode { get; set; }
        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        
    }
}
