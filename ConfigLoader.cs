﻿using TuringConfig.Actions;
using TuringConfig.Internal;
using TuringConfig.Models.Loading;

namespace TuringConfig
{
    public class ConfigLoader
    {
        private readonly LoadSettings _loadSettings = new LoadSettings();

        /// <summary>
        /// Initialise the config loader with default values.
        /// </summary>
        public ConfigLoader()
        {
            _loadSettings = new LoadSettings
            {
                ConfigDirectory = Globals.BaseDirectory
            };
        }

        /// <summary>
        /// Initialise the config loader with custom values.
        /// </summary>
        /// <param name="configDirectory">The directory to look for the config within.</param>
        public ConfigLoader(string configDirectory)
        {
            _loadSettings = new LoadSettings
            {
                ConfigDirectory = configDirectory
            };
        }

        /// <summary>
        /// Load the config file using a custom name for the config.
        /// </summary>
        /// <typeparam name="T">The type to de-serialise to and return populated from the file if found.</typeparam>
        /// <param name="name">The name of the config file if different from the class name.</param>
        /// <returns>A populated class from the JSON file.</returns>
        public T Load<T>(string name)
        {
            return LoadFile.FindAndLoad<T>(_loadSettings, name);
        }

        /// <summary>
        /// Load the config file using the same name as the given class.
        /// </summary>
        /// <typeparam name="T">The type to de-serialise to and return populated from the file if found.</typeparam>
        /// <returns>A populated class from the JSON file.</returns>
        public T Load<T>()
        {
            return LoadFile.FindAndLoad<T>(_loadSettings, null);
        }
    }
}
