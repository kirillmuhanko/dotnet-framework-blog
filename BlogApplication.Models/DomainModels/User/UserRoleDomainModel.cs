using Newtonsoft.Json;

namespace BlogApplication.Models.DomainModels.User
{
    public class UserRoleDomainModel
    {
        [JsonProperty("isChecked")] public bool IsChecked { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }
    }
}