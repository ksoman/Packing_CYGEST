using PackingCygest.Interface;
using PackingCygest.Droid.Implementation;
using SQLite;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(SqLiteDb))]

namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// The class implements the conenction to Isql db 
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.ISqLiteDb" />
    public class SqLiteDb : ISqLiteDb
    {
        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns>
        /// Instance of SQLiteAsycConnection
        /// </returns>
        public SQLiteAsyncConnection GetAsyncConnection()
        {
            
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);          
            var path = Path.Combine(docpath, "PackingCygestAppDB.db3");
            return new SQLiteAsyncConnection(path, SQLiteOpenFlags.SharedCache|SQLiteOpenFlags.Create|SQLiteOpenFlags.ReadWrite);
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>
        /// Instance of SQLiteConnection
        /// </returns>
        public SQLiteConnection GetConnection()
        {
            var docpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);         
            var path = Path.Combine(docpath, "PackingCygestAppDB.db3");
            return new SQLiteConnection(path,SQLiteOpenFlags.SharedCache|SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
        }
    }
}