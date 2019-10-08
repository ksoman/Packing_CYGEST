using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    public class LoadingEndControlModel
    {
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public int Index { get; set; }
        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        /// <value>
        /// The name of the control.
        /// </value>
        public string ControlName { get; set; }
        /// <summary>
        /// Gets or sets the control item count.
        /// </summary>
        /// <value>
        /// The control item count.
        /// </value>
        public string ControlItemCount { get; set; }

    }
}
