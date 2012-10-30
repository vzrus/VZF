using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Social.Livejournal.Model
{
    public class FriendgroupLj
    {
        public int id { get; set; }

        public string name { get; set; }

        public int sortorder { get; set; }

        public int @public { get; set; }
    }
}
