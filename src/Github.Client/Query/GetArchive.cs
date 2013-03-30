using System;
using System.Net;
using System.Threading.Tasks;
using Github.Domain.Model;

namespace Github.Client.Query
{
    public class GetArchive : QueryBase<Archive>
    {
        public GetArchive() 
            : base("repos/{owner}/{repo}/{format}/{ref}")
        {
            
        }

        public override async Task<Archive> RequestAsync(Uri prefix)
        {
            var bound = GetBoundUri(prefix);
            var client = new WebClient();
            var archive = new Archive {Url = bound.ToString()};
            archive.Data = await client.DownloadDataTaskAsync(archive.Url);
            return archive;
        }
    }
}
