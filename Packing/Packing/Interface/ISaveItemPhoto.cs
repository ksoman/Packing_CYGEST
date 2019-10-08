using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackingCygest.Interface
{
    public interface ISaveItemPhoto
    {
        /// <summary>
        /// Base64s to byte.
        /// </summary>
        /// <param name="base64">The base64.</param>
        /// <param name="name">The name.</param>
        /// <param name="folder">The folder.</param>
        /// <returns></returns>
        string Base64ToByte(string base64, string name, string folder);
    }
}
