namespace YAF.Types.Attributes
{
  #region Using

  using System;

  #endregion

  /// <summary>
  /// The service lifetime scope.
  /// </summary>
  public enum ServiceLifetimeScope
  {
    /// <summary>
    ///   The singleton.
    /// </summary>
    Singleton, 

    /// <summary>
    ///   Externally owned scope -- regular garbage collection
    /// </summary>
    Transient, 

    /// <summary>
    ///   The owned by the container lifetime.
    /// </summary>
    OwnedByContainer, 

    /// <summary>
    ///   One instance per container scope
    /// </summary>
    InstancePerScope, 

    /// <summary>
    ///   One instance per dependancy.
    /// </summary>
    InstancePerDependancy, 

    /// <summary>
    ///   One instance per context.
    /// </summary>
    InstancePerContext
  }

  /// <summary>
  /// The export service attribute.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public class ExportServiceAttribute : Attribute
  {
    #region Constructors and Destructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportServiceAttribute"/> class.
    /// </summary>
    /// <param name="serviceLifetimeScope">
    /// The service lifetime scope.
    /// </param>
    /// <param name="named">
    /// The named.
    /// </param>
    /// <param name="registerSpecifiedTypes">
    /// The register Specified Types.
    /// </param>
    public ExportServiceAttribute(
      ServiceLifetimeScope serviceLifetimeScope, [CanBeNull] string named, [NotNull] params Type[] registerSpecifiedTypes)
    {
      this.Named = named;
      this.ServiceLifetimeScope = serviceLifetimeScope;
      this.RegisterSpecifiedTypes = registerSpecifiedTypes;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExportServiceAttribute"/> class.
    /// </summary>
    /// <param name="serviceLifetimeScope">
    /// The service lifetime scope.
    /// </param>
    public ExportServiceAttribute(ServiceLifetimeScope serviceLifetimeScope)
      : this(serviceLifetimeScope, null)
    {
    }

    #endregion

    #region Properties

    /// <summary>
    ///   Gets or sets Named.
    /// </summary>
    public string Named { get; set; }

    /// <summary>
    ///   Gets or sets RegisterSpecifiedTypes.
    /// </summary>
    public Type[] RegisterSpecifiedTypes { get; set; }

    /// <summary>
    ///   Gets or sets ServiceLifetimeScope.
    /// </summary>
    public ServiceLifetimeScope ServiceLifetimeScope { get; protected set; }

    #endregion
  }
}