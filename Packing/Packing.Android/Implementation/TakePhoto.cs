using System;
using PackingCygest.Droid.Implementation;
using PackingCygest.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(TakePhoto))]
namespace PackingCygest.Droid.Implementation
{
    /// <summary>
    /// The take photo class will used to set path of folder to store the photo 
    /// The Class implements the I takePhoto class
    /// </summary>
    class TakePhoto : ITakePhoto
    {
        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <returns></returns>
        public string GetPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        }
    }
}