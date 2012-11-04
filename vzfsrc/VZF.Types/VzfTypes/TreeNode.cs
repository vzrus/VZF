using System;

namespace VZF.Types
{
    /// <summary>
    /// Dynatree tree node interface.
    /// </summary>
    public interface ITreeItem
    {
    }
    /// <summary>
    /// Dynatree TreeNode class
    /// </summary>
    public class TreeNode : ITreeItem
    {
        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string title;

        /// <summary>
        /// Gets the Tooltip.
        /// </summary>
        public string tooltip;

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string key;

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public string addClass;

        /// <summary>
        /// Gets the rel attr.
        /// </summary>
        // public string rel;

        /// <summary>
        /// Gets the State.
        /// </summary>
        public bool isFolder;

        /// <summary>
        /// Gets the State.
        /// </summary>
        public bool isLazy;
    }
}
