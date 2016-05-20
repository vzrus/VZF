namespace YAF.Types.Attributes
{
  #region Using

  using System;

  #endregion

  /// <summary>
  /// Exclude a field from the dynamic object conversion.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
  public class ExcludeAttribute : Attribute
  {
  }
}