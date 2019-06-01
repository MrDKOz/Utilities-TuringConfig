using Newtonsoft.Json;
using System.IO;
using TuringConfig.Models.Loading;

namespace TuringConfig.Actions
{
    internal class LoadFile
    {
        /// <summary>
        /// Find, read, and convert the JSON contents to the given type.
        /// </summary>
        /// <typeparam name="T">The type to de-serialise to.</typeparam>
        /// <param name="loadSettings">The configuration settings to obey.</param>
        /// <param name="name">The name of the file to look for.</param>
        /// <returns>A populated class.</returns>
        internal static T FindAndLoad<T>(LoadSettings loadSettings, string name = null)
        {
            string fileName = name ?? typeof(T).Name;
            string filePath = Path.Combine(loadSettings.ConfigDirectory, $"{fileName}.json");

            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            else
            {
                throw new FileNotFoundException($"Requested file '{filePath}' does not exist.");
            }
        }
    }
}
