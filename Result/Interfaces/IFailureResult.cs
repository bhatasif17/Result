namespace Result.Interfaces;

/// <summary>
///     A <see cref="INoneResult{T}" /> indicating failure, with no expected return value.
/// </summary>
/// <seealso cref="INoneResult" />
public interface IFailureResult : INoneResult
{
    /// <summary>
    ///     Gets the exception.
    /// </summary>
    /// <value>
    ///     The exception.
    /// </value>
    System.Exception Exception { get; }
}

/// <summary>
///     A <see cref="INoneResult{T}" /> indicating failure, with an expected return value of type <typeparamref name="T" />.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="INoneResult" />
internal interface IFailureResult<T> : IFailureResult, INoneResult<T>
{
}