using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PackingCygest.Interface;
using PackingCygest.Data;
using PackingCygest.Model;
using System.Globalization;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;

[assembly: Dependency(typeof(SaveItemPhoto))]
namespace PackingCygest.Droid.Implementation
{
    public class SaveItemPhoto :ISaveItemPhoto
    {
        public string Base64ToByte(string base64,string name, string folder)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/Data/PackingCygest.Android/files/" + folder + "/";
            //var sdCardPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath + "/" + folder + "/";
            Directory.CreateDirectory(sdCardPath);
            var filename = Path.Combine(sdCardPath, name + ".jpg");

            try
            {
                byte[] b = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes(filename, b);
            }
            catch (Exception e)
            {
                
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });

            }
            return filename;

        }

    }
}