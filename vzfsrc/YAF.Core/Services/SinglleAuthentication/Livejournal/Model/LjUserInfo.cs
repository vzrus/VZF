using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using Newtonsoft.Json;

namespace Social.Livejournal.Model
{
    [JsonObject]
    public class LjUserInfo
    {
        [JsonProperty("userid")]
        public int userid { get; set; }

        [JsonIgnore]
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public string message { get; set; }

        [JsonProperty("fullname")]
        public string fullname { get; set; }

        [JsonIgnore]
        public FriendgroupLj[] friendgroups { get; set; }

        [JsonIgnore]
        public string[] usejournals { get; set; }

    }
}
