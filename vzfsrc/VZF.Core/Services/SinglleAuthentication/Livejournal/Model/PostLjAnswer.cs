using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace Social.Livejournal.Model
{

    public class PostLjAnswer
    {
        public int anum { get; set; }

        public int itemid { get; set; }

        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public string url { get; set; }
    }
}
