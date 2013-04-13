using System.IO;
using System.IO.Compression;
using System.Linq;

namespace ProjectExtractor
{
    public class Extractor
    {
        public string ZipPath { get; private set; }

        public Extractor(string zipPath)
        {
            if (!File.Exists(zipPath))
                throw new FileNotFoundException("Project zip file not found");

            ZipPath = zipPath;
        }

        public void Extract(string path)
        {
            ZipFile.ExtractToDirectory(ZipPath, path);
            var di = new DirectoryInfo(path);
            var projDir = di.GetDirectories().First();
            MoveProjectContents(path, projDir);
            projDir.Delete();
        }

        protected void MoveProjectContents(string destination, DirectoryInfo projDir)
        {
            foreach (var file in projDir.GetFiles())
                file.MoveTo(destination + Path.DirectorySeparatorChar + file.Name);
            foreach (var dir in projDir.GetDirectories())
                dir.MoveTo(destination + Path.DirectorySeparatorChar + dir.Name);
        }
    }
}
