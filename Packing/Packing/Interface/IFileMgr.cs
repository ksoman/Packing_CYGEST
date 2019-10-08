namespace PackingCygest.Interface
{
    /// <summary>
    /// IfileMaanager for Photo Conversion to 64 bit
    /// </summary>
    public interface IFileMgr
    {

        /// <summary>
        /// Gets the base64 image string.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        string GetBase64ImageString(string filename);
        /// <summary>
        /// Deletes the downloaded photo.
        /// </summary>
        /// <param name="photoPath">The photo path.</param>
        void DeleteDownloadedPhoto(string photoPath);
    }
}
