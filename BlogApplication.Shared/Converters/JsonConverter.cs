using BlogApplication.Shared.Interfaces.Converters;
using Newtonsoft.Json;

namespace BlogApplication.Shared.Converters
{
    public class JsonConverter : IJsonConverter
    {
        public string ConvertFrom(object model)
        {
            var json = JsonConvert.SerializeObject(model);
            return json;
        }
    }
}