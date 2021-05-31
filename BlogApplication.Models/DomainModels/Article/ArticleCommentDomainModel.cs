using Newtonsoft.Json;

namespace BlogApplication.Models.DomainModels.Article
{
    public class ArticleCommentDomainModel
    {
        [JsonProperty("isEditable")] public bool IsEditable { get; set; }

        [JsonProperty("articleId")] public long ArticleId { set; get; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("userId")] public string UserId { get; set; }

        [JsonProperty("userName")] public string UserName { get; set; }
    }
}