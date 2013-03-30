using System;

namespace Github.Domain.Model
{
    public class Commit : IGithubModel
    {
        public string sha { get; set; }
        public string url { get; set; }
    }
}
