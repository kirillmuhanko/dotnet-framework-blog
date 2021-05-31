using System;
using System.Text;
using System.Web.Mvc;
using BlogApplication.Models.Constants.Controllers;
using Newtonsoft.Json;

namespace BlogApplication.Shared.ActionResults
{
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult()
        {
            Formatting = Formatting.None;
            SerializerSettings = new JsonSerializerSettings();
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        private Formatting Formatting { get; }

        private JsonSerializerSettings SerializerSettings { get; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var httpMethod = context.HttpContext.Request.HttpMethod;

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet)
                if (string.Equals(httpMethod, RequestTypes.Get, StringComparison.OrdinalIgnoreCase))
                {
                    var stringBuilder = new StringBuilder()
                        .Append("This request has been blocked because sensitive information ")
                        .Append("could be disclosed to third party web sites when this is used in a GET request. ")
                        .Append("To allow GET requests, set JsonRequestBehavior to AllowGet.");

                    throw new InvalidOperationException(stringBuilder.ToString());
                }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? ContentTypes.ApplicationJson : ContentType;

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null) return;
            var writer = new JsonTextWriter(response.Output) {Formatting = Formatting};
            var serializer = JsonSerializer.Create(SerializerSettings);
            serializer.Serialize(writer, Data);
            writer.Flush();
        }
    }
}