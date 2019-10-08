using System;
using PackingCygest.Interface;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;
using System.IO;

[assembly: Dependency(typeof(SaveImage))]
namespace PackingCygest.Droid.Implementation
{
    public class SaveImage: ISavePhoto
    {
        /// <summary>
        /// Saves the picture.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string SavePicture(byte[] image)
        {
            throw new NotImplementedException();
        }
        

        public string SavetoSd( string image, string name, string folder)
        /// <summary>
        /// Savetoes the sd.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="name">The name.</param>
        {
            //var pathToNewFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/Data/PackingCygest.Android/files/";
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/Android/Data/PackingCygest.Android/files/" + folder + "/";
            //var sdCardPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath + "/" + folder + "/";
            Directory.CreateDirectory(sdCardPath);
            var filename = Path.Combine(sdCardPath, name + ".svg");
       
            try
            {
                if (string.IsNullOrEmpty(image))
                {
                    image = "<svg\r\n   xmlns:dc=\"http://purl.org/dc/elements/1.1/\"\r\n   xmlns:cc=\"http://creativecommons.org/ns#\"\r\n   xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\"\r\n   xmlns:svg=\"http://www.w3.org/2000/svg\"\r\n   xmlns=\"http://www.w3.org/2000/svg\"\r\n   xmlns:sodipodi=\"http://sodipodi.sourceforge.net/DTD/sodipodi-0.dtd\"\r\n   xmlns:inkscape=\"http://www.inkscape.org/namespaces/inkscape\"\r\n   version=\"1.1\"\r\n   viewBox=\"0 0 16 16\"\r\n   id=\"svg31925\"\r\n   sodipodi:docname=\"divers.svg\"\r\n   inkscape:version=\"0.92.2 (5c3e80d, 2017-08-06)\">\r\n  <metadata\r\n     id=\"metadata31931\">\r\n    <rdf:RDF>\r\n      <cc:Work\r\n         rdf:about=\"\">\r\n        <dc:format>image/svg+xml</dc:format>\r\n        <dc:type\r\n           rdf:resource=\"http://purl.org/dc/dcmitype/StillImage\" />\r\n        <dc:title></dc:title>\r\n      </cc:Work>\r\n    </rdf:RDF>\r\n  </metadata>\r\n  <defs\r\n     id=\"defs31929\" />\r\n  <sodipodi:namedview\r\n     pagecolor=\"#ffffff\"\r\n     bordercolor=\"#666666\"\r\n     borderopacity=\"1\"\r\n     objecttolerance=\"10\"\r\n     gridtolerance=\"10\"\r\n     guidetolerance=\"10\"\r\n     inkscape:pageopacity=\"0\"\r\n     inkscape:pageshadow=\"2\"\r\n     inkscape:window-width=\"1920\"\r\n     inkscape:window-height=\"1017\"\r\n     id=\"namedview31927\"\r\n     showgrid=\"false\"\r\n     inkscape:zoom=\"41.7193\"\r\n     inkscape:cx=\"1.181057\"\r\n     inkscape:cy=\"8.4858537\"\r\n     inkscape:window-x=\"-8\"\r\n     inkscape:window-y=\"-8\"\r\n     inkscape:window-maximized=\"1\"\r\n     inkscape:current-layer=\"svg31925\" />\r\n  <path\r\n     fill=\"#444444\"\r\n     d=\"M9 10h-2c0-2 1.2-2.6 2-3 0.3-0.1 0.5-0.2 0.7-0.4 0.1-0.1 0.3-0.3 0.1-0.7-0.2-0.5-0.8-1-1.7-1-1.4 0-1.6 1.2-1.7 1.5l-2-0.3c0.1-1.1 1-3.2 3.6-3.2 1.6 0 3 0.9 3.6 2.2 0.4 1.1 0.2 2.2-0.6 3-0.4 0.4-0.8 0.6-1.2 0.7-0.6 0.4-0.8 0.2-0.8 1.2z\"\r\n     id=\"path31919\"\r\n     style=\"fill:#bc0100;fill-opacity:1\" />\r\n  <path\r\n     fill=\"#444444\"\r\n     d=\"M8 1c3.9 0 7 3.1 7 7s-3.1 7-7 7-7-3.1-7-7 3.1-7 7-7zM8 0c-4.4 0-8 3.6-8 8s3.6 8 8 8 8-3.6 8-8-3.6-8-8-8v0z\"\r\n     id=\"path31921\"\r\n     style=\"fill:#8a8b8c;fill-opacity:1\" />\r\n  <path\r\n     fill=\"#444444\"\r\n     d=\"M6.9 11h2v2h-2v-2z\"\r\n     id=\"path31923\"\r\n     style=\"fill:#bc0100;fill-opacity:1\" />\r\n</svg>";
                }      
                   System.IO.File.WriteAllText(filename, image);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
            }
            return filename;
        }   

           
        
    }
}