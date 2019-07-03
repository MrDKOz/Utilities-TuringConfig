

# TuringConfig
TuringConfig handles the creation, saving, and loading of your configuration files using the classes you've already written for your project.

The project is in a very early alpha stage currently, but can be downloaded form NuGet [here](https://www.nuget.org/packages/TuringConfig)

## Usage
### Default Settings
The following code will generate a file in the same directory as your executable with the name "ExampleConfig.json" and populate it using properties from within your class using default values, the JSON written in this example will be formatted:
    
    // A class containing all of your desired config keys.
    public class MyConfiguration
    {
        private bool enabled;
        [Required]
        public bool Enabled { get => enabled; set => enabled = value; }
        [Required, Range(0,10)]
        public int Number { get; set; }
    }

    class Program
    {
        // Define your configuration for access throughout your project.
        private static Config configManager = new Config();

        static void Main(string[] args)
        {
            // Load your configuration file, if the configuration
            // file does not exist, it will be created.
            MyConfiguration config = configManager.Load<MyConfiguration>();
            
            if (config.Enabled)
            {
                Console.WriteLine("The task is enabled.");
            }
            else
            {
                Console.WriteLine("The task is NOT enabled.");
            }
        }
    }

Edit the values as normal throughout the runtime of your application:

    config.Enabled = false;

Write all changes to the JSON file, when you next load the config it will contain the previously edited data:

    configManager.Save(config);

### Custom Settings

The following code will generate a file in the same directory as your executable with the name "defaultSettings.json" and populate it using properties from within your class using default values, the JSON written in this example will not be formatted:

    // Instantiate TuringConfig with custom parameters
    private static Config configManager = new Config(
        "C:\\_ServiceConfig",   // Directory to write the config too
        "defaultSettings.json", // The name of the configuration file
        false                   // Whether the JSON should be indent formatted);

Edit the values as normal throughout the runtime of your application:

    config.Enabled = false;

Write all changes to the JSON file, when you next load the config it will contain the previously edited data:

    configManager.Save(config);


### Class Validation

Validation uses the [DataAnnotations](https://www.nuget.org/packages/System.ComponentModel.Annotations) package to validate the current values of the class. You can validate an instantiated object by doing the following:

    List<ValidationResult> results = new List<ValidationResult>();

    if (!configManager.Validate(config, out results))
    {
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
