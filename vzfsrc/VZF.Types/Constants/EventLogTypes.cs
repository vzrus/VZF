namespace YAF.Types.Constants
{
    /// <summary>
    /// The event log types. Always use the same numbers in the enumeration and NEVER change the pairs. 
    /// </summary>
    public enum EventLogTypes
    {
        /// <summary>
        /// The debug.
        /// </summary>
        Debug = -1000,

        /// <summary>
        /// The trace.
        /// </summary>
        Trace = -500,

        /// <summary>
        ///   The error.
        /// </summary>
        Error = 0,

        /// <summary>
        ///   The warning.
        /// </summary>
        Warning = 1,

        /// <summary>
        ///   The information.
        /// </summary>
        Information = 2,

        /// <summary>
        ///   The sql error.
        /// </summary>
        SqlError = 3,

        /// <summary>
        ///   The user suspended.
        /// </summary>
        UserSuspended = 1000,

        /// <summary>
        ///   The user unsuspended.
        /// </summary>
        UserUnsuspended = 1001,

        /// <summary>
        ///   The user deleted.
        /// </summary>
        UserDeleted = 1002,

        /// <summary>
        /// The ban set.
        /// </summary>
        IpBanSet = 1003,

        /// <summary>
        /// The Ip Ban Lifted.
        /// </summary>
        IpBanLifted = 1004
    }
}