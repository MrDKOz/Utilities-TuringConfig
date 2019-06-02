using Newtonsoft.Json;
using System.IO;
using TuringConfig.Models.Loading;
using TuringConfig.Models.Saving;

namespace TuringConfig.Actions
{
    internal static class SaveFile
    {
        /// <summary>
        /// Saves the given populated object to the configured JSON file.
        /// </summary>
        /// <param name="populatedObjectToSave">A populated class containing data to save.</param>
        /// <param name="fileSettings">The settings to use when saving.</param>
        /// <param name="saveResult">The result of the save.</param>
        /// <returns>True/False depending on the outcome of the file save.</returns>
        internal static bool Save(object populatedObjectToSave, FileSettings fileSettings, out SaveResult saveResult)
        {
            string fileName = fileSettings.FileName ?? populatedObjectToSave.GetType().Name;
            string filePath = Path.Combine(fileSettings.ConfigDirectory, $"{fileName}.json");

            saveResult = new SaveResult
            {
                Success = false,
                FilePath = filePath
            };

            Formatting formatting = fileSettings.Formatted
                ? Formatting.Indented
                : Formatting.None;

            string contents = JsonConvert.SerializeObject(populatedObjectToSave, formatting);

            File.WriteAllText(filePath, contents);

            saveResult.Success = true;
            saveResult.Message = "Successfully updated configuration file.";

            return saveResult.Success;
        }
    }
}
