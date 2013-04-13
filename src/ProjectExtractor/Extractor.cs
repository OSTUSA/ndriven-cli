using System.IO;

namespace ProjectExtractor
{
    public class Extractor
    {
        public string ZipPath { get; private set; }

        public Extractor(string zipPath)
        {
            if (!File.Exists(zipPath))
                throw new FileNotFoundException("Solution file not found");

            ZipPath = zipPath;
        }
    }
}
