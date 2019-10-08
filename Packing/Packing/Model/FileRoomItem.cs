namespace PackingCygest.Model
{
    public class FileRoomItem
    {
       
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the identifier room.
        /// </summary>
        /// <value>
        /// The identifier room.
        /// </value>
        public int IdRoom { get; set; }
        /// <summary>
        /// Gets or sets the identifier item.
        /// </summary>
        /// <value>
        /// The identifier item.
        /// </value>
        public int IdItem { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the name fr.
        /// </summary>
        /// <value>
        /// The name fr.
        /// </value>
        public string NameFr { get; set; }
        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>
        /// The name en.
        /// </value>
        public string NameEn { get; set; }
        /// <summary>
        /// Gets or sets the item dismounting.
        /// </summary>
        /// <value>
        /// The item dismounting.
        /// </value>
        public string ItemDismounting { get; set; }
        /// <summary>
        /// Gets or sets the item reassembling.
        /// </summary>
        /// <value>
        /// The item reassembling.
        /// </value>
        public string ItemReassembling { get; set; }
    }
}
