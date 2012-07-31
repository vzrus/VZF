using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Social.Twitter
{
    public interface ITwitterAppConfig
    {
        string twitterConsumerKey { get; }
        string twitterConsumerSecret { get; }
    }
}
