using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Twitter
{
    [JsonObject]
    public class TwitterAccessToken
    {
        //
        // Summary:
        //     Gets or sets the user ID.
        [JsonProperty("user_id")]
        public decimal UserId { get; set; }
        //
        // Summary:
        //     Gets or sets the token.
        [JsonProperty("token")]
        public string Token { get; set; }
        //
        // Summary:
        //     Gets or sets the token secret.
        [JsonProperty("token_secret")]
        public string TokenSecret { get; set; }
    }
}
