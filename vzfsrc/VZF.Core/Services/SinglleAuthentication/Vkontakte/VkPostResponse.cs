using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Vkontakte
{
    [JsonObject]
    public class VkPostResponse
    {
        [JsonProperty("response")]
        public PostItem Response { get; set; }
    }

    [JsonObject]
    public class PostItem
    {
        [JsonProperty("post_id")]
        public string PostID { get; set; }
    }
}
