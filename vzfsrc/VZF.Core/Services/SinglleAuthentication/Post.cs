using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Social
{
    public class Post
    {
        public string Title { get; set; }
        
        public string Link { get; set; }

        public string Teaser { get; set; }

        public string Preview {get; set;}

        public List<string> Images { get; set; }

        public string Content { get; set; }

        public string TwitterText { get; set; }
    }
}
