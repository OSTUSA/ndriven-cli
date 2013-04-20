using System;
using System.IO;
using System.Threading.Tasks;

namespace Github.Domain.Model
{
    public class Archive : IGithubModel
    {
        public string Url { get; set; }
        public byte[] Data { get; set; }

        public async Task<bool> WriteToFile(string file)
        {
            using (
                var stream = new FileStream(file, FileMode.CreateNew, FileAccess.Write, FileShare.None, bufferSize: 4096,
                                            useAsync: true))
            {
                try
                {
                    await stream.WriteAsync(Data, 0, Data.Length);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
