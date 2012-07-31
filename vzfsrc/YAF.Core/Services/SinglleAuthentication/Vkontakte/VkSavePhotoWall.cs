using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Vkontakte
{
    [JsonObject]
    public class VkSavePhotoWallResponse
    {
        [JsonProperty("response")]
        public List<VkSavePhotoWall> Response { get; set; }
    }

    [JsonObject]
    public class VkSavePhotoWall
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("pid")]
        public string Pid { get; set; }

        [JsonProperty("aid")]
        public int Aid { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }

        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("src_big")]
        public string BigSrc { get; set; }

        [JsonProperty("src_small")]
        public string SmallSrc { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }
    }
}