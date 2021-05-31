using System;
using System.Collections.Specialized;
using System.Web;

namespace BlogApplication.Shared.Builders.Strings
{
    public class QueryStringBuilder
    {
        private readonly NameValueCollection _nameValueCollection;
        private readonly UriBuilder _uriBuilder;

        private QueryStringBuilder(string uri)
        {
            _uriBuilder = new UriBuilder(uri);
            _nameValueCollection = HttpUtility.ParseQueryString(_uriBuilder.Query);
        }

        public static QueryStringBuilder Create(string uri)
        {
            var builder = new QueryStringBuilder(uri);
            return builder;
        }

        public QueryStringBuilder AddQuery(string name, string value)
        {
            _nameValueCollection[name] = value;
            return this;
        }

        public string Build()
        {
            _uriBuilder.Query = _nameValueCollection.ToString();
            var uri = _uriBuilder.ToString();
            return uri;
        }
    }
}