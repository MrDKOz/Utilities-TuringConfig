using Newtonsoft.Json;
using System;
using System.IO;
using TuringConfig.Models;
using TuringConfig.Models.Loading;

namespace TuringConfig.Actions
{
    internal class LoadFile
    {
        /// <summary>
        /// Find, read, and convert the JSON contents to the given type.
        /// </summary>
        /// <typeparam name="T">The type to de-serialise to.</typeparam>
        /// <param name="fileSettings">The configuration settings to obey.</param>
        /// <param name="name">The name of the file to look for.</param>
        /// <returns>A populated class.</returns>
        internal static T FindAndLoad<T>(FileSettings fileSettings, out LoadResult loadResult)
        {
            T result = default;

            string fileName = Path.GetFileNameWithoutExtension(fileSettings.FileName ?? typeof(T).Name);
            string filePath = Path.Combine(fileSettings.ConfigDirectory, $"{fileName}.json");

            loadResult = new LoadResult
            {
                Success = false,
                FilePath = filePath
            };

            try
            {
                if (EnsureFileExists<T>(fileSettings, filePath, out string message))
                {
                    string fileContents = File.ReadAllText(filePath);

                    loadResult.Success = true;
                    loadResult.Message = "Successfully loaded config from JSON file.";

                    result = JsonConvert.DeserializeObject<T>(fileContents);
                }
                else
                {
                    loadResult.Success = false;
                    loadResult.Message = message;
                }
            }
            catch (Exception ex)
            {
                loadResult.Message = $"File found, however there was an error de-serialising. Error: {ex.Message}";
            }

            return result;
        }

        /// <summary>
        /// Check if the file exists, if it does not then create it.
        /// </summary>
        /// <typeparam name="T">The type to serialise to create the file.</typeparam>
        /// <param name="fileSettings">The settings used to create the file.</param>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="message">Result of the config generation.</param>
        /// <returns>True/False if the file was able to be generated.</returns>
        private static bool EnsureFileExists<T>(FileSettings fileSettings, string filePath, out string message)
        {
            if (File.Exists(filePath))
            {
                message = "File to load found.";
                return true;
            }

            GenerateFile.Generate<T>(fileSettings, out GenerationResult generationResult);

            if (generationResult.Success)
            {
                message = $"Could not find file, so created file and returned empty object.";
            }
            else
            {
                message = generationResult.Message;
            }

            return generationResult.Success;
        }
    }
}
