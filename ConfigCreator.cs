using TuringConfig.Actions;
using TuringConfig.Models;

namespace TuringConfig
{
    public class ConfigCreator
    {
        private readonly GenerationSettings settings;

        public GenerationResult Details;

        /// <summary>
        /// Use default settings.
        /// </summary>
        public ConfigCreator()
        {
            settings = new GenerationSettings
            {
                OutputDirectory = null,
                OutputFileName = null,
                FormatJsonOutput = true,
                Overwrite = true
            };

            Details = new GenerationResult
            {
                Message = "No creation attempt has been made."
            };
        }

        /// <summary>
        /// Set configuration settings for the generation of the config file.
        /// </summary>
        /// <param name="outputDirectory">The directory to which the file will be placed.</param>
        /// <param name="outputFileName">The name of the with (no extension) to be written.</param>
        /// <param name="formatJsonOutput">Whether the JSON should be indented when written.</param>
        /// <param name="overwrite">Whether the file should be overwritten if it already exists.</param>
        public ConfigCreator(string outputDirectory, string outputFileName, bool formatJsonOutput, bool overwrite)
        {
            settings = new GenerationSettings
            {
                OutputDirectory = outputDirectory,
                OutputFileName = outputFileName,
                FormatJsonOutput = formatJsonOutput,
                Overwrite = overwrite
            };

            Details = new GenerationResult
            {
                Message = "No creation attempt has been made."
            };
        }

        /// <summary>
        /// Uses the given settings to generate a config file.
        /// </summary>
        /// <param name="objectToBuild">The object to serialise and output in JSON.</param>
        /// <returns>Details of the generation.</returns>
        public bool Create(object objectToBuild)
        {
            bool result = GenerateFile.Generate(objectToBuild, settings.OutputDirectory, settings.OutputFileName, settings.FormatJsonOutput, settings.Overwrite, out GenerationResult generationResult);

            Details = generationResult;

            return result;
        }
    }
}
