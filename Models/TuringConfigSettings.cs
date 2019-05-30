namespace TuringConfig.Models
{
    internal class TuringConfigSettings
    {
        public string OutputDirectory { get; set; }
        public string OutputFileName { get; set; }
        public bool Overwrite { get; set; }
        public bool FormatJsonOutput { get; set; }
    }
}
