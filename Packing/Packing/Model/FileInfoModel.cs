using SQLite;
using System.Collections.Generic;

namespace PackingCygest.Model
{
    /// <summary>
    /// this class will be used get the files details prior to loading and delivery 
    /// </summary>
    public class FileInfoModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the file number.
        /// </summary>
        /// <value>
        /// The file number.
        /// </value>
        [PrimaryKey]
        public string File_number { get; set; }
        /// <summary>
        /// Gets or sets the client number.
        /// </summary>
        /// <value>
        /// The client number.
        /// </value>
        public string Client_number { get; set; }
        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public string Volume { get; set; }
        /// <summary>
        /// Gets or sets the country loading.
        /// </summary>
        /// <value>
        /// The country loading.
        /// </value>
        public string Country_loading { get; set; }
        /// <summary>
        /// Gets or sets the country delivery.
        /// </summary>
        /// <value>
        /// The country delivery.
        /// </value>
        public string Country_delivery { get; set; }
        /// <summary>
        /// Gets or sets the transportation.
        /// </summary>
        /// <value>
        /// The transportation.
        /// </value>
        public string Transportation { get; set; }
        /// <summary>
        /// Gets or sets the mobile number.
        /// </summary>
        /// <value>
        /// The mobile number.
        /// </value>
        public string Mobile_Number { get; set; }
        /// <summary>
        /// Gets or sets the qty missing.
        /// </summary>
        /// <value>
        /// The qty missing.
        /// </value>
        public int Qty_Missing { get; set; }
        /// <summary>
        /// Gets or sets the qty double.
        /// </summary>
        /// <value>
        /// The qty double.
        /// </value>
        public int Qty_Double { get; set; }
        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        /// <value>
        /// The signature.
        /// </value>
        public string Signature { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [information cubage].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [information cubage]; otherwise, <c>false</c>.
        /// </value>
        public bool Info_Cubage { get; set; }
    }
}
