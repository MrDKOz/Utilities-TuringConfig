namespace TuringConfig.Models
{
    public class ConfigGenerationResult
    {
        public string FilePath { get; set; }
        public string Reason { get; set; }
        public bool Success { get; set; }
        public bool OverwritePerformed { get; set; }
    }
}
