using System;
using Github.Domain.Query;

namespace Github.Domain.Model
{
    public class Tag : IGithubModel
    {
        public string name { get; set; }
        public Commit commit { get; set; }
        public string zipball_url { get; set; }
        public string tarball_url { get; set; }
        public Archive zipball
        {
            get { return new Archive {Url = zipball_url}; }
        }
    }
}
