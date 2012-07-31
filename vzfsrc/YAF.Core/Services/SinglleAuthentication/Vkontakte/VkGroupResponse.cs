using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Vkontakte
{
    [JsonObject]
    public class VkGroupResponse
    {
        [JsonProperty("response")]
        public List<GroupItem> Response { get; set; }
    }

    public class GroupItem
    {
        [JsonProperty("gid")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("photo_medium")]
        public string MediumPhoto { get; set; }

        [JsonProperty("photo_big")]
        public string BigPhoto { get; set; }

    }
}
