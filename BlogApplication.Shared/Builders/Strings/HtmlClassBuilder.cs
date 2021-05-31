using System.Collections.Generic;
using BlogApplication.Models.Constants.StringSymbols;

namespace BlogApplication.Shared.Builders.Strings
{
    public class HtmlClassBuilder
    {
        private readonly IList<string> _htmlClassList;

        private HtmlClassBuilder()
        {
            _htmlClassList = new List<string>();
        }

        public static HtmlClassBuilder Create()
        {
            return new HtmlClassBuilder();
        }

        public HtmlClassBuilder WithHtmlClass(string htmlClass)
        {
            _htmlClassList.Add(htmlClass);
            return this;
        }

        public HtmlClassBuilder WithHtmlClass(string htmlClass, bool condition)
        {
            if (condition) _htmlClassList.Add(htmlClass);
            return this;
        }

        public string Build()
        {
            var str = string.Join(StringSymbols.Space, _htmlClassList);
            return str;
        }
    }
}