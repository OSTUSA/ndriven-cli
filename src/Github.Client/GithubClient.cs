using System;
using System.Linq;
using System.Reflection;
using Github.Domain.Model;
using Github.Domain.Query;

namespace Github.Client
{
    public class GithubClient
    {
        public Uri Uri { get; protected set; }

        public GithubClient()
        {
            Uri = new Uri("https://api.github.com");
        }

        public IGithubModel Query<TQuery>(object prams) where TQuery : IQuery, new()
        {
            dynamic query = GetQuery<TQuery>(prams);
            return query.Request(Uri);
        }

        public TQuery GetQuery<TQuery>(object prams) where TQuery : IQuery, new()
        {
            var props = prams.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            var templateParams = props.ToDictionary(prop => prop.Name, prop => (string) prop.GetValue(prams));
            var query = new TQuery();
            query.Params = templateParams;
            return query;
        }
    }
}
