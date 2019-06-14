using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TuringConfig.Actions;
using TuringConfig.Internal;
using TuringConfig.Models.Loading;
using TuringConfig.Models.Saving;

namespace TuringConfig
{
    public class Config
    {
        private readonly FileSettings _fileSettings;

        public LoadResult LoadDetails;
        public SaveResult SaveDetails;

        /// <summary>
        /// Initialise the config loader with default values.
        /// </summary>
        public Config()
        {
            _fileSettings = new FileSettings
            {
                ConfigDirectory = Globals.BaseDirectory,
                Formatted = true
            };

            LoadDetails = new LoadResult
            {
                Message = "No load attempt has been made yet."
            };

            SaveDetails = new SaveResult
            {
                Message = "No save attempt has been made yet."
            };
        }

        /// <summary>
        /// Initialise the config loader with custom values.
        /// </summary>
        /// <param name="configDirectory">The directory to look for the config within.</param>
        public Config(string configDirectory, string fileName, bool formattedJsonOutput = true)
        {
            _fileSettings = new FileSettings
            {
                ConfigDirectory = configDirectory,
                FileName = fileName,
                Formatted = formattedJsonOutput
            };

            LoadDetails = new LoadResult
            {
                Message = "No load attempt has been attempted yet."
            };

            SaveDetails = new SaveResult
            {
                Message = "No save attempt has been made yet."
            };
        }

        /// <summary>
        /// Load the config file using the same name as the given class.
        /// </summary>
        /// <typeparam name="T">The type to de-serialise to and return populated from the file if found.</typeparam>
        /// <returns>A populated class from the JSON file.</returns>
        public T Load<T>()
        {
            T result = LoadFile.FindAndLoad<T>(_fileSettings, out LoadResult loadResult);

            LoadDetails = loadResult;

            return result;
        }

        /// <summary>
        /// Object to save.
        /// </summary>
        /// <param name="objectToSave"></param>
        /// <returns>True/False as to whether it was possible to save.</returns>
        public bool Save(object objectToSave)
        {
            bool success = SaveFile.Save(objectToSave, _fileSettings, out SaveResult saveResult);

            SaveDetails = saveResult;

            return success;
        }

        /// <summary>
        /// Validate the given object using the data annotations against the class.
        /// </summary>
        /// <param name="objectToValidate">The populated object to validate.</param>
        /// <param name="validationResults">Any results of the validation that caused a failure.</param>
        /// <returns>True/False as to whether the object meets the requirements.</returns>
        public bool Validate(object objectToValidate, out List<ValidationResult> validationResults)
        {
            ValidationContext context = new ValidationContext(objectToValidate);
            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(objectToValidate, context, validationResults, true);
        }
    }
}
