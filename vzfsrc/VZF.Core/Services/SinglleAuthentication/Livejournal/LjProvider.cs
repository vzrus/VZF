using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using Social.Livejournal.Model;

namespace Social.Livejournal
{
    public class LjProvider
    {
        public LjUserInfo Auth(UserPassword username)
        {
            ILj proxy = XmlRpcProxyGen.Create<ILj>();
            var ans = proxy.Login(username);
            return ans;
        }

        public void Publish(UserPassword username, Post message, string ljgroup = null)
        {
            ILj proxy = XmlRpcProxyGen.Create<ILj>();
            var post = new PostLj();
            post.username = username.username;
            post.password = username.password;
            post.ver = 1;
            post.@event = message.Content;
            post.subject = message.Title;
            post.lineendings = "pc";
            post.year = DateTime.Now.Year;
            post.mon = DateTime.Now.Month;
            post.day = DateTime.Now.Day;
            post.hour = DateTime.Now.Hour;
            post.min = DateTime.Now.Minute;
            if (!string.IsNullOrWhiteSpace(ljgroup))
            {
                post.usejournal = ljgroup;
            }
            else
            {
                post.usejournal = username.username;
            }
            var ans = proxy.Post(post);
        }
    }
}
