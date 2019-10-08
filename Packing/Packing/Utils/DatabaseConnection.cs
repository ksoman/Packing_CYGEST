using PackingCygest.Interface;
using SQLite;
using Xamarin.Forms;

namespace PackingCygest.Utils
{
    public static class DatabaseConnection
    {
        public static SQLiteConnection _con
        { get; set; }

        public static SQLiteAsyncConnection _conAsync
        { get; set; }

        public static SQLiteConnection DbConnection()
        {
            return _con = DependencyService.Get<ISqLiteDb>().GetConnection();
        }

        public static SQLiteAsyncConnection DbConnectionAsync()
        {
            return _conAsync = DependencyService.Get<ISqLiteDb>().GetAsyncConnection();
        }
    }
}
