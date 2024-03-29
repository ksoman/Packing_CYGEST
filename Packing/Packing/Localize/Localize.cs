﻿using System.Globalization;
using System.Reflection;
using System.Resources;

namespace PackingCygests.Localize
{
    public static class Localize
    {

        /// <summary>
        /// Get matching string for equivalent text used in the application 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="cultureDetails">The culture details.</param>
        /// <returns></returns>
        public static string GetString(string key, string cultureDetails)
        {
            CultureInfo cultureInfo = new CultureInfo(cultureDetails);

            ResourceManager temp = new ResourceManager("PackingCygest.Resources.AppResource", typeof(Localize).GetTypeInfo().Assembly);

            string result = temp.GetString(key, cultureInfo);

            return result;
        }
    }
}
