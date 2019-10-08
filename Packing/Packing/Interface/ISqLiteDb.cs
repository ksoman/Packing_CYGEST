using SQLite;

namespace PackingCygest.Interface
{
    /// <summary>
    ///  interface ISqLiteDb
    /// </summary>
    public interface ISqLiteDb
    {
        /// <summary>
        /// Gets the asynchronous connection.
        /// </summary>
        /// <returns>Instance of SQLiteAsycConnection</returns>
        SQLiteAsyncConnection GetAsyncConnection();
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>Instance of SQLiteConnection</returns>
        SQLiteConnection GetConnection();
    }
}
