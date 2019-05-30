using System.IO;

namespace TuringConfig.Internal
{
    public static class Globals
    {
        public static string BaseDirectory
        {
            get
            {
                return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }
    }
}
