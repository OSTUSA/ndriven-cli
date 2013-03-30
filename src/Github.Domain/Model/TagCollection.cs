using System.Collections.Generic;
using System.Linq;

namespace Github.Domain.Model
{
    public class TagCollection : List<Tag>, IGithubModel
    {
        public Tag GetLatest()
        {
            return this.First();
        }
    }
}
