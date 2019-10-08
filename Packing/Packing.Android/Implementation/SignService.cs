using PackingCygest.Interface;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;
using System.IO;

[assembly: Dependency(typeof(SignService))]
namespace PackingCygest.Droid.Implementation
{
    public class SignService : ISignService
    {
        public string Sign()
        {

            //var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            var pathToNewFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/Data/PackingCygest.Android/files/Signature/";
            Directory.CreateDirectory(pathToNewFolder);
            //var folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments)+ "/Signature/";

            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = "MyNewSignature" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            //string filePath = System.IO.Path.Combine(pictures, name);
            string filePath = System.IO.Path.Combine(pathToNewFolder, name);

            return filePath;
        }
   
    }
}