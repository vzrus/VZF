using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Social.Vkontakte
{
    [JsonObject]
    public class VkPhotoUploadServerResponse
    {
        [JsonProperty("response")]
        public PhotoUploadServerItem Response { get; set; }
    }

    public class PhotoUploadServerItem
    {
        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }

    }
}
