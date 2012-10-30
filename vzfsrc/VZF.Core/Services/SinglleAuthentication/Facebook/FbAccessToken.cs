using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Facebook
{
    [JsonObject]
    public class FbAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
