using System;
using PackingCygest.Interface;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;
using System.IO;

[assembly: Dependency(typeof(SqLitePath))]

namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// This class implements the Get Folder path method
    /// </summary>
    /// <seealso cref="PackingCygest.Interface.ISqLitePath" />
    public class SqLitePath : ISqLitePath
    {
        /// <summary>
        /// Gets the sq lite path.
        /// </summary>
        /// <param name="dbName">Name of the database.</param>
        /// <returns></returns>
        public string GetSqLitePath(string dbName)
        {
            var docpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);         
            Directory.CreateDirectory(docpath);
            var path = Path.Combine(docpath, dbName);

            return path;
        }
    }
}