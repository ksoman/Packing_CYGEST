using System;
using System.Globalization;
using PackingCygest.Data;
using PackingCygest.Interface;
using PackingCygest.Droid.Implementation;
using PackingCygest.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileMgr))]
namespace PackingCygest.Droid.Implementation
{
   public  class FileMgr : IFileMgr
    {


        /// <summary>
        /// Gets the base64 image string.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        public string GetBase64ImageString(string filename)
        {
            string res = string.Empty;
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(filename);
                res = Convert.ToBase64String(b);
                

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
            return res;
        }


        public void DeleteDownloadedPhoto(string photopath)
        {
            try
            {
                if(!string.IsNullOrEmpty(photopath))
                {
                    System.IO.File.Delete(photopath);
                }

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

        }

    }
}