using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
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
        internal static T FindAndLoad<T>(LoadSettings loadSettings, out LoadResult loadResult, string name = null)
        {
            T result = default;

            string fileName = Path.GetFileNameWithoutExtension(name ?? typeof(T).Name);
            string filePath = Path.Combine(loadSettings.ConfigDirectory, $"{fileName}.json");

            loadResult = new LoadResult
            {
                Success = false,
                FilePath = filePath
            };

            if (File.Exists(filePath))
            {
                try
                {
                    string fileContents = File.ReadAllText(filePath);

                    loadResult.Success = true;
                    loadResult.Message = "Successfully loaded config from JSON file.";

                    result = JsonConvert.DeserializeObject<T>(fileContents);
                }
                catch (Exception ex)
                {
                    loadResult.Message = $"File found, however there was an error de-serialising. Error: {ex.Message}";
                }
            }
            else
            {
                loadResult.Message = $"Could not find file using name of '{fileName}'";
            }

            return result;
        }
    }
}
