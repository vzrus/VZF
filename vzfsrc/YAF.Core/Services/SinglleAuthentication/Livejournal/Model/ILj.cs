using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace Social.Livejournal.Model
{
    [XmlRpcUrl("http://www.livejournal.com/interface/xmlrpc")]
    public interface ILj : IXmlRpcProxy
    {
        [XmlRpcMethod("LJ.XMLRPC.login")]
        LjUserInfo Login(UserPassword user);

        [XmlRpcMethod("LJ.XMLRPC.postevent")]
        PostLjAnswer Post(PostLj post);
    }
}
