namespace PackingCygest.Interface
{
    /// <summary>
    /// Isave photo is used in the process of saving photo
    /// </summary>
    public interface ISavePhoto
    {
        /// <summary>
        /// Saves the picture.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        string SavePicture(byte[] image);

        /// <summary>
        /// Savetoes the sd.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="name">The name.</param>
        /// <param name="folder"></param>
        string SavetoSd( string image, string name, string folder);
        

    }
}
