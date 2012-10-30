using System;

using Twitterizer;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Social.Twitter
{

    /// <summary>
    /// A consumer capable of communicating with Twitter.
    /// </summary>
    public class TwitterProvider
    {
        private static string RequestTokenUri = "https://api.twitter.com/oauth/request_token?oauth_callback={0}";

        private static string AccessToken = "https://api.twitter.com/oauth/access_token";

        public ITwitterAppConfig Config { get; set; }

        public TwitterAccessToken twitterAccessToken { get; set; }

        public string Authorize(string redirectTo)
        {
            OAuthTokenResponse requestToken = OAuthUtility.GetRequestToken(Config.twitterConsumerKey, Config.twitterConsumerSecret, redirectTo);

            // Direct or instruct the user to the following address:
            Uri authorizationUri = OAuthUtility.BuildAuthorizationUri(requestToken.Token);

            return authorizationUri.ToString();
        }

        public TwitterAccessToken GetAuthToken(string requestToken, string verifier)
        {
            var accessToken = OAuthUtility.GetAccessToken(Config.twitterConsumerKey, Config.twitterConsumerSecret,
                                                      requestToken, verifier);

            twitterAccessToken = new TwitterAccessToken()
            {
                Token = accessToken.Token,
                TokenSecret = accessToken.TokenSecret,
                UserId = accessToken.UserId
            };
            return twitterAccessToken;
        }

        public string GetUserInfo(decimal userId)
        {
            var  tokens =  new OAuthTokens();
            tokens.ConsumerKey = Config.twitterConsumerKey;
            tokens.ConsumerSecret = Config.twitterConsumerSecret;
            tokens.AccessToken = twitterAccessToken.Token;
            tokens.AccessTokenSecret = twitterAccessToken.TokenSecret;

            var response = TwitterUser.Show(tokens, userId);
            return response.Content;
        }

        public void Publish(Post post)
        {
            var  tokens =  new OAuthTokens();
            tokens.ConsumerKey = Config.twitterConsumerKey;
            tokens.ConsumerSecret = Config.twitterConsumerSecret;
            tokens.AccessToken = twitterAccessToken.Token;
            tokens.AccessTokenSecret = twitterAccessToken.TokenSecret; 

            TwitterStatus.Update(tokens, post.TwitterText);
            
        }

    }
}