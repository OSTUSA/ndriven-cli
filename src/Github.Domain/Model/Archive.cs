using System.IO;

namespace Github.Domain.Model
{
    public class Archive : IGithubModel
    {
        public string Url { get; set; }
        public byte[] Data { get; set; }

        public void WriteToFile(string file)
        {
            File.WriteAllBytes(file, Data);
        }
    }
}
