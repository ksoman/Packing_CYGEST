
namespace PackingCygest.Interface
{
    /// <summary>
    /// ISQLitePath 
    /// </summary>
    public interface ISqLitePath
    {
        /// <summary>
        /// Gets the sq lite path.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <returns></returns>
        string GetSqLitePath(string dbName);
    }
}
