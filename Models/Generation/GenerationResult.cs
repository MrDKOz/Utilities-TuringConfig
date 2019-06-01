namespace TuringConfig.Models
{
    public class GenerationResult
    {
        public string FilePath { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool OverwritePerformed { get; set; }
    }
}
