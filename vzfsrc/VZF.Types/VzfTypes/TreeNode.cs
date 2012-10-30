using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VZF.Types
{
    public class TreeNode
    {
        /// <summary>
        /// Title of the Node
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Parent node of leaf node.
        /// </summary>
        public bool isFolder { get; set; }
        /// <summary>
        /// Lazy loading enabled or not.
        /// </summary>
        public bool isLazy { get; set; }
        /// <summary>
        /// Hidden id of the Node
        /// </summary>
        public string key { get; set; }
    }
}
