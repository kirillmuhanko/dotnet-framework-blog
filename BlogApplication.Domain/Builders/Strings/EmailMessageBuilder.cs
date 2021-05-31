using System.Web.Mvc;

namespace BlogApplication.Domain.Builders.Strings
{
    public static class EmailMessageBuilder
    {
        public static string BuildResetYourPasswordText(string url)
        {
            var builder = new TagBuilder("p");
            builder.SetInnerText("Please reset your password by clicking ");
            var actionLink = new TagBuilder("a");
            actionLink.SetInnerText("here.");
            actionLink.MergeAttribute("href", url);
            builder.InnerHtml += actionLink;
            var str = builder.ToString();
            return str;
        }
    }
}