using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Github.Domain.Model;

namespace Github.Client
{
    public class GithubClient
    {
        public Uri Uri { get; protected set; }

        public GithubClient()
        {
            Uri = new Uri("https://api.github.com");
        }

        public async Task<TModel> Query<TModel>(object prams) where TModel : IGithubModel, new()
        {
            var query = GetQuery<TModel>(prams);
            if (query == null)
                throw new QueryNotFoundException(String.Format("Query for {0} not found", typeof(TModel).Name));
            return await query.RequestAsync(Uri);
        }

        public dynamic GetQuery<TModel>(object prams) where TModel : IGithubModel, new()
        {
            var props = prams.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            var templateParams = props.ToDictionary(prop => prop.Name, prop => (string) prop.GetValue(prams));
            return GetQuery<TModel>(templateParams);
        }

        private static dynamic GetQuery<TModel>(Dictionary<string, string> templateParams) where TModel : IGithubModel, new()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes().Where(t => t.Namespace == "Github.Client.Query");
            foreach (object query in from type in types where IsModelMatch(type, typeof (TModel)) select Activator.CreateInstance(type))
            {
                ((dynamic) query).Params = templateParams;
                return query;
            }
            return null;
        }

        private static bool IsModelMatch(Type t, Type model)
        {
            if (t.BaseType == null) return false;
            var genericTypes = t.BaseType.GenericTypeArguments;
            if (genericTypes.Length <= 0) return false;
            return genericTypes.First() == model;
        }
    }
}
