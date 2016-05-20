namespace YAF.Types.Attributes
{
  #region Using

  using System;

  #endregion

  /// <summary>
  /// The inject attribute -- mark a property that needs injection. Must be public.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public class Inject : Attribute
  {
  }
}