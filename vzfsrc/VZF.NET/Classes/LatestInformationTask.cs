namespace YAF.Classes
{
  #region Using

    using System;
    using System.Web;

    using VZF.Data.Common;

    using YAF.Core;
    using YAF.Core.Tasks;
    using YAF.RegisterV2;
    using YAF.Types;
    using YAF.Types.Attributes;
    using YAF.Types.Interfaces;
    using VZF.Utils;

    #endregion

  /// <summary>
  /// Gets the latest information from YAF headquarters
  /// </summary>
  [ExportService(ServiceLifetimeScope.Singleton)]
  public class LatestInformationTask : LongBackgroundTask, IStartTasks
  {
    #region Constants and Fields

    /// <summary>
    ///   The _task name.
    /// </summary>
    private const string _taskName = "LatestInformationTask";

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets Culture.
    /// </summary>
    public string Culture { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// The run once.
    /// </summary>
    public override void RunOnce()
    {
      return;
      try
      {
        using (var reg = new RegisterV2())
        {
          reg.Timeout = 30000;

          // load the latest info -- but only provide the current version information and the user's two-letter language information. Nothing trackable.))
          var latestInfo = reg.LatestInfo(YafForumInfo.AppVersionCode, this.Culture);

          if (latestInfo != null)
          {
            this.Get<HttpApplicationStateBase>().Set("YafRegistrationLatestInformation", latestInfo);
          }
        }
      }
      catch (Exception x)
      {
          CommonDb.eventlog_create(YafContext.Current.PageModuleID, null, _taskName, "Exception In {1}: {0}".FormatWith(x, _taskName));
      }
    }

    #endregion

    #region Implemented Interfaces

    #region IStartTasks

    /// <summary>
    /// Start various tasks
    /// </summary>
    /// <param name="manager">
    /// </param>
    public void Start([NotNull] ITaskModuleManager manager)
    {
      CodeContracts.ArgumentNotNull(manager, "manager");

      this.Culture = "US";

      manager.StartTask(_taskName, () => this);
    }

    #endregion

    #endregion
  }
}