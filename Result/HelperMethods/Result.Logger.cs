using System.Diagnostics.CodeAnalysis;
using Result.Interfaces;

namespace Result.HelperMethods;

/// <summary>
///     Provides the setter for the log action that takes place when a <see cref="IFailureResult" /> is constructed.
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBeInternal")]
public static partial class Result
{
    internal static Action<System.Exception> Logger { get; private set; } = exception => { };

    /// <summary>
    ///     Sets the logger.
    /// </summary>
    /// <param name="action">The action.</param>
    public static void SetLogger(Action<System.Exception> action)
    {
        Logger = action;
    }
}