using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Social.Facebook
{
    public class FbProvider
    {
        private static string AuthorizeUri = "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope=email,publish_stream,offline_access";

        private static string GetAccessTokenUri = "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}";
        private static string GetUserInfoUri = "https://graph.facebook.com/me?access_token={0}";
        private static string PostUri = "https://graph.facebook.com/{0}/feed?access_token={1}";

        private static string GraphUri = "https://graph.facebook.com/{0}";

        public IFbAppConfig Config { get; set; }


        public string AccessToken { get; set; }
        
        public string Authorize(string redirectTo)
        {
            return string.Format(AuthorizeUri, Config.AppId, redirectTo);
        }

        public bool GetAccessToken(string code, string redirectTo)
        {
            var request = string.Format(GetAccessTokenUri, Config.AppId, redirectTo, Config.AppSecret, code);
            WebClient webClient = new WebClient();
            string response = webClient.DownloadString(request); 
            try
            {
                var pairResponse = response.Split('&');
                AccessToken = pairResponse[0].Split('=')[1];
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }

        public JObject GetUserInfo()
        {
            var request = string.Format(GetUserInfoUri, AccessToken);
            WebClient webClient = new WebClient();

            string response = webClient.DownloadString(request); 
            return JObject.Parse(response);
        }

        public void Publish(Post post, string facebookId = null)
        {
            if (string.IsNullOrWhiteSpace(facebookId))
            {
                var json = GetUserInfo();
                var fbUserInfo = JsonConvert.DeserializeObject<FbUserInfo>(json.ToString());
                facebookId = fbUserInfo.Id;
            }
            var request = string.Format(PostUri, facebookId, AccessToken);

            var sb = new StringBuilder();

            //name
            if (!string.IsNullOrWhiteSpace(post.Title))
            {
                sb.AppendFormat("name={0}&", post.Title);
            }

            //message
            if (!string.IsNullOrWhiteSpace(post.Teaser))
            {
                sb.AppendFormat("message={0}&", post.Teaser);
            }

            //picture
            if (!string.IsNullOrWhiteSpace(post.Preview))
            {
                sb.AppendFormat("picture={0}&", post.Preview);
            }

            //link
            if (!string.IsNullOrWhiteSpace(post.Link))
            {
                sb.AppendFormat("link={0}&", post.Link);
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var bytes = Encoding.UTF8.GetBytes(sb.ToString());

            var responseBytes = webClient.UploadData(request, bytes);

            var response = Encoding.UTF8.GetString(responseBytes);
            JObject jsonObject = JObject.Parse(response);
        }
    }
}
