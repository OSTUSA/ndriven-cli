using Github.Domain.Model;

namespace Github.Client.Query
{
    public class GetTagCollection : QueryBase<TagCollection>
    {
        public GetTagCollection() 
            : base("repos/{owner}/{repo}/tags")
        {
            
        }
    }
}
