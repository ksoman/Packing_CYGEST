using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
   public class ListViewRubricModel
    {
        /// <summary>
        /// Gets or sets the idrubric.
        /// </summary>
        /// <value>
        /// The idrubric.
        /// </value>
        public int Idrubric { get; set; }
        /// <summary>
        /// Gets or sets the count rubric.
        /// </summary>
        /// <value>
        /// The count rubric.
        /// </value>
        public int CountRubric { get; set; }
        /// <summary>
        /// Gets or sets the count already scanned rubric.
        /// </summary>
        /// <value>
        /// The count already scanned rubric.
        /// </value>
        public int CountAlreadyScannedRubric { get; set; }
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
        public string ImageSVGPath { set; get; }
    }
}
