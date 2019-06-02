
# TuringConfig
TuringConfig handles the creation, saving, and loading of your configuration files using the classes you've already written for your project.

The project is in a very early alpha stage currently, but can be downloaded form NuGet [here](https://www.nuget.org/packages/TuringConfig)

## Usage
### Default Settings
The following code will generate a file in the same directory as your executable with the name "ExampleConfig.json" and populate it using properties from within your class using default values, the JSON written in this example will be formatted:

    // Instantiate TuringConfig
    public Config taskConfig = new Config();
    
    // Use Turing config to create and populate your class 'ExampleConfig'
    public ExampleConfig settings = myTestConfig.Load<ExampleConfig>();

Edit the values as normal throughout the runtime of your application:

    settings.Enabled = false;

Write all changes to the JSON file, when you next load the config it will contain the previously edited data:

    taskConfig.Save();

### Custom Settings

The following code will generate a file in the same directory as your executable with the name "defaultSettings.json" and populate it using properties from within your class using default values, the JSON written in this example will not be formatted:

    // Instantiate TuringConfig
    public Config taskConfig = new Config(
        "C:\\_ServiceConfig",   // Directory to write the config too
        "defaultSettings.json", // The name of the configuration file
        false                   // Whether the JSON should be indent formatted);
    
    // Use Turing config to create and populate your class 'ExampleConfig'
    public ExampleConfig settings = myTestConfig.Load<ExampleConfig>();

Edit the values as normal throughout the runtime of your application:

    settings.Enabled = false;

Write all changes to the JSON file, when you next load the config it will contain the previously edited data:

    taskConfig.Save();


