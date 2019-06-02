using Newtonsoft.Json;
using System;
using System.IO;
using TuringConfig.Models;
using TuringConfig.Models.Loading;

namespace TuringConfig.Actions
{
    internal class GenerateFile
    {
        /// <summary>
        /// Generate a new config file with the given parameters.
        /// </summary>
        /// <typeparam name="T">The type to serialise to create the file.</typeparam>
        /// <param name="fileSettings">The settings used to create the file.</param>
        /// <param name="generationResult">The result of the generation.</param>
        /// <returns>True/False if the file was able to be generated.</returns>
        internal static bool Generate<T>(FileSettings fileSettings, out GenerationResult generationResult)
        {
            generationResult = new GenerationResult
            {
                Success = false,
                Message = "Generation has not been attempted yet."
            };

            try
            {
                string fileName = Path.GetFileNameWithoutExtension(fileSettings.FileName ?? typeof(T).Name);
                string filePath = Path.Combine(fileSettings.ConfigDirectory, $"{fileName}.json");

                generationResult.FilePath = filePath;

                Formatting formatting = fileSettings.Formatted
                    ? Formatting.Indented
                    : Formatting.None;

                string contents = JsonConvert.SerializeObject((T)Activator.CreateInstance(typeof(T)), formatting);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                File.WriteAllText(filePath, contents);

                generationResult.Success = true;
            }
            catch(Exception ex)
            {
                generationResult.Message = $"There was an error when attempting to create the config file: {ex.Message}";
            }

            return generationResult.Success;
        }
    }
}
