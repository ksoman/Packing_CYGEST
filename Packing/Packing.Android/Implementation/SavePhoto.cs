

using System;
using Android.Content;
using PackingCygest.Data;
using PackingCygest.Droid.Implementation;
using PackingCygest.Interface;
using PackingCygest.Model;
using Xamarin.Forms;
using Uri = Android.Net.Uri;
using System.Globalization;
using Android.Provider;

[assembly: Dependency(typeof(MyPhoto))]

namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// This class implements the I save Photo class
    /// This will used during the Take photo action 
    /// </summary>
    public class MyPhoto
    {

        /// <summary>
        /// Saves the picture.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        public string SavePicture(byte[] image)
        {
            SavePictureToDisk("MySignature", image);
            return "Saved";
        }

        public void CreateCamera()
        {
            Intent camera = new Intent(MediaStore.ActionImageCapture);
            
        }

        /// <summary>
        /// Saves the picture to disk.
        /// The catch part will through error 
        /// The Catch expection will log the error in SQLlite DB
        /// The dump functionality will allow the error to  send by mail 
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="imageData">The image data.</param>
        public void SavePictureToDisk(string filename, byte[] imageData)
        {
            var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            var pictures = dir.AbsolutePath;
            //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
            string name = filename + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
            string filePath = System.IO.Path.Combine(pictures, name);
            
            try
            {
                System.IO.File.WriteAllBytes(filePath, imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
               
                mediaScanIntent.SetData(Uri.FromFile(new Java.IO.File(filePath)));
                Forms.Context.SendBroadcast(mediaScanIntent);
            }
            catch (Exception e)
            {
                DatabaseAccessAsync da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message, StackTrace = e.StackTrace, TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture), MethodName = e.Source
                });
                
            }

        }

       

       
        //public string SavetoSd(string image, string name)
        //{
        //    return "";
        //}

        //public bool HasPermission()
        //{
        //    bool status = false;


        //    return status;
        //}

       
    }
}