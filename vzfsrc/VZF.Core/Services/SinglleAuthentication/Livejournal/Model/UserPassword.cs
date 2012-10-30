using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Livejournal.Model
{
    [JsonObject]
    public class UserPassword
    {
        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }

        public int ver
        {
            get
            {
                return 1;
            }
        }
    }
}
