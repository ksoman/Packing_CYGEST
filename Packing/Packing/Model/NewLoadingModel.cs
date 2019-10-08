using System.Collections.Generic;

namespace PackingCygest.Model
{

    /// <summary>
    /// This model wil ued to pass data between Forms to ensure all selected items are mapped and sent
    /// </summary>
    public class NewLoadingModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get; set; }
        /// <summary>
        /// Gets or sets the file item model.
        /// </summary>
        /// <value>
        /// The file item model.
        /// </value>
        public FileItemModel FileItemModel { get; set; }
        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public List<RoomsModel>  Rooms { get; set; }
        /// <summary>
        /// Gets or sets the rubrics.
        /// </summary>
        /// <value>
        /// The rubrics.
        /// </value>
        public List<RubricsModel> Rubrics { get; set; }
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<ItemModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the rooms files.
        /// </summary>
        /// <value>
        /// The rooms files.
        /// </value>
        public List<RoomFileModel> RoomsFiles { get; set; }
        /// <summary>
        /// Gets or sets the items files.
        /// </summary>
        /// <value>
        /// The items files.
        /// </value>
        public List<ItemFileModel> ItemsFiles { get; set; }

        /// <summary>
        /// Gets or sets the item file mapper.
        /// </summary>
        /// <value>
        /// The item file mapper.
        /// </value>
        public ItemFileModel ItemFileMapper { get; set; }
        /// <summary>
        /// Gets or sets the room file mapper.
        /// </summary>
        /// <value>
        /// The room file mapper.
        /// </value>
        public RoomFileModel RoomFileMapper { get; set; }
        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>
        /// The room.
        /// </value>
        public RoomsModel Room { get; set; }
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public ItemModel Item { get; set; }
        /// <summary>
        /// Gets or sets the rubric.
        /// </summary>
        /// <value>
        /// The rubric.
        /// </value>
        public RubricsModel Rubric { get; set; }
        /// <summary>
        /// Gets or sets the selected data.
        /// </summary>
        /// <value>
        /// The selected data.
        /// </value>
        public string SelectedData { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NewLoadingModel"/> is search.
        /// </summary>
        /// <value>
        ///   <c>true</c> if search; otherwise, <c>false</c>.
        /// </value>
        public bool Search { get; set; }
        /// <summary>
        /// Gets or sets the selected barcode.
        /// </summary>
        /// <value>
        /// The selected barcode.
        /// </value>
        public string SelectedBarcode { get; set; }
        /// <summary>
        /// Gets or sets the selected identifier room.
        /// </summary>
        /// <value>
        /// The selected identifier room.
        /// </value>
        public string SelectedIdRoom { get; set; }
        /// <summary>
        /// Gets or sets the selected identifier item.
        /// </summary>
        /// <value>
        /// The selected identifier item.
        /// </value>
        public string SelectedIdItem { get; set; }
        /// <summary>
        /// Gets or sets the selected identifier file item.
        /// </summary>
        /// <value>
        /// The selected identifier file item.
        /// </value>
        public string SelectedIdFileItem{ get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [rubric flow].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rubric flow]; otherwise, <c>false</c>.
        /// </value>
        public bool RubricFlow { get; set; }
    }
}
