using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Model
{
    public class ItemsSearchModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier item.
        /// </summary>
        /// <value>
        /// The identifier item.
        /// </value>
        public string IdItem { get; set; }

        /// <summary>
        /// Gets or sets the identifier file item.
        /// </summary>
        /// <value>
        /// The identifier file item.
        /// </value>
        public string IdFileItem { get; set; }

        /// <summary>
        /// Gets or sets the identifier room.
        /// </summary>
        /// <value>
        /// The identifier room.
        /// </value>
        public string IdRoom { get; set; }

        /// <summary>
        /// Gets or sets the identifier file room.
        /// </summary>
        /// <value>
        /// The identifier file room.
        /// </value>
        public string IdFileRoom { get; set; }

        /// <summary>
        /// Gets or sets the SVG image path.
        /// </summary>
        /// <value>
        /// The SVG image path.
        /// </value>
        public string SvgImagePath { get; set; }

        /// <summary>
        /// Gets or sets the barecode.
        /// </summary>
        /// <value>
        /// The barecode.
        /// </value>
        public string Barecode { get; set; }

        /// <summary>
        /// Gets or sets the name of the room.
        /// </summary>
        /// <value>
        /// The name of the room.
        /// </value>
        public string RoomName { get; set; }

        /// <summary>
        /// Gets or sets the idrubric.
        /// </summary>
        /// <value>
        /// The idrubric.
        /// </value>
        public string Idrubric { get; set; }

        /// <summary>
        /// Gets or sets the name of the rubrics.
        /// </summary>
        /// <value>
        /// The name of the rubrics.
        /// </value>
        public string RubricsName { get; set; }

        /// <summary>
        /// Gets or sets the comments loading.
        /// </summary>
        /// <value>
        /// The comments loading.
        /// </value>
        public string Comments_loading { get; set; }
    }
}
