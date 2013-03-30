using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Github.Domain.Model;

namespace Github.Domain.Query
{
    public interface IQuery<TModel> : IQuery where TModel : IGithubModel
    {
        Task<TModel> RequestAsync(Uri prefix);
    }

    public interface IQuery
    {
        UriTemplate Template { get; }

        IDictionary<string, string> Params { get; set; }
    }
}
