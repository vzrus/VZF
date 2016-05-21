namespace YAF.Modules
{
    #region Using

    using System;
    using System.Web;

    using VZF.Utils.Helpers;

    using YAF.Core;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.EventProxies;
    using YAF.Types.Interfaces;

    #endregion

    /// <summary>
    /// The last visit handler.
    /// </summary>
    [ExportService(ServiceLifetimeScope.InstancePerScope)]
    public class LastVisitEventHandler : IHandleEvent<ForumPagePreLoadEvent>, IHandleEvent<ForumPageUnloadEvent>
    {
        private readonly HttpRequestBase _requestBase;

        private readonly HttpResponseBase _responseBase;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LastVisitEventHandler"/> class.
        /// </summary>
        /// <param name="yafSession">The yaf session.</param>
        /// <param name="requestBase">The request base.</param>
        /// <param name="responseBase">The response base.</param>
        public LastVisitEventHandler(
            [NotNull] IYafSession yafSession, HttpRequestBase requestBase, HttpResponseBase responseBase)
        {
            this._requestBase = requestBase;
            this._responseBase = responseBase;
            this.YafSession = yafSession;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets Order.
        /// </summary>
        public int Order
        {
            get
            {
                return 1000;
            }
        }

        /// <summary>
        /// Gets or sets YafSession.
        /// </summary>
        public IYafSession YafSession { get; set; }

        /// <summary>
        /// Handles the specified @event.
        /// </summary>
        /// <param name="event">The @event.</param>
        public void Handle(ForumPageUnloadEvent @event)
        {
            /*/if (YafContext.Current.IsGuest && (!this.YafSession.LastVisit.HasValue || this.YafSession.LastVisit.Value <= DateTimeHelper.SqlDbMinTime()))
            //{
            //  // update last visit going forward...
            //  this.YafSession.LastVisit = DateTime.UtcNow;
            //}*/
        }

        #endregion

        #region Implemented Interfaces

        #region IHandleEvent<ForumPagePreLoadEvent>

        /// <summary>
        /// The handle.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public void Handle([NotNull] ForumPagePreLoadEvent @event)
        {
            string previousVisitKey = "PreviousVisit";

            if (!YafContext.Current.IsGuest && YafContext.Current.Page[previousVisitKey] != DBNull.Value
                && !this.YafSession.LastVisit.HasValue)
            {
                this.YafSession.LastVisit = Convert.ToDateTime(YafContext.Current.Page[previousVisitKey]);
            }
            else if (YafContext.Current.IsGuest && !this.YafSession.LastVisit.HasValue)
            {
                if (this._requestBase.Cookies.Get(previousVisitKey) != null)
                {
                    // have previous visit cookie...
                    string previousVisitInsecure = this._requestBase.Cookies.Get(previousVisitKey).Value;
                    DateTime previousVisit;

                    if (DateTime.TryParse(previousVisitInsecure, out previousVisit))
                    {
                        this.YafSession.LastVisit = previousVisit;
                    }
                }
                else
                {
                    this.YafSession.LastVisit = DateTimeHelper.SqlDbMinTime();
                }

                // set the last visit cookie...
                HttpCookie httpCookie = new HttpCookie(previousVisitKey, DateTime.UtcNow.ToString())
                    { Expires = DateTime.Now.AddMonths(6) };
                this._responseBase.Cookies.Add(httpCookie);
            }
        }

        #endregion

        #endregion
    }
}