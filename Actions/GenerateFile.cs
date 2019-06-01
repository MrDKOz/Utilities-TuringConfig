using Newtonsoft.Json;
using System;
using System.IO;
using TuringConfig.Internal;
using TuringConfig.Models;

namespace TuringConfig.Actions
{
    internal class GenerateFile
    {
        /// <summary>
        /// Generate a JSON file, containing the serialisation of the target object.
        /// </summary>
        /// <param name="target">The object to convert to JSON.</param>
        /// <param name="outputPath">The directory to output the file to.</param>
        /// <param name="outputFileName">The name of the file to create.</param>
        /// <param name="formatted">Whether the JSON should be written formatted.</param>
        /// <param name="overwrite">Whether the file should be overwritten if it already exists.</param>
        /// <returns>Details of the generation.</returns>
        internal static bool Generate(object target, string outputPath, string outputFileName, bool formatted, bool overwrite, out GenerationResult generationResult)
        {
            generationResult = new GenerationResult
            {
                Success = false,
                OverwritePerformed = false
            };

            try
            {
                // If no name was defined, use the name of class.
                string filename = Path.GetFileNameWithoutExtension(outputFileName ?? target.GetType().Name);

                // If no output directory was defined, then use the current directory.
                string directory = outputPath ?? Globals.BaseDirectory;
                string filePath = Path.Combine(directory, $"{filename}.json");

                generationResult.FilePath = filePath;

                string json = JsonConvert.SerializeObject(
                    target,
                    formatted
                        ? Formatting.Indented
                        : Formatting.None
                    );

                if (overwrite)
                {  // Overwrite is true, so just do it.
                    generationResult.OverwritePerformed = File.Exists(filePath);

                    generationResult.Success = WriteFile(filePath, json);
                }
                else if (File.Exists(filePath))
                { // Overwrite is false, and file exists so we don't attempt.
                    generationResult.Message = $"Failure. File already exists, and parameter 'Overwrite' is false.";
                }
                else
                { // Overwrite is false, and the file doesn't exist, so write config.
                    generationResult.Success = WriteFile(filePath, json);
                }

                if (generationResult.Success)
                {
                    generationResult.Message = $"Success. File successfully written.";
                }
            }
            catch (Exception ex)
            {
                generationResult.Success = false;
                generationResult.Message = ex.Message;
            }

            return generationResult.Success;
        }

        /// <summary>
        /// Write the file with the given contents.
        /// </summary>
        /// <param name="path">Path to write the file to.</param>
        /// <param name="contents">Contents to write to the file.</param>
        /// <returns>Whether the file was written successfully.</returns>
        private static bool WriteFile(string path, string contents)
        {
            bool result = false;

            try
            {
                File.WriteAllText(path, contents);

                result = true;
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
