using System;
using Newtonsoft.Json;

namespace BlogApplication.Models.DomainModels.Article
{
    public class ArticleDomainModel
    {
        [JsonProperty("hasImage")] public bool HasImage { get; set; }

        [JsonProperty("isLiked")] public bool IsLiked { get; set; }

        [JsonProperty("isLikeDeleted")] public bool IsLikeDeleted { get; set; }

        [JsonProperty("addedDateTime")] public DateTime AddedDateTime { get; set; }

        [JsonProperty("modifiedDateTime")] public DateTime ModifiedDateTime { get; set; }

        [JsonProperty("dislikeCount")] public int DislikeCount { get; set; }

        [JsonProperty("likeCount")] public int LikeCount { get; set; }

        [JsonProperty("id")] public long Id { get; set; }

        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("title")] public string Title { get; set; }
    }
}