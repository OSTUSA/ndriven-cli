using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Github.Domain.Model;
using Github.Domain.Query;
using Newtonsoft.Json;

namespace Github.Client.Query
{
    abstract public class QueryBase<TModel> : IQuery<TModel> where TModel : IGithubModel, new()
    {
        public const string UserAgent = "NDrivenCli";

        public UriTemplate Template { get; private set; }

        public IDictionary<string, string> Params { get; set; }

        protected QueryBase(string templateString)
        {
            Template = new UriTemplate(templateString);
        }

        public virtual async Task<TModel> RequestAsync(Uri prefix)
        {
            var bound = GetBoundUri(prefix);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            var json = await client.GetStringAsync(bound.ToString());
            return JsonConvert.DeserializeObject<TModel>(json);
        }

        protected Uri GetBoundUri(Uri prefix)
        {
            if (Params == null) throw new InvalidOperationException("Query Parameters have not been bound");
            try
            {
                return Template.BindByName(prefix, Params);
            }
            catch (ArgumentException e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
