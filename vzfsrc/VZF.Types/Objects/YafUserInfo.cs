
namespace VZF.Types.Objects
{
    using System;

    /// <summary>
    /// Yaf User Info
    /// </summary>
    [Serializable]
    public class YafUserInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the realname.
        /// </summary>
        /// <value>
        /// The realname.
        /// </value>
        public string realname { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        /// <value>
        /// The avatar.
        /// </value>
        public string avatar { get; set; }

        /// <summary>
        /// Gets or sets the interests.
        /// </summary>
        /// <value>
        /// The interests.
        /// </value>
        public string interests { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public string homepage { get; set; }

        /// <summary>
        /// Gets or sets the profilelink.
        /// </summary>
        /// <value>
        /// The profilelink.
        /// </value>
        public string profilelink { get; set; }

        /// <summary>
        /// Gets or sets the posts.
        /// </summary>
        /// <value>
        /// The posts.
        /// </value>
        public string posts { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public string points { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string rank { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the joined.
        /// </summary>
        /// <value>
        /// The joined.
        /// </value>
        public string joined { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="YafUserInfo"/> is online.
        /// </summary>
        /// <value>
        ///   <c>true</c> if online; otherwise, <c>false</c>.
        /// </value>
        public bool online { get; set; }

        /// <summary>
        /// Gets or sets the action buttons.
        /// </summary>
        /// <value>
        /// The action buttons.
        /// </value>
        public string actionButtons { get; set; }
    }
}
